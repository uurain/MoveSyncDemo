local UIHeadBar = class(require("View.basePanel"))

function UIHeadBar:init()
    self.super.init(self)

    self.txtName = self.view:GetChild("name")
    self.bloodBar = self.view:GetChild("blood")
end

function UIHeadBar:SetName(val)
    self.txtName.text = val
end

function UIHeadBar:SetBloodVal(val)
    self.bloodBar.value = val * 100
end

function UIHeadBar:SetPos(pos)
    self.view.x = pos.x
    self.view.y = pos.y
end

return UIHeadBar