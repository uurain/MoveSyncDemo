local BasePanel = class("BasePanel")

function BasePanel:ctor(pType)
	self.panelType = pType
	self.view = nil
	self.transform = nil
	self.panelState = UIPanelState.Loading
	self.moneyBar = 0
    self.hideBattle = 0
	self.modalLayer = nil
	self.blueIndex = -1
	-- log("PanelName:  "..self.panelType)
	-- To overwrite....
end

function BasePanel:Awake(obj)
	-- self.gameObject = obj
	-- self.transform = obj.transform
	self.view = obj
	self:init()
	-- logWarn("Awake lua--->>"..self.panelType.name)
end

function BasePanel:show(...)
	if self.view ~= nil then
		if self.panelType and self.panelType.uType == UIType.window then
			UIManager.ShowWindow(self.view)
		else
			self.view.visible = true
		end		
		if self.moneyBar == 1 then
			UIManager.ShowPanel(UIPanelType.MoneyBar)
		end
        if self.hideBattle == 1 then
            UIManager.GetPanel(UIPanelType.Battle.name):SetVisible(false)
        end
	end
	self:ShowBlur()
end

function BasePanel:hide()
	if self.view ~= nil then
		if self.panelType and self.panelType.uType == UIType.window then
			UIManager.HideWindow(self.view)
		else
			self.view.visible = false
		end
		if self.moneyBar == 1 then
			UIManager.HidePanel(UIPanelType.MoneyBar)
		end
        if self.hideBattle == 1 then
            UIManager.GetPanel(UIPanelType.Battle.name):SetVisible(true)
        end
	end
    if self.panelType ~= nil and self.panelType.uType ~= UIType.tip then
        UIManager.ShowModalWait()
        TimeMgr.DelayDo(function()
            UIManager. HideModalWait()
        end, 0.5)
    end

    self:HideBlur()
end

function BasePanel:ShowBlur()
	if self.blueIndex == 0 then
		self.blueIndex = self.blueIndex + 1
		PgEventDispatch.CallPGEvent(PgEventID.MainSceneBlurEffect, true)
	end
end

function BasePanel:HideBlur()
	if self.blueIndex > 0 then
		self.blueIndex = self.blueIndex-1
		PgEventDispatch.CallPGEvent(PgEventID.MainSceneBlurEffect, false)
	end
end

function BasePanel:CreateModalLayer(color)
	self.modalLayer = UIPackage.CreateObject("Common", "FullScreenBtn")
	if color ~= nil then
		self.modalLayer:GetChild("bg").color = color		
	else
		self.modalLayer:GetChild("bg").color = Color.New(0,0,0,0)
		self.blueIndex = 0
	end	
	self.modalLayer:SetSize(GRoot.inst.width, GRoot.inst.height)
	self.modalLayer:AddRelation(GRoot.inst, RelationType.Size)
	self.modalLayer.x = -self.view.x
	self.modalLayer.y = -self.view.y
	self.modalLayer.onClick:Add(self.EmptyOnClick, self)
	self.view:AddChildAt(self.modalLayer, 0)	
end

function BasePanel:EmptyOnClick()

end

function BasePanel:init()

end

function BasePanel:OnDestroy()
	self:hide()
	if self.view ~= nil then
		self.view:Dispose()
		self.view = nil
	end
	if self.panelState == UIPanelState.Loading then
		self.panelState = UIPanelState.NeedDestory
	end
end

return BasePanel