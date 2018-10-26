
require "Common/define"
require "View/UIDefine"

LogicManager = {}
local this = LogicManager
local _LogicTable = {}

LogicType = {
	GameMgr = "GameMgr",
	SceneMgr = "SceneMgr",
}


function LogicManager.Init()
	local temp = {}
	table.insert(temp, UIPanelType.Login.name)

	for k,v in pairs(LogicType) do
		table.insert(temp, v)
	end

	for k,v in pairs(temp) do
		this.Get(v)
	end
end


function LogicManager.Get(logicName)
	local logic = _LogicTable[string.lower(logicName)]
	if logic == nil then
		logic = this.Create(logicName)
	end
	return logic
end

function LogicManager.Create(logicName)
	local logic = require("Logic."..logicName.."Logic").new()
	logic:Init()
	logic:Reset()
	this.Add(logicName, logic)
	return logic
end
function LogicManager.Add(logicName, logic)
	_LogicTable[string.lower(logicName)] = logic
end

function LogicManager.Reset()
	for k, v in pairs(_LogicTable) do
		v:Reset()
	end
end
