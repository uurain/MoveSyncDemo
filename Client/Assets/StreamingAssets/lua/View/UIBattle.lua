local UIBattle = class(require("View.BasePanel"))

function UIBattle:init()
	self.super:init()

	self.btnSkill1 = self.view:GetChild("btnSkill1")
	self.btnSkill1.onClick:Add(self.OnClickSkill, self)

	self.gameMgrLogic = LogicManager.Get(LogicType.GameMgr)
end

function UIBattle:OnClickSkill()
	self.gameMgrLogic:InputSkill(1)
end

return UIBattle