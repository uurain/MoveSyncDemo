local UILoading = class(require("View.BasePanel"))

function UILoading:ctor()	
	self.slider = nil
	self.loadingType = UILoadingType.battle
	self.beginLoading = false
end

function UILoading:init()
	self.super:init()
	self.slider = self.view:GetChild("pb")
	self:SetPercent(0)
	self.showBattle = false
	self.UpdateHandle = handler(self, self.Update)

	-- local bgGloader = self.view:GetChild("n2")
	-- bgGloader.width = GRoot.inst.width
	-- bgGloader.height = GRoot.inst.height
	-- bgGloader.url = "UI/BigPic/bg1.jpg"
end

function UILoading:show()
    self:SetPercent(0)

	self.super.show(self)
	self.showBattle = false
	UpdateBeat:Add(self.UpdateHandle)
end

function UILoading:SetLoadingType(type)
	self.loadingType = type
	if self.loadingType == UILoadingType.LuaLoad then
		self.beginLoading = false
	end

	logError("type:"..type)
end

function UILoading:BeginLoading()
	logError("beginLoading")
	self.beginLoading = true
end

function UILoading:hide()
	self.super.hide(self)
	UpdateBeat:Remove(self.UpdateHandle)
	self:SetPercent(0)
end

function UILoading:Update()
	-- if self.loadingType == UILoadingType.battle then
	-- 	self:SetPercent(battleResPool:GetLoadProcess())
	-- elseif self.loadingType == UILoadingType.LuaLoad then
	-- 	if self.beginLoading then
	-- 		self:SetPercent(PreloadMgr.GetLoadProcess())
	-- 	end
	-- end
end

function UILoading:SetPercent( val)
	if self.slider ~= nil then
		self.slider.value = val * 100
	end

	-- log(val)

	if val > 0.999 then		
		if self.showBattle == false then
			self.showBattle = true
			self:hide()
		end
	end
end

function UILoading:OnDestroy()
	self.super:OnDestroy()
	self.slider = nil
end

return UILoading