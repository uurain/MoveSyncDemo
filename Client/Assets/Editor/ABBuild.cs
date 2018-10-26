using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Security.Cryptography;
using LuaFramework;

// @开头的文件夹 默认下面的所有文件都打包在同一个ab文件中 AssetbundleName全都设置文件夹名

public class AssetBundleRef
{
    public string path;
    public int refCount;
    public System.Type type;
    public Object obj;
    public string guid;

    public AssetBundleRef()
    {
        refCount = 1;
    }
}

public class AssetBundleBinary
{
    public string path;
    public string fileMd5;
    public string guidName;
    public bool isExtension = true; // 是否有后缀名
}

public class ABBuild {

    public static List<string> MainFileSplitList = new List<string>();

    public static Dictionary<string, AssetBundleRef> mDependenciesDic = new Dictionary<string, AssetBundleRef>();

    public static List<AssetBundleBinary> mAllAbFiles = new List<AssetBundleBinary>();

    public static string BuildTargetPath {
        get
        {
            return Application.dataPath + "/StreamingAssets/";
        }
    }
    public static string BuildSrcPath
    {
        get { return "Assets/AbAsset"; }
    }
    // 目录中所有子文件为一个AB
    public static readonly string _MainFileSplit = "@";
    // _ 表示这个文件夹被忽略
    public static readonly string _NotUsed = "_";

    public static readonly string _shaderAbName = "_allShaderAb";

    public static readonly bool _IsMd5 = false;

    [MenuItem("Assets/清理所有AssetBundle的Tag")]
    public static void ClearAllAssetNames()
    {
        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        if (assetBundleNames == null || assetBundleNames.Length <= 0)
            return;
        for (int i = 0; i < assetBundleNames.Length; ++i)
        {
            float process = ((float)i) / ((float)assetBundleNames.Length);
            EditorUtility.DisplayProgressBar("清理Tag中...", assetBundleNames[i], process);
            AssetDatabase.RemoveAssetBundleName(assetBundleNames[i], true);
            EditorUtility.UnloadUnusedAssetsImmediate();
        }
        EditorUtility.ClearProgressBar();
    }

    [MenuItem("Assets/平台打包/Android")]
    public static void OnBuildPlatformAndroid()
    {
        BuildPlatform(BuildTarget.Android);
    }
    [MenuItem("Assets/平台打包/Windows")]
    public static void OnBuildPlatformWindows()
    {
        BuildPlatform(BuildTarget.StandaloneWindows);
    }

    static public void BuildPlatform(BuildTarget platform)
    {
        MainFileSplitList.Clear();
        mDependenciesDic.Clear();
        mAllAbFiles.Clear();

        if (!AppConst.LuaBundleMode)
        {
            HandleLuaFile();
        }

        List<string> assetsEnum = GetFiles(BuildSrcPath, false).Where(s => s.Contains(".meta") == false).ToList();

        for (int i = 0; i < assetsEnum.Count; ++i)
        {
            setAssetBundleName(assetsEnum[i]);
        }

        UnityEngine.Debug.Log(MainFileSplitList.Count);
        for (int i = 0; i < MainFileSplitList.Count; ++i)
        {
            List<string> mainSplitAssets = GetFiles(MainFileSplitList[i], true).Where(s => s.Contains(".meta") == false).ToList();
            assetsEnum.AddRange(mainSplitAssets);
            for (int j = 0; j < mainSplitAssets.Count; ++j)
            {
                setAssetBundleName(mainSplitAssets[j], MainFileSplitList[i]);
            }
        }

        List<string> sceneList = SetScenePackage();
        //assetsEnum.AddRange(sceneList);

        for (int i = 0; i < assetsEnum.Count; ++i)
        {
            SetDefend(assetsEnum[i], assetsEnum);
        }



        SetDependAbName();

        if (!Directory.Exists(BuildTargetPath))
        {
            Directory.CreateDirectory(BuildTargetPath);
        }
        BuildPipeline.BuildAssetBundles(BuildTargetPath, BuildAssetBundleOptions.DisableWriteTypeTree |
            BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.ChunkBasedCompression,
            platform);

        //SetAssetbundleBinaryTxt();

        //RemoveBundleManifestFiles(BuildTargetPath);

        BuildFileIndex();
        AssetDatabase.Refresh();
    }

