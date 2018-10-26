using UnityEngine;
using System.Collections;
using LuaInterface;
using System.Collections.Generic;

namespace LuaFramework {
    public class LuaManager : Manager {
        private LuaState lua;
        private LuaLoader loader;
        private LuaLooper loop = null;

        LuaFunction updateFunc = null;
        LuaFunction lateUpdateFunc = null;
        LuaFunction fixedUpdateFunc = null;
        LuaFunction levelLoaded = null;

        Dictionary<string, LuaTable> dataTableDic = new Dictionary<string, LuaTable>();

        // Use this for initialization
        void Awake() {
            loader = new LuaLoader();
            lua = new LuaState();
            this.OpenLibs();
            lua.LuaSetTop(0);

            LuaBinder.Bind(lua);
            DelegateFactory.Init();
            LuaCoroutine.Register(lua, this);
        }

        public void InitStart() {
            InitLuaPath();
            InitLuaBundle();
            this.lua.Start();    //启动LUAVM
            this.StartMain();
            this.StartLooper();
        }

        void StartLooper() {
            loop = gameObject.AddComponent<LuaLooper>();
            loop.luaState = lua;
        }

        //cjson 比较特殊，只new了一个table，没有注册库，这里注册一下
        protected void OpenCJson() {
            lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
            lua.OpenLibs(LuaDLL.luaopen_cjson);
            lua.LuaSetField(-2, "cjson");

            lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
            lua.LuaSetField(-2, "cjson.safe");
        }

        void StartMain() {
            lua.DoFile("Main.lua");

            LuaFunction main = lua.GetFunction("Main");
            main.Call();
            main.Dispose();
            main = null;

            updateFunc = lua.GetFunction("Update");
            lateUpdateFunc = lua.GetFunction("LateUpdate");
            fixedUpdateFunc = lua.GetFunction("FixedUpdate");
            levelLoaded = lua.GetFunction("OnLevelWasLoaded");
        }
        
        /// <summary>
        /// 初始化加载第三方库
        /// </summary>
        void OpenLibs() {
            lua.OpenLibs(LuaDLL.luaopen_pb);      
            lua.OpenLibs(LuaDLL.luaopen_lpeg);
            lua.OpenLibs(LuaDLL.luaopen_bit);
            //lua.OpenLibs(LuaDLL.luaopen_socket_core);

            //this.OpenCJson();
        }

        /// <summary>
        /// 初始化Lua代码加载路径
        /// </summary>
        void InitLuaPath() {
            if (AppConst.DebugMode) {
                string rootPath = AppConst.FrameworkRoot;
                lua.AddSearchPath(rootPath + "/Lua");
                lua.AddSearchPath(rootPath + "/ToLua/Lua");
            } else {
                lua.AddSearchPath(Util.DataPath + "lua");
            }
        }

        /// <summary>
        /// 初始化LuaBundle
        /// </summary>
        void InitLuaBundle() {
            if (loader.beZip) {
                loader.AddBundle("lua/lua.unity3d");
                loader.AddBundle("lua/lua_math.unity3d");
                loader.AddBundle("lua/lua_system.unity3d");
                loader.AddBundle("lua/lua_system_reflection.unity3d");
                loader.AddBundle("lua/lua_unityengine.unity3d");
                loader.AddBundle("lua/lua_common.unity3d");
                loader.AddBundle("lua/lua_logic.unity3d");
                loader.AddBundle("lua/lua_view.unity3d");
                loader.AddBundle("lua/lua_controller.unity3d");
                loader.AddBundle("lua/lua_misc.unity3d");

                loader.AddBundle("lua/lua_manager.unity3d");

                loader.AddBundle("lua/lua_protobuf.unity3d");
                loader.AddBundle("lua/lua_3rd_cjson.unity3d");
                loader.AddBundle("lua/lua_3rd_luabitop.unity3d");
                loader.AddBundle("lua/lua_3rd_pbc.unity3d");
                loader.AddBundle("lua/lua_3rd_pblua.unity3d");
                loader.AddBundle("lua/lua_3rd_sproto.unity3d");
            }
        }

        public void DoFile(string filename) {
            lua.DoFile(filename);
        }

        // Update is called once per frame
        public object[] CallFunction(string funcName, params object[] args) {
            LuaFunction func = lua.GetFunction(funcName);
            if (func != null) {
                return func.LazyCall(args);
            }
            return null;
        }

        public LuaFunction GetFunction(string funcName)
        {
            LuaFunction func = lua.GetFunction(funcName);
            return func;
        }

        public void LuaGC() {
            lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
        }

        public void Close() {

            foreach(var val in dataTableDic)
            {
                val.Value.Dispose();
            }

            SafeRelease(ref updateFunc);
            SafeRelease(ref lateUpdateFunc);
            SafeRelease(ref fixedUpdateFunc);
            SafeRelease(ref levelLoaded);

            if (loop != null)
            {
                loop.Destroy();
                loop = null;
            }

            if (lua != null)
            {
                lua.Dispose();
                lua = null;
                loader = null;
            }            
        }

        public void Update()
        {
            if (updateFunc != null)
            {
                // 这样没gc
                updateFunc.BeginPCall();
                updateFunc.Push(Time.deltaTime);
                updateFunc.Push(Time.unscaledDeltaTime);
                updateFunc.PCall();
                updateFunc.EndPCall();
            }
        }

        public void LateUpate()
        {
            if (lateUpdateFunc != null)
            {
                lateUpdateFunc.Call();
            }
        }

        public void FixedUpdate()
        {
            if (fixedUpdateFunc != null)
            {
                fixedUpdateFunc.Call(Time.fixedDeltaTime);
            }
        }

        public void OnLevelLoaded(int level)
        {
            if(levelLoaded != null)
                levelLoaded.Call(level);
        }

        public LuaTable GetStaticDataTable(string tableName)
        {
            LuaTable luaTable = null;
            if(dataTableDic.TryGetValue(tableName, out luaTable))
            {
                return luaTable;
            }
            luaTable = lua.GetTable(tableName);
            dataTableDic[tableName] = luaTable;
            return luaTable;
        }

        void SafeRelease(ref LuaFunction luaRef)
        {
            if (luaRef != null)
            {
                luaRef.Dispose();
                luaRef = null;
            }
        }
    }
}