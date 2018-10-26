
CtrlNames = {
	Prompt = "PromptCtrl",
	Message = "MessageCtrl"
}

PanelNames = {
	"PromptPanel",	
	"MessagePanel",
}

LogicType = {
	Battle = "LogicBattle",
}

EBattleType = {
	none = 0,waitPvp = 1,pvp = 2,dugneon = 3,
}

--场景状态
ESceneState =
{
    None = 0,
    Enter = 1,
    Start = 2,
    Over = 3,
}

ModelType = {
	All = 0,
	Model = 1,
	Weapon = 2,
	ModelController = 3,
	Wing = 4,
	Horse = 5,
}

-- 游戏逻辑帧 1秒10帧
GAME_FRAME = 0.1
GAME_FPS = 10


--协议类型--
ProtocalType = {
	BINARY = 0,
	PB_LUA = 1,
	PBC = 2,
	SPROTO = 3,
}
--当前使用的协议类型--
TestProtoType = ProtocalType.BINARY;

AppConst = AppConst;
Util = LuaFramework.Util;
LuaHelper = LuaFramework.LuaHelper;
ByteBuffer = LuaFramework.ByteBuffer;


resMgr = LuaHelper.GetResManager();
-- resMgr2 = LuaHelper.GetResManager2();
panelMgr = LuaHelper.GetPanelManager();
soundMgr = LuaHelper.GetSoundManager();
networkMgr = LuaHelper.GetNetManager();
saveDataMgr = LuaHelper.GetSaveDataManager();
gameSceneMgr = LuaHelper.GetGameSceneManager();




WWW = UnityEngine.WWW;
--Time = UnityEngine.Time
GameObject = UnityEngine.GameObject;
Physics	= UnityEngine.Physics
Application = UnityEngine.Application

function Res_LoadGameObject(path, cb)
	resMgr:LoadGameObject(path, cb)
end

function Res_LoadAbInfo(path, cb)
	resMgr:Load(path, cb)
end

function Res_UnloadAssetObj(ao)
	resMgr:Unload(ao)
end