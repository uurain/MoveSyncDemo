﻿using UnityEngine;
using System.Collections;
using LuaFramework;
using pg;

public class StartUpCommand : ControllerCommand {

    public override void Execute(IMessage message) {
        if (!Util.CheckEnvironment()) return;

        GameObject gameMgr = GameObject.Find("GlobalGenerator");
        if (gameMgr != null) {
            AppView appView = gameMgr.AddComponent<AppView>();
        }
        //-----------------关联命令-----------------------
        AppFacade.Instance.RegisterCommand(NotiConst.DISPATCH_MESSAGE, typeof(SocketCommand));

        //-----------------初始化管理器-----------------------
        AppFacade.Instance.AddManager<LuaManager>(ManagerName.Lua);
        AppFacade.Instance.AddManager<PanelManager>(ManagerName.Panel);
        AppFacade.Instance.AddManager<SoundManager>(ManagerName.Sound);
        AppFacade.Instance.AddManager<TimerManager>(ManagerName.Timer);
        AppFacade.Instance.AddManager<NetworkManager>(ManagerName.Network);
        AppFacade.Instance.AddManager<ResManager>(ManagerName.Resource);
        AppFacade.Instance.AddManager<ThreadManager>(ManagerName.Thread);
        AppFacade.Instance.AddManager<ObjectPoolManager>(ManagerName.ObjectPool);
        AppFacade.Instance.AddManager<SaveDataManager>(ManagerName.SaveData);
        AppFacade.Instance.AddManager<GameSceneMgr>(ManagerName.GameScene);
        //AppFacade.Instance.AddManager<UpdateManager>(ManagerName.Update);
        AppFacade.Instance.AddManager<DownloadManager>(ManagerName.Download);
        AppFacade.Instance.AddManager<DataMgr>(ManagerName.Data);
        AppFacade.Instance.AddManager<GameManager>(ManagerName.Game);        
    }
}