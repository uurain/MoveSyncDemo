using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaFramework;
using UnityEngine.AI;

public class AppConst {
#if UNITY_EDITOR
    public const bool DebugMode = true;                       //调试模式-用于内部测试
#else
    public const bool DebugMode = false;   
#endif
    /// <summary>
    /// 如果想删掉框架自带的例子，那这个例子模式必须要
    /// 关闭，否则会出现一些错误。
    /// </summary>
    public const bool ExampleMode = false;                       //例子模式 

    /// <summary>
    /// 如果开启更新模式，前提必须启动框架自带服务器端。
    /// 否则就需要自己将StreamingAssets里面的所有内容
    /// 复制到自己的Webserver上面，并修改下面的WebUrl。
    /// </summary>
    public const bool UpdateMode = false;                       //更新模式-默认关闭 
    public const bool LuaByteMode = false;                       //Lua字节码模式-默认关闭 
    public const bool LuaBundleMode = false;                    //Lua代码AssetBundle模式

    public const int TimerInterval = 1;
    public const int GameFrameRate = 30;                        //游戏帧频

    public const string AppName = "LuaFramework";               //应用程序名称
    public const string LuaTempDir = "Lua/";                    //临时目录
    public const string AppPrefix = AppName + "_";              //应用程序前缀
    public const string ExtName = ".unity3d";                   //素材扩展名
    public const string AssetDir = "StreamingAssets";           //素材目录 
    public const string WebUrl = "http://localhost:6688/";      //测试更新地址

    public static string UserId = string.Empty;                 //用户ID
    public static int SocketPort = 0;                           //Socket服务器端口
    public static string SocketAddress = string.Empty;          //Socket服务器地址

    public static string FrameworkRoot {
        get {
            return Application.dataPath + "/" + AppName;
        }
    }

    public static string GetPbFilePath()
    {
        string path = "";
        if (AppConst.DebugMode)
        {
            path =  AppConst.FrameworkRoot;
        }
        else {
            path = Util.DataPath;
        }
        return path + "/Lua/Protocol/Pbc/";
    }

    static int terrainMask = 0;
    public static float CheckHeight(Vector3 position)
    {
        if (terrainMask == 0)
            terrainMask = 1 << LayerMask.NameToLayer("Terrain");
        float temp = position.y;
        position.y = 10000;
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, 100000, terrainMask))
        {
            return hit.point.y;
        }
        return temp;
    }

    public static List<Vector3> GetMapNav(Vector3 srcPos, Vector3 tPos)
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        if (NavMesh.CalculatePath(srcPos, tPos, NavMesh.AllAreas, path))
        {
        }
        else
        {
            NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(tPos, out hit, 100.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                NavMesh.CalculatePath(srcPos, hit.position, NavMesh.AllAreas, path);
            }
        }

        if (path.status == NavMeshPathStatus.PathComplete || path.status == NavMeshPathStatus.PathPartial)
        {
            if(path.corners.Length > 1)
            {
                List<Vector3> pathList = new List<Vector3>();
                pathList.AddRange(path.corners);
                pathList.RemoveAt(0);
                return pathList;
            }
        }
        return null;
    }
}
