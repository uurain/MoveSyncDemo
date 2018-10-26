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
	self.unit.atk = info.atk
	self.unit.def = info.def
	self.unit.hp = info.hp
	self.unit.hpMax = info.hpMax
	self.unit.moveSpeed = info.moveSpeed
	self.unit.name = info.name

	self.displayCompt = self:AddCompt(ComponentType.displayActorCompt)
	self.displayCompt:SetWorldModel()
	self.displayCompt:CreateClothes(1001)

	self.transCompt = self:GetCompt(ComponentType.transform)
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

function ActorLogic:SetMoveTarget(tPos)
	self.isTargetPos = true
	self.moveTargetPos = tPos
end

function ActorLogic:SetMoveSpeed(val)
	self.unit.moveSpeed = val
end

function ActorLogic:SetMoveStop(tPos)
	
end

function ActorLogic:Update(timeDelta)
	if self.isTargetPos then
		local curPos = self.transCompt:GetPos()
		local tPos = self.moveTargetPos

		local dx = tPos.x - curPos.x
		local dz = tPos.z - curPos.z
		local distance = Mathf.Sqrt(dx * dx + dz * dz)
		if distance < 0.001 then
			self.isTargetPos = false
		else
			local canMoveDist = self.unit.moveSpeed * timeDelta
			if canMoveDist < distance then
				dx = dx / distance * canMoveDist
				dz = dz / distance * canMoveDist
			end
			self.transCompt:AddPos(dx,0, dz)
			self:SyncDisplayPos()
		end
	end
end

function ActorLogic:SyncDisplayPos()
	if self.displayCompt ~= nil then
		self.displayCompt:SetLocalPos(self.transCompt:GetPos())
	end
end

function ActorLogic:Dispose()
	for k,v in pairs(self.comptTable) do
		v:Dispose()
	end
	self.comptTable = {}
end

return ActorLogic