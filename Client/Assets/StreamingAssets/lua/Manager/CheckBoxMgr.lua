---
--- Created by Administrator.
--- DateTime: 2017/8/9 16:34
---
CheckBoxMgr = {}

CheckBoxMgr.Type =
{
    OK = 1,
    OKAndCanel1 = 0,
    OKAndCanel2 = 3,
    OKAndCanel3 = 2
}

function CheckBoxMgr.ShowCheck(text1,handleOk,handleCancel,text2,text3)

    local tip1 = ""
    local tip2 = ""
    local tip3 = ""
    local showType = nil
    local event = nil

    if text1 ~= nil then
        tip1 = text1

        if handleOk ~= nil then
            if text2 == nil and text3 == nil then
                tip2 = ""
                tip3 = ""
                showType = CheckBoxMgr.Type.OKAndCanel1
            elseif text2 ~= nil and text3 == nil then
                tip2 = text2
                tip3 = ""
                showType = CheckBoxMgr.Type.OKAndCanel2
            elseif text2 ~= nil and text3 ~= nil then
                tip2 = text2
                tip3 = text3
                showType = CheckBoxMgr.Type.OKAndCanel3
            end
            event = handleOk
        else
            showType = CheckBoxMgr.Type.OK
        end
        UIManager.ShowPanel(UIPanelType.CheckBox,function(ui)
            ui:UpdateUI(tip1,tip2,tip3,showType,event,handleCancel)
        end)
    end

end

function CheckBoxMgr.ShowTip(text)
    if text == nil then
        return
    end

    if UIManager.PanelIsActive(UIPanelType.Loading) then
        return
    end

    local ui = UIManager.GetPanel(UIPanelType.Tip.name)
    ui:AddText(text)
end

function CheckBoxMgr.showItemGetTip(itemId,itemCount)
    if itemCount > 0 then
        local itemName = DBLogic.QueryString(DBNames.Item, itemId, DBLogic.Item.name)
        local tip = ""

        if itemId < DBEnum.Moneys.Total then
            tip = string.format(Language.Get("checkboxmgr_text1"),itemCount,itemName)
        else
            tip = string.format(Language.Get("checkboxmgr_text2"),itemCount,itemName)
        end

        CheckBoxMgr.ShowTip(tip)
    end
end