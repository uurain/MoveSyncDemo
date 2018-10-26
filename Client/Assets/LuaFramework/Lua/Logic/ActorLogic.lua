local ActorLogic = class("ActorLogic")

function ActorLogic:ctor()
	self.comptTable = {}
	self.unit = {}

	self.isTargetPos = false
	self.moveTargetPos = nil
end

function ActorLogic:Init()

end

function ActorLogic:InitUnitData( info )
	self.unit = {}
	self:UpdateUnit(info)

	self.displayCompt = self:AddCompt(ComponentType.displayActorCompt)
	self.displayCompt:SetWorldModel()
	self.displayCompt:CreateClothes(1001)

	self:PlayAni("standfight")

	self.transCompt = self:GetCompt(ComponentType.transform)

	self.headBar = UIManager.CreateHeadBar()
	self.headBar:SetName(self.unit.name)
	self.headBar:SetBloodVal(self.unit.hp/self.unit.hpMax)
end

function ActorLogic:GetCompt(comptType)
	local compt = self.comptTable[comptType]
	if not compt then
		logError("don't have compt:"..comptType)
	end
	return compt
end

function ActorLogic:AddCompt(comptType)
	if self.comptTable[comptType] ~= nil then
		logError("AddCompt haved! "..comptType)
		return nil
	end
	local compt = nil
	if comptType == ComponentType.transform then
		compt = require("Logic.Component.TransformCompt").new()
	elseif comptType == ComponentType.displayActorCompt then
		compt = require("Logic.Component.DisplayActorCompt").new()
	end
	compt:Init(self)
	self.comptTable[comptType] = compt
	return compt
end

function ActorLogic:LocalStop()
	self:SetMoveTarget({})
end

function ActorLogic:UpdateUnit(info)
	self.unit.atk = info.atk
	self.unit.def = info.def
	self.unit.hp = info.hp
	self.unit.hpMax = info.hpMax
	self.unit.moveSpeed = info.moveSpeed
	self.unit.name = info.name
end

function ActorLogic:SetMoveTarget(tPosTable)	
	self.moveTargetPosTable = tPosTable
	-- for i,v in ipairs(self.moveTargetPosTable) do
	-- 	print(i,v.x, v.z)
	-- end
	self:SetNextMovePos()
end

function ActorLogic:SetNextMovePos()
	local targetCount = #self.moveTargetPosTable
	if targetCount <= 0 then
		self.isTargetPos = false
		self.moveTargetPos = nil
		self:PlayAni("standfight")
		return false
	end
	self.moveTargetPos = self.moveTargetPosTable[1]
	table.remove(self.moveTargetPosTable, 1)
	self.isTargetPos = true
	log("self.moveTargetPos:"..self.moveTargetPos.x..";"..self.moveTargetPos.z)
	self:SetLookAt(self.moveTargetPos)
	self:PlayAni("walk")
end

function ActorLogic:SetMoveStop(val)
	local tPosTable = {}
	table.insert(tPosTable, val)
	self:SetMoveTarget(tPosTable)
end

function ActorLogic:SetMoveSpeed(val)
	self.unit.moveSpeed = val
end

function ActorLogic:SetLookAt(tPos)
	self.transCompt:SetLookPos(tPos)
	if self.displayCompt ~= nil then
		self.displayCompt:SetLookPos(tPos)
	end
end

function ActorLogic:OnBehurt(atkActor, damageInfo)
	log("OnBehurt:"..damageInfo.damage)
end

function ActorLogic:Update(timeDelta)
	self:SyncHeadBarPos()
	if self.isTargetPos then
		local curPos = self.transCompt:GetPos()
		local tPos = self.moveTargetPos

		local dx = tPos.x - curPos.x
		local dz = tPos.z - curPos.z
		local distance = Mathf.Sqrt(dx * dx + dz * dz)
		if distance < 0.001 then
			self:SetNextMovePos()
		else
			local canMoveDist = self.unit.moveSpeed * timeDelta
			if canMoveDist < distance then
				dx = dx / distance * canMoveDist
				dz = dz / distance * canMoveDist
			end
			self.transCompt:AddPos(dx, 0, dz)
			self:SyncDisplayPos()
		end
	end
end

function ActorLogic:SyncHeadBarPos()
	if self.headBar ~= nil then
		local worldPos = self.transCompt:GetPos()
		local tempPos = Vector3.New(worldPos.x, worldPos.y + 2, worldPos.z)
		local screenPos = UnityEngine.Camera.main:WorldToScreenPoint(tempPos)
		screenPos.y = UnityEngine.Screen.height - screenPos.y
		if screenPos.z < 0 then
			screenPos.x = 9999999
		end
		local uiPos = GRoot.inst:GlobalToLocal(Vector2.New(screenPos.x, screenPos.y))
		self.headBar:SetPos(uiPos)
		self.headBar:SetBloodVal(self.unit.hp / self.unit.hpMax)
	end
end

function ActorLogic:SyncDisplayPos()
	if self.displayCompt ~= nil then
		self.displayCompt:SetLocalPos(self.transCompt:GetPos())
	end
end

function ActorLogic:PlayAni(aniName)
	log("PlayAni:"..aniName)
	self.displayCompt:PlayAni(aniName)
end

function ActorLogic:Dispose()
	for k,v in pairs(self.comptTable) do
		v:Dispose()
	end
	self.comptTable = {}
end

return ActorLogic