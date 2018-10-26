local GameMgrLogic = class(require("Logic.BaseLogic"))

function GameMgrLogic:Init()
	self.actorTable = {}
	self.localPlayer = nil

	Event.AddListener(Msg.Id.AckPlayerEntryList, handler(self.OnAckPlayerEntryList, self))
	Event.AddListener(Msg.Id.AckPlayerLeaveList, handler(self.OnAckPlayerLeaveList, self))
	Event.AddListener(Msg.Id.AckPublicPropertyList, handler(self.OnAckPublicPropertyList, self))
	Event.AddListener(Msg.Id.AckSwapScene, handler(self.OnAckSwapScene, self))
	Event.AddListener(Msg.Id.ReqAckStop, handler(self.OnReqAckStop, self))
	Event.AddListener(Msg.Id.ReqAckPlayerMove, handler(self.OnReqAckPlayerMove, self))
	Event.AddListener(Msg.Id.AckSkillStart, handler(self.OnAckSkillStart, self))

	PgEventDispatch.RegisterCallback(PgEventID.SceneClick, handler(self.OnSceneClick, self))

    self.updateHandle = UpdateBeat:CreateListener(self.Update, self)
    UpdateBeat:AddListener(self.updateHandle)
end

function GameMgrLogic:LocalTest()
	local testTable = {}
	testTable.object_guid = 100
	testTable.x = 0
	testTable.y = 0
	testTable.z = 0
	testTable.r = 0
	testTable.player_info = {}
	testTable.player_info.moveSpeed = 10
	testTable.player_info.atk = 10
	testTable.player_info.def = 10
	testTable.player_info.hp = 100
	testTable.player_info.hpMax = 100
	testTable.player_info.name = "test"

	local actor = require("Logic.LocalPlayer").new()
	self.localPlayer = actor
	actor:Init()

	local tCompt = actor:AddCompt(ComponentType.transform)
	local tData = {}
	tData.pos = Vector3.New(testTable.x, testTable.y, testTable.z)
	tData.dir = testTable.r
	tCompt:SetData(tData)

	actor:InitUnitData(testTable.player_info)

	self.actorTable[testTable.object_guid] = actor

	UIManager.ShowPanel(UIPanelType.Loading)
	resMgr:LoadScene("scene1", function ()
		UIManager.ShowPanel(UIPanelType.Battle)
		UIManager.HidePanel(UIPanelType.Loading)
	end)
end

function GameMgrLogic:OnAckPlayerEntryList(msg)
	for i,v in ipairs(msg.object_list) do
		local actor = nil
		log("v.object_guid:"..v.object_guid..";"..ConstMgr.uid)
		if v.object_guid == ConstMgr.uid then
			actor = require("Logic.LocalPlayer").new()
			self.localPlayer = actor
		else
			actor = require("Logic.ActorLogic").new()
		end
		actor:Init()

		local tCompt = actor:AddCompt(ComponentType.transform)
		local tData = {}
		tData.pos = Vector3.New(v.x, v.y, v.z)
		tData.dir = v.r
		tCompt:SetData(tData)

		actor:InitUnitData(v.player_info)

		self.actorTable[v.object_guid] = actor
	end
end

function GameMgrLogic:OnAckPlayerLeaveList(msg)
	for i,v in ipairs(msg.object_list) do
		local actor = self.actorTable[v]
		if actor ~= nil then
			actor:Dispose()
			self.actorTable[v] = nil 
		end
	end
end

function GameMgrLogic:OnAckPublicPropertyList(msg)
	-- body
end

function GameMgrLogic:OnAckSwapScene(msg)
	if self.localPlayer ~= nil then
		self.localPlayer:SwapScene(msg)
	end

	-- UIManager.ShowPanel(UIPanelType.Loading)
	resMgr:LoadScene("scene1", function ()	
		UIManager.ShowPanel(UIPanelType.Battle)
		-- UIManager.HidePanel(UIPanelType.Loading)
	end)
end

function GameMgrLogic:OnReqAckStop(msg)
	log("OnReqAckStop:"..msg.mover)
	local actor = self.actorTable[msg.mover]
	if actor then
		actor:SetMoveStop(Vector3.New(msg.source_pos.x, msg.source_pos.y, msg.source_pos.z))
	end
end

function GameMgrLogic:OnReqAckPlayerMove(msg)
	log("OnReqAckPlayerMove:"..msg.mover)
	-- for i,v in ipairs(msg.target_pos) do
	-- 	log("move"..i..":"..v.x..";"..v.y..";"..v.z)
	-- end
	if msg.mover ~= ConstMgr.uid then
		local actor = self.actorTable[msg.mover]
		if actor then
			actor:SetMoveTarget(msg.target_pos)
		end
	end
end

function GameMgrLogic:OnAckSkillStart(msg)
	log("OnAckSkillStart:"..msg.caster)
	local actor = self.actorTable[msg.caster]
	if actor then
		actor:PlayAni("attack01")
	end
end

function GameMgrLogic:OnSceneClick(hitPos )
	if self.localPlayer ~= nil then
		self.localPlayer:SetNavTarget(hitPos)
	end
end

function GameMgrLogic:InputSkill(skillId)
	if self.localPlayer ~= nil then
		self.localPlayer:LocalStop()
		Msg_ReqStop(ConstMgr.uid, self.localPlayer.transCompt:GetPos())
		Msg_ReqUseSkill(skillId)
	end
end

function GameMgrLogic:Update()
	for k,v in pairs(self.actorTable) do
		v:Update(Time.deltaTime)
	end
end

function GameMgrLogic:Dispose()
	if self.updateHandle ~= nil then
        UpdateBeat:RemoveListener(self.updateHandle)
        self.updateHandle = nil
	end

	self.super.Dispose(self)
end

return GameMgrLogic