    public static void SetDefend(string filePath, List<string> assetsEnum)
    {
        string[] dependencies = AssetDatabase.GetDependencies(filePath);
        for(int i = 0; i < dependencies.Length; ++i)
        {
            if (assetsEnum.Contains(dependencies[i]))
                continue;
            if(dependencies[i].EndsWith(".cs"))
                continue;
            string guid = AssetDatabase.AssetPathToGUID(dependencies[i]);
            AssetBundleRef abRef = null;
            if(!mDependenciesDic.TryGetValue(guid, out abRef))
            {
                Object obj = AssetDatabase.LoadMainAssetAtPath(dependencies[i]);
                abRef = new AssetBundleRef();
                abRef.obj = obj;
                abRef.path = dependencies[i];
                abRef.type = obj.GetType();
                abRef.guid = guid;

                mDependenciesDic.Add(guid, abRef);
            }
            else
            {
                abRef.refCount++;
            }
        }
    }

    public static List<string> SetScenePackage()
    {
        List<string> assetsEnum = GetFiles(BuildSrcPath + "/_Scene", true).Where(s => s.EndsWith(".unity") == true).ToList();
        for(int i = 0; i < assetsEnum.Count; ++i)
        {
            setAssetBundleName(assetsEnum[i]);
        }
        return assetsEnum;
    }

    public static void SetDependAbName()
    {
        foreach(var val in mDependenciesDic)
        {
            if (val.Value.type == typeof(Shader))
            {
                setAssetBundleName(val.Value.path, "", _shaderAbName);
            }
            else if (val.Value.refCount > 1)
            {
                setAssetBundleName(val.Value.path, "", "");
            }
        }
    }

    public static void SetAssetbundleBinaryTxt()
    {
        AssetBundleBinary dependency = new AssetBundleBinary();
        dependency.path = AppConst.AssetDir;
        dependency.guidName = dependency.path;
        dependency.isExtension = false;
        mAllAbFiles.Add(dependency);

        FileStream fs = new FileStream(BuildTargetPath + "abFile.txt", FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < mAllAbFiles.Count; ++i)
        {
            if(mAllAbFiles[i].isExtension)
                mAllAbFiles[i].fileMd5 = Util.md5file(BuildTargetPath + mAllAbFiles[i].guidName + ".unity3d");
            else
                mAllAbFiles[i].fileMd5 = Util.md5file(BuildTargetPath + mAllAbFiles[i].guidName);
            sw.WriteLine(string.Format("{0}|{1}|{2}", mAllAbFiles[i].guidName, mAllAbFiles[i].fileMd5, mAllAbFiles[i].path));
        }
        sw.Close();
        fs.Close();
    }

