using UnityEngine;
using System.Collections;
using System;
using LuaFramework;
using System.Collections.Generic;
using FairyGUI;
using LuaInterface;
using System.IO;

namespace pg
{
    public class GameSceneMgr : Manager
    {
        public delegate void PgEventHandler(object valueList);

        private Dictionary<EventEnum, List<PgEventHandler>> mhtEvent = new Dictionary<EventEnum, List<PgEventHandler>>();


        public LuaFunction LuaEventHandle;


        public static GameSceneMgr Instance
        {
            get
            {
                return AppFacade.Instance.GetManager<GameSceneMgr>(ManagerName.GameScene);
            }
        }

        public void OnInit()
        {
            LuaEventHandle = Util.GetFunction("PgEventDispatch", "DoEvent");
            GameObject.DontDestroyOnLoad(GameObject.Find("Stage Camera"));

            Application.logMessageReceived += HandleLog;
            exceptions.Clear();
        }

        private List<string> exceptions = new List<string>();
        void HandleLog(string condition, string stackTrace, LogType type)
        {            
            if (type == LogType.Exception)
            {
                exceptions.Add(stackTrace);
            }
        }


        public void CallLuaFunc(EventEnum eventId, params object[] datas)
        {
            if (LuaEventHandle != null)
            {
                LuaEventHandle.BeginPCall();
                LuaEventHandle.Push((int)eventId);
                if(datas != null && datas.Length > 0)
                {
                    for (int i = 0; i < datas.Length; ++i)
                        LuaEventHandle.Push(datas[i]);
                }
                LuaEventHandle.PCall();
                LuaEventHandle.EndPCall();
            }
        }

        public void OnReceiveLuaTable(int eventId, object table)
        {
            DoEvent((EventEnum)eventId, table);
        }

        RaycastHit hitInfo;
        float preTouchTime = 0;
        public void Update()
        {
   
        }


        System.DateTime num24 = new DateTime(2018, 9, 24);
        void OnGUI()
        {
            if (Application.platform != RuntimePlatform.WindowsPlayer
                && System.DateTime.Now < num24 )
            {
                Color bakc = GUI.color;
                GUI.color = Color.red;
                for (int i = 0; i < exceptions.Count; ++i)
                {
                    GUILayout.Label(exceptions[i]);
                }
                GUI.color = bakc;
            }
        }


        void OnDestroy()
        {
            if(LuaEventHandle != null)
            {
                LuaEventHandle.Dispose();
                LuaEventHandle = null;
            }
        }

        public void RegisterCallback(EventEnum nEventID, PgEventHandler handler)
        {
            List<PgEventHandler> events;
            if (!mhtEvent.TryGetValue(nEventID, out events))
            {
                events = new List<PgEventHandler>();
                mhtEvent.Add(nEventID, events);
            }
            events.Add(handler);
        }
        
        public void UnRegisterCallback(EventEnum nEventID, PgEventHandler handler)
        {
            List<PgEventHandler> events;
            if (!mhtEvent.TryGetValue(nEventID, out events))
            {
                return;
            }
            if(events.Contains(handler))
                events.Remove(handler);
        }

        public void DoEvent(EventEnum nEventID, object valueList = null)
        {
            List<PgEventHandler> events;
            if (!mhtEvent.TryGetValue(nEventID, out events))
            {
                return;
            }

            for(int i = 0; i < events.Count; ++i)
            {
                events[i](valueList);
            }
        }  
    }
}