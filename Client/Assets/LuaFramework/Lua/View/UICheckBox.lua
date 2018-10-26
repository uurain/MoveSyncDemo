---
--- Created by Administrator.
--- DateTime: 2017/8/9 16:06
---
local UICheckBox = class(require("View.basePanel"))

function UICheckBox:init()

    self.super.init(self)
    self.super.CreateModalLayer(self, Color(0,0,0,0.7))

    self.typeControl = self.view:GetController("type")
    self.view:GetChild("btnOk").onClick:Add(self.OnOk,self)
    self.view:GetChild("btnCancel").onClick:Add(self.OnCancel,self)
    self.Tip1 = self.view:GetChild("tip1")
end

function UICheckBox:UpdateUI(tip1, showType, event, eventCancel)
    self.Tip1.text = tip1
    self.typeControl.selectedIndex = showType
    self.event = event
    self.eventCancel = eventCancel
end

function UICheckBox:OnOk()
    if self.event ~= nil then
        self.event()
    end
    self:hide()
end

function UICheckBox:OnCancel()
    if self.eventCancel ~= nil then
        self.eventCancel()
    end
    self:hide()
end

return UICheckBox