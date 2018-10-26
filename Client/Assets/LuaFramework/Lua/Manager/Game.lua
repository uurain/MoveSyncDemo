
-- local json = require "cjson"
-- local util = require "3rd/cjson/util"

require "Table/Language"
require "Table/DataTable"

require "Manager/UIManager"
require "Manager/LogicManager"
require "Manager/IconManager"
require "Manager/CheckBoxMgr"
require "Manager/TimeMgr"
require "Manager/DisplayObjectMgr"
require "Manager/PreloadMgr"
require "Manager/ConstMgr"

require "Common/functions"
require "Common/FairyGUI"
require "Common/bit"

-- require "Logic/PgEventDispatch"





--管理器--
Game = {};
local this = Game;

local game; 
local transform;
local gameObject;
-- local WWW = UnityEngine.WWW;


function Game.PreLoadFile()
    for i, v in ipairs(preLoadData) do
        package.loaded[v] = nil
        require(v)
    end
end

--初始化完成，发送链接服务器信息--
function Game.OnInitOK()
    AppConst.SocketPort = 2012;
    AppConst.SocketAddress = "127.0.0.1";
    -- networkMgr:SendConnect();

    -- --注册LuaView--

    this.PreLoadFile()

    -- this.test_class_func();
    -- this.test_pblua_func();
    -- this.test_cjson_func();
    -- this.test_pbc_func();
    -- this.test_lpeg_func();
    -- this.test_sproto_func();
    -- coroutine.start(this.test_coroutine);
    -- staticMgr:InitData();
    UIManager.Init();
    LogicManager.Init();
    IconManager.Init();
    PreloadMgr.Init()

    UIManager.ShowPanel(UIPanelType.Login)
    logWarn('LuaFramework InitOK-11111111111111111-->>>');
end

--销毁--
function Game.OnDestroy()
    --logWarn('OnDestroy--->>>');
end
