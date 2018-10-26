local LocalPlayer = class(require("Logic.ActorLogic"))

function LocalPlayer:Init()
	self.super.Init(self)

	self.sceneId = 0
end

function LocalPlayer:InitUnitData(info)
	self.super.InitUnitData(self, info)

	PgJoystick.GetInstance():SetCamLookAt(self.displayCompt:GetRootNode().transform);
end

function LocalPlayer:SwapScene(info)
	self.sceneId = info.scene_id

	local tCompt = self:GetCompt(ComponentType.transform)
	local tData = {}
	tData.pos = Vector3.New(info.x, info.y, info.z)
	tData.dir = info.r
	tCompt:SetData(tData)
end

function LocalPlayer:SetNavTarget(tPos)
	local navPath = AppConst.GetMapNav(self.transCompt:GetPos(), tPos)
	if navPath ~= nil then
		local posTable = {}
        for i = 0, navPath.Count - 1 do
            table.insert(posTable,navPath[i])
        end
        Msg_ReqMove(ConstMgr.uid, posTable)
		self:SetMoveTarget(posTable)		
	end
end


return LocalPlayer