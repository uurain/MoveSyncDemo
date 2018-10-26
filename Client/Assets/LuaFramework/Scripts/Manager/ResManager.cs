using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using LuaInterface;
using UObject = UnityEngine.Object;
using LuaFramework;
using UnityEngine.SceneManagement;
using Tangzx.ABSystem;



public class ResManager : Manager
{
    public enum ETaskType
    {
        gameobject,
        abinfo,
    }
    public class TaskLoader
    {
        public TaskLoader(string p, ETaskType t)
        {
            path = p;
            taskType = t;
        }

        private ETaskType taskType = ETaskType.gameobject;
        private string path;

        public LoadGameObjectCompleteHandler onGoComplete;
        public AssetBundleManager.LoadAssetCompleteHandler onAssetComplete;

        public void Begin(int prority = 0)
        {
            if(taskType == ETaskType.gameobject)
            {
                AssetObject assetObj = AppFacade.Instance.GetManager<ResManager>(ManagerName.Resource).GetCached(path);
                if(assetObj != null)
                {
                    RequestGoComplete(assetObj);
                    RequstComplete();
                    return;
                }
            }
            AppFacade.Instance.GetManager<ResManager>(ManagerName.Resource).assetManager.Load(path, prority, OnLoadComplete);
        }

        private void OnLoadComplete(AssetBundleInfo abInfo)
        {
            if (abInfo != null)
            {
                switch(taskType)
                {
                    case ETaskType.gameobject:
                        {
                            GameObject go = abInfo.Instantiate();
                            AssetObject assetObj = go.AddUniqueCompoment<AssetObject>();
                            assetObj.key = path;
                            RequestGoComplete(assetObj);
                        }
                        break;
                    case ETaskType.abinfo:
                        {
                            RequestAbInfoComplete(abInfo);
                        }
                        break;
                }
            }
            RequstComplete();
        }

        private void RequestGoComplete(AssetObject assetObj)
        {
            if (onGoComplete != null)
            {
                var handler = onGoComplete;
                onGoComplete = null;
                handler(assetObj);
            }
        }

        private void RequestAbInfoComplete(AssetBundleInfo abInfo)
        {
            if (onAssetComplete != null)
            {
                var handler = onAssetComplete;
                onAssetComplete = null;
                handler(abInfo);
            }
        }

        private void RequstComplete()
        {
            AppFacade.Instance.GetManager<ResManager>(ManagerName.Resource).LoadComplete(this);
        }
    }

    public delegate void LoadGameObjectCompleteHandler(AssetObject go);
    public delegate void LoadObjectCompleteHandler(UnityEngine.Object obj);

    private const int MaxRequstCount = 5;

    // 加载中
    private bool _isCurrentLoading;
    // 缓存
    private Dictionary<string, Tangzx.ABSystem.ObjectPool<AssetObject>> _cachedAssetObj = new Dictionary<string, Tangzx.ABSystem.ObjectPool<AssetObject>>();

    // 等待加载的task
    private List<TaskLoader> _waitLoadQueue = new List<TaskLoader>();
    // 正在加载的task
    private HashSet<TaskLoader> _currentLoadQueue = new HashSet<TaskLoader>();
    // 加载loader缓存
    private Dictionary<string, TaskLoader> _loaderCache = new Dictionary<string, TaskLoader>();

    public AssetBundleManager assetManager;
    AssetBundleLoadProgress mProcess;

    Transform _cachedParent = null;

    public void Initialize(string manifestName, Action initOK)
    {
        GameObject cachedGo = new GameObject("CachedParent");
        cachedGo.SetActive(false);
        _cachedParent = cachedGo.transform;
        _cachedParent.SetParent(transform);

        if (assetManager == null)
            assetManager = gameObject.AddUniqueCompoment<AssetBundleManager>();
        assetManager.Init(initOK);
        assetManager.onProgress = OnAssetProgress;
    }

    private void OnAssetProgress(AssetBundleLoadProgress progress)
    {
        mProcess = progress;
    }

    public float GetProgress()
    {
        if(mProcess != null)
        {
            return mProcess.complete;
        }
        return 0;
    }