    private static void RemoveBundleManifestFiles(string outPath)
    {
		string[] files = Directory.GetFiles(outPath, "*.manifest", SearchOption.TopDirectoryOnly);
		for (int i = 0; i < files.Length; ++i)
		{
			File.Delete(files[i]);
		}
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <param name="abNamePath">表示文件所在文件路径的根文件目录，用于将这个参数下的所有文件打包成一个ab</param>
    /// <param name="abAssetName">指定使用此参数作为ab名称</param>
    static public void setAssetBundleName(string filePath, string abNamePath = "", string abAssetName = "")
    {
        string abName = abAssetName;
        bool isAbAssetName = false;
        if (abName == "")
        {
            Object obj = AssetDatabase.LoadMainAssetAtPath(filePath);
            if (obj.GetType() == typeof(Shader))
            {
                setAssetBundleName(filePath, "", _shaderAbName);
                return;
            }
            if (abNamePath != "")
            {
                abName = abNamePath.Replace("\\", "/");
                isAbAssetName = true;
            }
            else
                abName = filePath;
            if(abName.StartsWith(BuildSrcPath))
                abName = abName.Substring(BuildSrcPath.Length + 1);
            else
                abName = abName.Substring(("Assets").Length + 1);// 这个不是打包文件夹内内容
            abName = abName.Replace("/", "$");
        }
        else
        {
            isAbAssetName = true;
        }        
        if (_IsMd5)
        {
            AssetBundleBinary info = new AssetBundleBinary();
            info.path = abName + ".unity3d";
            if (isAbAssetName)
            {
                abName = Util.md5(abName);
            }
            else
            {
                abName = AssetDatabase.AssetPathToGUID(filePath);
                abName = Util.md5(abName);
            }
            info.guidName = abName;
            if (mAllAbFiles.Find((p) => { return p.guidName == info.guidName; }) == null)
                mAllAbFiles.Add(info);
        }
        abName += ".unity3d";
        AssetImporter importer = AssetImporter.GetAtPath(filePath);
        if (importer.assetBundleName.CompareTo(abName) != 0)
        {
            importer.assetBundleName = abName;
            //importer.SaveAndReimport();
        }
    }

    // 获取文件夹下的所有文件包括子文件
    // IEnumerable<string> assetsEnum = GetFiles("Assets/Resources").Where(s => s.Contains(".meta") == false);
    public static List<string> GetFiles(string path, bool isSubDir)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        List<string> files = new List<string>();
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            if (Directory.Exists(path))
            {
                string local = System.IO.Path.GetFileName(path);
                //如果是目录的话 
                if (local.StartsWith(_MainFileSplit) && !isSubDir)
                {
                    // 下面的文件打包成一个
                    MainFileSplitList.Add(path);
                    continue;
                }
                else if(local.StartsWith(_NotUsed) && !isSubDir)
                {
                    continue;
                }
                else
                {
                    try
                    {
                        foreach (string subDir in Directory.GetDirectories(path))
                        {
                            queue.Enqueue(subDir);
                        }
                    }
                    catch (System.Exception ex)
                    {
                        UnityEngine.Debug.LogError(ex.Message);
                    }
                }
            }            
            try
            {
                files.AddRange(Directory.GetFiles(path).ToList());
            }
            catch (System.Exception ex)
            {
                UnityEngine.Debug.LogError(ex.Message);
            }
        }
        for (int i = 0; i < files.Count; ++i )
        {
            files[i] = files[i].Replace("\\", "/");
        }
        return files;
    }

    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();
    /// <summary>
    /// 处理Lua文件
    /// </summary>
    static void HandleLuaFile()
    {
        string resPath = BuildTargetPath;
        string luaPath = resPath + "/lua/";

        //----------复制Lua文件----------------
        if (!Directory.Exists(luaPath))
        {
            Directory.CreateDirectory(luaPath);
        }
        string[] luaPaths = { Application.dataPath + "/LuaFramework/lua/",
                              Application.dataPath + "/LuaFramework/Tolua/Lua/" };

        for (int i = 0; i < luaPaths.Length; i++)
        {
            paths.Clear(); files.Clear();
            string luaDataPath = luaPaths[i].ToLower();
            Recursive(luaDataPath);
            int n = 0;
            foreach (string f in files)
            {
                if (f.EndsWith(".meta")) continue;
                string newfile = f.Replace(luaDataPath, "");
                string newpath = luaPath + newfile;
                string path = Path.GetDirectoryName(newpath);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                if (File.Exists(newpath))
                {
                    File.Delete(newpath);
                }
                if (AppConst.LuaByteMode)
                {
                    //EncodeLuaFile(f, newpath);
                }
                else {
                    File.Copy(f, newpath, true);
                }
                UpdateProgress(n++, files.Count, newpath);
            }
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }

    static void UpdateProgress(int progress, int progressMax, string desc)
    {
        string title = "Processing...[" + progress + " - " + progressMax + "]";
        float value = (float)progress / (float)progressMax;
        EditorUtility.DisplayProgressBar(title, desc, value);
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs)
        {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    public static int isshareres(Object tobj)
    {
        System.Type type = tobj.GetType();
        if (type == typeof(Shader))
        {
            return 0;
        }
        if (type == typeof(Font))
        {
            return 0;
        }
        if (type == typeof(AudioClip))
            return 0;
        if (type == typeof(Texture2D) || type == typeof(Texture))
        {
            Texture tex = tobj as Texture;
            if (tex.width >= 512 || tex.height >= 512)
                return 0;
        }
        return -1;
    }

    static void BuildFileIndex()
    {
        string resPath = BuildTargetPath;
        ///----------------------创建文件列表-----------------------
        string newFilePath = resPath + "/files.txt";
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta") || file.Contains(".DS_Store") || file.EndsWith(".manifest")) continue;

            string md5 = Util.md5file(file);
            string value = file.Replace(resPath, string.Empty);
            sw.WriteLine(value + "|" + md5);
        }
        sw.Close(); fs.Close();
    }
}
