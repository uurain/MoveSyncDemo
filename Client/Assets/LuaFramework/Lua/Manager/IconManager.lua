---
--- Created by Administrator.
--- DateTime: 2017/7/28 9:16
---

IconManager = {}
local this = IconManager
local RoleIcon

IconManager.TextMoneyIcon =
{

    Glod =  "<img src='ui://6u4a28edf29ag4j'  width = '40' height = '40' />",
    TradeMoney = "<img src='ui://6u4a28edf29ag4h'  width = '40' height = '40' />",
    bandingMoney = "<img src='ui://6u4a28edf29ag4k'  width = '40' height = '40' />",
    Money = "<img src='ui://6u4a28edf29ag4i'  width = '40' height = '40' />",
    Exp = "<img src='ui://6u4a28edqs6cg5d'  width = '40' height = '40' />"
}

function IconManager.GetMoneyLitIcon(moneyType)
    if moneyType == DBEnum.Moneys.TradeMoney then
        return "ui://6u4a28edf29ag4h"
    elseif moneyType == DBEnum.Moneys.Gold then
        return "ui://6u4a28edf29ag4j"
    elseif moneyType == DBEnum.Moneys.Money then
        return "ui://6u4a28edf29ag4i"
    elseif moneyType == DBEnum.Moneys.BindingMoney then
        return "ui://6u4a28edf29ag4k"
    elseif moneyType == DBEnum.Moneys.Exp then
        return "ui://6u4a28edqs6cg5d"
    end
    return "ui://6u4a28edf29ag4j"
end

function IconManager.Init()
    this.RoleIcon = {}

    this.RoleIcon[0] = {package = 'RoleIcon',name = 'shenjiang_touxiang'};
    this.RoleIcon[1] = {package = 'RoleIcon',name = 'cike'};
    this.RoleIcon[2] = {package = 'RoleIcon',name = 'cike'};
    this.RoleIcon[3] = {package = 'RoleIcon',name = 'shenjiang_touxiang'};
    this.RoleIcon[4] = {package = 'RoleIcon',name = 'cike'};
    this.RoleIcon[5] = {package = 'RoleIcon',name = 'shenjiang_touxiang'};
end

---@param id int 玩家头像id
---@return string 玩家头像url
function IconManager.GetRoleIcon(id)

    local roleIcon =  UIPackage.GetItemURL(this.RoleIcon[id].package,this.RoleIcon[id].name)

    if roleIcon == nil then
        logError('RoleIcon not find ' .. id)
    end

    return roleIcon
end

function IconManager.GetIconUrl(path)
    local p = string.split(path, "/")
    if #p > 1 then
        return UIPackage.GetItemURL(p[1],p[2])
    else
        return ""
    end
end

function IconManager.GetResUrl(pName, rName)
    return UIPackage.GetItemURL(pName, rName)
end

function IconManager.GetItemIcon(itemId)
    local iconPath = DBLogic.QueryString(DBNames.Item, itemId, DBLogic.Item.icon)
    return this.GetIconUrl(iconPath)
end

function IconManager.GetSkillIcon(iconName)
    local icon =  UIPackage.GetItemURL("RoleIcon",iconName)
    if icon == nil then
        -- logError('skill not find ' .. iconName)
    end
    return icon
end
