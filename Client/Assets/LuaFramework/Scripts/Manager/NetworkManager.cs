using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using NFSDK;
using System.IO;
using ProtoBuf;
using pg;

namespace LuaFramework
{
    public class NetworkManager : Manager
    {

        static readonly object m_lockObject = new object();
        static Queue<KeyValuePair<int, LuaByteBuffer>> mEvents = new Queue<KeyValuePair<int, LuaByteBuffer>>();

        public static void AddEvent(int _event, LuaByteBuffer data)
        {
            lock (m_lockObject)
            {
                mEvents.Enqueue(new KeyValuePair<int, LuaByteBuffer>(_event, data));
            }
        }

        public static NetworkManager Instance()
        {
            return AppFacade.Instance.GetManager<NetworkManager>(ManagerName.Network);
        }

        bool mBeConnect = false;
        LuaFunction mLuaTable = null;

        void Awake()
        {
            Init();

            //AddReceiveCallBack(1, OnSkillObjectX);
        }

        public void OnSkillObjectX(UInt16 id, MemoryStream stream)
        {
            msg.AckLogin xData = Serializer.Deserialize<msg.AckLogin>(stream);
            Debug.Log("acklogin:" + xData.ErrorCode);
        }

        void Update()
        {
            NFCNet.Instance().doUpdate();
        }

        void OnGUI()
        {
            //if(GUI.Button(new Rect(Screen.width/2, Screen.height/2, 100, 100), "test1"))
            //{
            //    ConnectServer("127.0.0.1", 3563);
            //}

            //if (GUI.Button(new Rect(Screen.width / 2, Screen.height / 2+120, 100, 100), "test1"))
            //{
            //    msg.ReqLogin req = new msg.ReqLogin();
            //    req.UseId = 100;
            //    MemoryStream stream = new MemoryStream();
            //    Serializer.Serialize<msg.ReqLogin>(stream, req);
            //    SendMsgNet(0, stream);
            //}
        }

        void Init()
        {
            NFCNet.Instance().OnParseMsg = ParseMsg;
            NFCNet.Instance().OnDisConnected += DisConnected;
            NFCNet.Instance().OnConnected += Connected;
        }

        public void OnInit()
        {
            CallMethod("Start");
        }

        public void Unload()
        {
            CallMethod("Unload");
        }

        /// <summary>
        /// 执行Lua方法
        /// </summary>
        public object[] CallMethod(string func, params object[] args)
        {
            return Util.CallMethod("Network", func, args);
        }

        public void ConnectServer(string ip, UInt16 port)
        {
            ShutDown();

            //if (ip == "127.0.0.1" && port != 14001)
            //{
            //    ip = NFCNet.Instance().ip;
            //}

            NFCNet.Instance().ready(ip, port);
            NFCNet.Instance().connect();
        }

        public void ShutDown()
        {
            if (NFCNet.Instance().isConnected())
                NFCNet.Instance().shutDown();
        }

        public void AddReceiveCallBack(UInt16 id, NFCMessageDispatcher.MessageHandler netHandler)
        {
            NFCNetDispatcher.Instance().AddReceiveCallBack((UInt16)id, netHandler);
        }

        private void SendByPb(int unMsgID, byte[] datas)
        {
            MemoryStream pack = new MemoryStream();

            BinaryWriter writer = new BinaryWriter(pack);
            UInt32 msgLen = (UInt32)datas.Length + (UInt32)ConstDefine.NF_PACKET_HEAD_SIZE;
            writer.Write(NFCNet.ConvertUint16((UInt16)msgLen));
            writer.Write(NFCNet.ConvertUint16((UInt16)unMsgID));            
            writer.Write(datas);
            NFCNet.Instance().sendMsg(pack);
        }

        public void SendMsg(int unMsgID, LuaByteBuffer buffer)
        {
            byte[] datas = buffer.buffer;
            SendByPb(unMsgID, datas);
        }

        public void SendMsgNet(UInt16 unMsgID, MemoryStream stream)
        {
            SendByPb((int)unMsgID, stream.ToArray());
        }

        public void ParseMsg(UInt16 msgId, MemoryStream stream)
        {
            //Debug.Log("ParseMsg:" + msgId);
            if (!NFCNetDispatcher.Instance().dispatchMessage(msgId, stream))
            {
                //var xMsg = Serializer.Deserialize<NFMsg.MsgBase>(stream);
                byte[] byteArray = new byte[stream.Length];
                stream.Read(byteArray, 0, byteArray.Length);
                //AddEvent(msgId, luaByteBuf);

                OnLuaDispatch(msgId, byteArray);
                //ProtoFactory.Recycle(xMsg);
            }
        }


        public void DisConnected(object sender, DisConnectedEventArgs e)
        {
            Debug.Log("DisConnected:"+e.eErrorCode);
            Util.CallMethod("Network", "OnNetError", (int)e.eErrorCode);
        }

        public void Connected(object sender, ConnectedEventArgs e)
        {
            Debug.Log("Connected!");
            Util.CallMethod("Network", "OnConnected");
        }

        void OnLuaDispatch(int msgId, byte[] byteArray)
        {
            if (mLuaTable == null)
            {
                mLuaTable = Util.GetFunction("Network", "OnSocket");
            }
            if (byteArray != null)
                mLuaTable.Call(msgId, new LuaByteBuffer(byteArray));
            else
                mLuaTable.Call(msgId);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        new void OnDestroy()
        {
            //SocketClient.OnRemove();
            Debug.Log("~NetworkManager was destroy");
            if (mLuaTable != null)
                mLuaTable.Dispose();
            NFCNet.Instance().shutDown();
        }
    }
}