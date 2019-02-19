using UnityEngine;
using System.Collections;
using LuaFramework;
using System.Collections.Generic;
using pg;

public class Base : MonoBehaviour {
    private AppFacade m_Facade;
    private LuaManager m_LuaMgr;
    private ResManager m_ResMgr;
    private NetworkManager m_NetMgr;
    private SoundManager m_SoundMgr;
    private TimerManager m_TimerMgr;
    private ThreadManager m_ThreadMgr;
    private ObjectPoolManager m_ObjectPoolMgr;
    private SaveDataManager m_SaveDataMgr;
    private GameSceneMgr m_GameSceneMgr;
    //private UpdateManager m_UpdateManager;
    private DownloadManager m_DownloadManager;
    private DataMgr m_DataMgr;
    /// <summary>
    /// 注册消息
    /// </summary>
    /// <param name="view"></param>
    /// <param name="messages"></param>
    protected void RegisterMessage(IView view, List<string> messages) {
        if (messages == null || messages.Count == 0) return;
        Controller.Instance.RegisterViewCommand(view, messages.ToArray());
    }

    /// <summary>
    /// 移除消息
    /// </summary>
    /// <param name="view"></param>
    /// <param name="messages"></param>
    protected void RemoveMessage(IView view, List<string> messages) {
        if (messages == null || messages.Count == 0) return;
        Controller.Instance.RemoveViewCommand(view, messages.ToArray());
    }

    protected AppFacade facade {
        get {
            if (m_Facade == null) {
                m_Facade = AppFacade.Instance;
            }
            return m_Facade;
        }
    }

    protected LuaManager LuaManager {
        get {
            if (m_LuaMgr == null) {
                m_LuaMgr = facade.GetManager<LuaManager>(ManagerName.Lua);
            }
            return m_LuaMgr;
        }
    }

    protected ResManager ResManager {
        get {
            if (m_ResMgr == null) {
                m_ResMgr = facade.GetManager<ResManager>(ManagerName.Resource);
            }
            return m_ResMgr;
        }
    }
    //protected UpdateManager UpdateManager
    //{
    //    get
    //    {
    //        if (m_UpdateManager == null)
    //        {
    //            m_UpdateManager = facade.GetManager<UpdateManager>(ManagerName.Update);
    //        }
    //        return m_UpdateManager;
    //    }
    //}
    protected DownloadManager DownloadManager
    {
        get
        {
            if (m_DownloadManager == null)
            {
                m_DownloadManager = facade.GetManager<DownloadManager>(ManagerName.Download);
            }
            return m_DownloadManager;
        }
    }

    protected NetworkManager NetManager {
        get {
            if (m_NetMgr == null) {
                m_NetMgr = facade.GetManager<NetworkManager>(ManagerName.Network);
            }
            return m_NetMgr;
        }
    }

    protected SoundManager SoundManager {
        get {
            if (m_SoundMgr == null) {
                m_SoundMgr = facade.GetManager<SoundManager>(ManagerName.Sound);
            }
            return m_SoundMgr;
        }
    }

    protected SaveDataManager SaveDataManager
    {
        get
        {
            if (m_SaveDataMgr == null)
                m_SaveDataMgr = facade.GetManager<SaveDataManager>(ManagerName.SaveData);
            return m_SaveDataMgr;
        }
    }

    protected TimerManager TimerManager {
        get {
            if (m_TimerMgr == null) {
                m_TimerMgr = facade.GetManager<TimerManager>(ManagerName.Timer);
            }
            return m_TimerMgr;
        }
    }

    protected ThreadManager ThreadManager {
        get {
            if (m_ThreadMgr == null) {
                m_ThreadMgr = facade.GetManager<ThreadManager>(ManagerName.Thread);
            }
            return m_ThreadMgr;
        }
    }

    protected ObjectPoolManager ObjPoolManager {
        get {
            if (m_ObjectPoolMgr == null) {
                m_ObjectPoolMgr = facade.GetManager<ObjectPoolManager>(ManagerName.ObjectPool);
            }
            return m_ObjectPoolMgr;
        }
    }

    protected GameSceneMgr GameSceneManager
    {
        get
        {
            if(m_GameSceneMgr == null)
                m_GameSceneMgr = facade.GetManager<GameSceneMgr>(ManagerName.GameScene);
            return m_GameSceneMgr;
        }
    }

    protected DataMgr DataMgr
    {
        get
        {
            if (m_DataMgr == null)
            {
                m_DataMgr = facade.GetManager<DataMgr>(ManagerName.Data);
            }
            return m_DataMgr;
        }
    }
}