    public void LoadPackage(string fullPath, Action<string> dl, LuaFunction luaFunc)
    {
#if AB_MODE
        assetManager.Load(fullPath, delegate (Tangzx.ABSystem.AssetBundleInfo abInfo) {
            string pkgName = "";
            if (abInfo == null)
            {
                Debug.LogError("LoadPackage error:" + fullPath);
            }
            else
            {
                // 这里改了个恶心的bug， 同时调用一样的图集的时候 第一次进来会直接卸载ab,第二次进来ab就已经没了。
                // 所以这里需要做一下判断，按道理应该用package name来做判断 去个巧 这样判断也是可以满足需求
                if (abInfo.bundle != null)
                {
                    pkgName = FairyGUI.UIPackage.AddPackage(abInfo.bundle).name;
                }                
            }
            if (dl != null)
                dl(pkgName);
            if(luaFunc != null)
            {
                luaFunc.Call(pkgName);
                luaFunc.Dispose();
                luaFunc = null;
            }
        });
#else
#if UNITY_EDITOR
        string pkgName = FairyGUI.UIPackage.AddPackage(fullPath, (string name, string extension, System.Type type) => {
            string pName = name.Substring(name.LastIndexOf('&') + 1, name.Length - name.LastIndexOf('&') - 1);
            return UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/AbAsset/" + fullPath + "/" + pName + extension, type);
        }).name;
        if (dl != null)
            dl(pkgName);
        if (luaFunc != null)
        {
            luaFunc.Call(pkgName);
            luaFunc.Dispose();
            luaFunc = null;
        }
#endif
#endif
    }

    void Update()
    {
        if(_isCurrentLoading)
        {
            if(_waitLoadQueue.Count > 0)
            {
                int canLoadCount = MaxRequstCount - _currentLoadQueue.Count;
                while (canLoadCount > 0 && _waitLoadQueue.Count > 0)
                {
                    TaskLoader taskLoader = _waitLoadQueue[0];
                    _waitLoadQueue.RemoveAt(0);
                    _currentLoadQueue.Add(taskLoader);
                    taskLoader.Begin();
                    canLoadCount--;
                }
            }
        }
    }

    public void Load(string path, AssetBundleManager.LoadAssetCompleteHandler handler = null)
    {
        TaskLoader taskLoader = CreateLoadTaskLoader(path, ETaskType.abinfo);
        taskLoader.onAssetComplete += handler;

        AddWaitQueue(taskLoader);
    }

    public void LoadGameObject(string path, LoadGameObjectCompleteHandler handler = null)
    {
        TaskLoader taskLoader = CreateLoadTaskLoader(path, ETaskType.gameobject);
        taskLoader.onGoComplete += handler;

        AddWaitQueue(taskLoader);
    }

    public void LoadScene(string sceneName, System.Action handler)
    {
#if AB_MODE
        string path = string.Format("Scene/{0}.unity", sceneName);
        Load(path, delegate (AssetBundleInfo info)
        {
            SceneManager.LoadScene(sceneName);
            if (handler != null)
                handler();
        });
#else
        SceneManager.LoadScene(sceneName);
        if (handler != null)
            handler();
#endif
    }

    public void LoadComplete(TaskLoader task)
    {
        _currentLoadQueue.Remove(task);
        if (_waitLoadQueue.Count <= 0)
            _isCurrentLoading = false;
    }

    public AssetObject GetCached(string path)
    {
        Tangzx.ABSystem.ObjectPool<AssetObject> aPool = null;
        if (_cachedAssetObj.TryGetValue(path, out aPool))
        {
            if (aPool.countInactive > 0)
            {
                AssetObject ao = aPool.Get();
                ao.CachedTransform.SetParent(null);
                return ao;
            }
        }
        return null;
    }

    public void Unload(AssetObject assetObj)
    {
        if(assetObj == null)
        {
            Debug.LogError("Unknow Destroy!");
            return;
        }
        Tangzx.ABSystem.ObjectPool<AssetObject> aPool = null;
        if(!_cachedAssetObj.TryGetValue(assetObj.key, out aPool))
        {
            aPool = new Tangzx.ABSystem.ObjectPool<AssetObject>(null, null);
        }
        if (aPool.countInactive > 5)
            GameObject.DestroyImmediate(assetObj.gameObject);
        else
        {
            assetObj.CachedTransform.SetParent(_cachedParent);
            aPool.Release(assetObj);
        }
    }

    private void AddWaitQueue(TaskLoader taskLoader)
    {
        if(!_waitLoadQueue.Contains(taskLoader))
            _waitLoadQueue.Add(taskLoader);
        _isCurrentLoading = true;
    }


    private TaskLoader CreateLoadTaskLoader(string abFileName, ETaskType t)
    {
        TaskLoader loader = null;

        if (_loaderCache.ContainsKey(abFileName))
        {
            loader = _loaderCache[abFileName];
        }
        else
        {
            loader = new TaskLoader(abFileName, t);
        }
        return loader;
    }
}
