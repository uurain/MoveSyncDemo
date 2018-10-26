local TransformCompt = class(require("Logic.Component.BaseCompt"))

function TransformCompt:ctor()
	self.pos = Vector3.zero
	self.dir = 0
	self.lookPos = Vector3.zero
end

function TransformCompt:SetData(dataTable)
	self.pos = dataTable.pos
	self.dir = dataTable.dir
end

function TransformCompt:SetPos(pos)
	self.pos = pos
end

function TransformCompt:GetPos()
	return self.pos
end

function TransformCompt:SetLookPos(pos)
	self.lookPos = pos
end

function TransformCompt:GetLookPos()
	return self.lookPos
end

function TransformCompt:AddPos(x,y,z)
	self.pos.x = self.pos.x + x
	self.pos.y = self.pos.y + y
	self.pos.z = self.pos.z + z
end

return TransformCompt