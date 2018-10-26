local GameObjectNode = class("GameObjectNode")

function GameObjectNode:ctor(resPath)
    self.parent = nil
    self.resId = 0
    self.childTable = {}
    self.assetObj = nil
    self.resPath = resPath
    self.isDispose = false
    self.needSetTransform = false
    self.parentPosName = ""
    self.curPlayAniName = ""
    self.animator = nil
end

function GameObjectNode:Load(cb)
    self.cb = cb
    Res_LoadGameObject(self.resPath, handler(self.OnLoad, self))
end

function GameObjectNode:SetParentPosName(val)
    self.parentPosName = val
end

function GameObjectNode:PlayAni(val)
    self.curPlayAniName = val
    -- log("GameObjectNode PlayAni:"..val)
    if self.animator ~= nil then
         -- log("GameObjectNode PlayAni1:"..val)
        self.animator:CrossFade(val, 0.1)
    end
end

function GameObjectNode:IsPlayAni(aniName)
    if self.animator == nil then
        return false
    end
    return self.animator:GetCurrentAnimatorStateInfo(0):IsName(aniName) or self.animator:GetNextAnimatorStateInfo(0):IsName(aniName)
end

function GameObjectNode:IsLoaded()
    return self.assetObj ~= nil
end

function GameObjectNode:OnLoad(ao)
    if ao == nil then
        logError("GameObjectNode error:"..self.resPath)
        return
    end
    if self.isDispose then
        Res_UnloadAssetObj(ao)
        return
    end
    ao.CachedTransform.localPosition = Vector3.zero
    ao.CachedTransform.localEulerAngles = Vector3.zero
    ao.CachedTransform.localScale = Vector3.one
    self.assetObj = ao
    self.animator = self.assetObj.CachedGameObject:GetComponent("Animator")
    if self.curPlayAniName ~= "" then        
        self:PlayAni(self.curPlayAniName)
    end
    if self.parent ~= nil then
        self:SetTransformParent()
    end
    for i, v in ipairs(self.childTable) do
        if v.needSetTransform and v.assetObj ~= nil then
            v:SetTransformParent()
        end
    end
    if self.cb ~= nil then
        self.cb(self.assetObj)
    end
end


function GameObjectNode:SetTransformParent()
    self.needSetTransform = false
    if self.parent == nil then
        if self.assetObj ~= nil then
            self.assetObj.CachedTransform:SetParent(nil)
        end
    else
        if self.assetObj ~= nil and self.parent.assetObj ~= nil then
            local tempParent = nil
            if self.parentPosName == "" then
                tempParent = self.parent.assetObj.CachedTransform
            else
                local parentTemp = self.parent:GetChildTrans(self.parentPosName)
                if parentTemp == nil then
                    parentTemp = self.parent.assetObj.CachedTransform
                else
                    tempParent = parentTemp.transform
                end
            end
            self.assetObj.CachedTransform:SetParent(tempParent, false)
            self.assetObj.CachedTransform.localPosition = Vector3.zero
            self.assetObj.CachedTransform.localEulerAngles = Vector3.zero
            self.assetObj.CachedTransform.localScale = Vector3.one
        else
            self.needSetTransform = true
        end
    end
end

function GameObjectNode:InternalSetParent(val)
    self.parent = val
    self:SetTransformParent()
end

function GameObjectNode:RemoveFromParent()
    if self.parent ~= nil then
        self.parent:RemoveChild(self)
    end
end

function GameObjectNode:AddChild(val)
    if val.parent ~= self then
        val:RemoveFromParent()
        val:InternalSetParent(self)
        table.insert(self.childTable, val)
    end
end

function GameObjectNode:RemoveChild(val)
    for i, v in ipairs(self.childTable) do
        if v == val then
            self.childTable[i]:InternalSetParent(nil)
            self.childTable[i] = nil
            break
        end
    end
end

function GameObjectNode:GetChildTrans(transName)
    if self.assetObj ~= nil then
        return self.assetObj:GetNode(transName)
    end
    return nil
end

function GameObjectNode:Dispose(disposeChild)
    self:RemoveFromParent()
    for i, v in ipairs(self.childTable) do
        v:InternalSetParent(nil)
        if disposeChild then
            v:Dispose(disposeChild)
        end
    end
    self.childTable = {}
    if self:IsLoaded() then
        Res_UnloadAssetObj(self.assetObj)
        self.assetObj = nil
    end
    self.animator = nil
    self.resPath = ""
    self.isDispose = true
    self.cb = nil
end

return GameObjectNode