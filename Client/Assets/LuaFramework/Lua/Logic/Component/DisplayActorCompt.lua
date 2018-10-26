local DisplayActorCompt = class(require("Logic.Component.BaseCompt"))

local _screenScale = 75

-- local function begin!
local function _NewNode(resPath)
    return require("Logic.Component.GameObjectNode").new(resPath)
end
-- local function end!

function DisplayActorCompt:ctor()
    self.modelTable = {}
    self.weaponTable = {}
    self.weaponId = -1
    self.isRide = false

    self.initPos = Vector2.zero
    self.initLocalEuler = nil

    self.localScale = 1
    self.localEulerAngles = Vector3.zero
    self.posOffset = Vector2.zero

    self.touchObj = nil
    self.rotationSpeed = 0

    self.rootNode = nil
end

-- 这个接口必须要在create完之后马上设置 否则后面坐标 缩放==都不能生效
function DisplayActorCompt:SetWrapperParent(wrapperParent)
    self.goWrapper = FairyGUI.GoWrapper.New()
    wrapperParent:SetNativeObject(self.goWrapper)
    self.goWrapper.z = 1000
    self.initPos = self.goWrapper.xy
end

-- 设置世界模式 跟上面模式不能并存
function DisplayActorCompt:SetWorldModel()
    self.rootWorldObj = GameObject.New("WorldModel")
    GameObject.DontDestroyOnLoad(self.rootWorldObj)
end

function DisplayActorCompt:SetVisible(val)
    if self.rootWorldObj ~= nil then
        self.rootWorldObj:SetActive(val)
        return
    end
    if self.goWrapper ~= nil then
        self.goWrapper.visible = val
    end
end

function DisplayActorCompt:SetLocalScale(val)
    self.localScale = val
    local scaleVal = _screenScale * self.localScale
    if self.rootWorldObj ~= nil then
        self.rootWorldObj.transform.localScale = Vector3.New(scaleVal, scaleVal, scaleVal)
        return
    end
    if self.goWrapper ~= nil then
        self.goWrapper.scale = Vector2.New(scaleVal, scaleVal)
    else
        logError("没有调用SetWrapperParent方法")
    end
end

-- 直接设置模型scale 建议使用SetLocalScale来做换算
function DisplayActorCompt:SetSize(val)
    if self.goWrapper ~= nil then
        self.goWrapper.scale = val
    end
end

function DisplayActorCompt:SetLocalEulerAngles(val)
    if self.initLocalEuler == nil then
        self.initLocalEuler = val
    end
    self.localEulerAngles = val
    if self.rootWorldObj ~= nil then
        self.rootWorldObj.transform.localEulerAngles = val
        return
    end
    if self.goWrapper ~= nil then
        self.goWrapper.cachedTransform.localEulerAngles = val
    else
        logError("没有调用SetWrapperParent方法")
    end
end

function DisplayActorCompt:SetLocalPos(val)
    if self.rootWorldObj ~= nil then
        self.rootWorldObj.transform.localPosition = val
    end
end

function DisplayActorCompt:SetLookPos(val)
    if self.rootWorldObj ~= nil then
        val.y = self.rootWorldObj.transform.localPosition.y
        self.rootWorldObj.transform:LookAt(Vector3.New(val.x, val.y, val.z))
    end
end

function DisplayActorCompt:SetPosOffset(val)
    self.posOffset = val
    if self.goWrapper ~= nil then
        self.goWrapper.xy = val + self.initPos
    else
        logError("没有调用SetWrapperParent方法")
    end
end

function DisplayActorCompt:GetRootNode()
    if self.rootWorldObj ~= nil then
        return self.rootWorldObj
    end
    return self.rootNode
end

function DisplayActorCompt:SetOnGetRootNode(cb)
    self.onRootNodeCb = cb
end

-- clothId 外观id
-- npcId npcId查询npc表
function DisplayActorCompt:CreateClothes(clothId, npcId)
    local resId = clothId
    if resId == nil then
        resId = npcId
    end
    if not self:_CheckModel(ModelType.Model, resId) then
        return
    end
    local resPath = "Actor/actor1.prefab"
    -- if npcId ~= nil then
    --     resPath = DBLogic.QueryString(DBNames.Npc, npcId, DBLogic.NPC.prefab)
    -- else
    --     resPath = self:_GetEquipmentResPath(clothId)
    -- end
    self.modelTable[ModelType.Model] = _NewNode(resPath)
    self.modelTable[ModelType.Model].resId = resId
    self.modelTable[ModelType.Model]:Load(function (assetObj)
        self:_OnNodeLoad(ModelType.Model, assetObj, self.modelTable[ModelType.Model])
    end)
end

function DisplayActorCompt:CreateWeapon(weaponId)
    if self.weaponId == weaponId then
        return
    else
        self:DisposeModel(ModelType.Weapon)
    end
    local weaponPathTable, weaponPosTable = self:_GetWeaponResPath(weaponId)
    for i, v in ipairs(weaponPathTable) do
        local nodeWeapon = _NewNode(v)
        nodeWeapon:SetParentPosName(weaponPosTable[i])
        nodeWeapon:Load(function (assetObj)
            self:_OnNodeLoad(ModelType.Weapon, assetObj, nodeWeapon)
        end)
        table.insert(self.weaponTable, nodeWeapon)
    end
end

function DisplayActorCompt:CreateWing(wingId)
    if not self:_CheckModel(ModelType.Wing, wingId) then
        return
    end
    local resPath = DBLogic.QueryValue(DBNames.Wing, wingId, DBLogic.Wing.model)
    self.modelTable[ModelType.Wing] = _NewNode(resPath)
    self.modelTable[ModelType.Wing].resId = wingId
    self.modelTable[ModelType.Wing]:SetParentPosName("wing_point")
    self.modelTable[ModelType.Wing]:Load(function (assetObj)
        self:_OnNodeLoad(ModelType.Wing, assetObj, self.modelTable[ModelType.Wing])
    end)
end

function DisplayActorCompt:CreateHorse()
    self.isRide = true
end


--obj 触摸对象 speed 旋转速度
function DisplayActorCompt:RegisterTouchMove(obj, speed)
    self.rotationSpeed = speed
    if speed == nil or speed == 0 then
        self.rotationSpeed = 10
    end
    if self.touchObj == nil then
        self.touchObj = require("View.ModelTouchConrol").new()
        self.touchObj:SetTouchPad(obj)
    end
    self:_SetTouchTarget()
end

function DisplayActorCompt:_SetTouchTarget()
    if self.touchObj ~= nil and self.goWrapper ~= nil then
        self.touchObj:SetTrans(self.goWrapper.cachedTransform)
    else
        logError("没有调用SetWrapperParent方法")
    end
end

function DisplayActorCompt:ResetRotation()
    self:SetLocalEulerAngles(self.initLocalEuler)
end

function DisplayActorCompt:PlayAni(aniName)
    if aniName == nil or aniName == "" then
        return
    end
     -- log("DisplayActorCompt PlayAni:"..aniName)
    if self.modelTable[ModelType.Model] ~= nil then
        if not self.modelTable[ModelType.Model]:IsPlayAni("attack01") then
            self.modelTable[ModelType.Model]:PlayAni(aniName)
        end
    end
end

function DisplayActorCompt:IsPlayAni(aniName)
    if aniName == nil or aniName == "" then
        return false
    end
    if self.modelTable[ModelType.Model] ~= nil then
        return self.modelTable[ModelType.Model]:IsPlayAni(aniName)
    end
end

function DisplayActorCompt:_OnNodeLoad(modelType, assetObj, modelNode, parentName)
    if modelType == ModelType.Model then
        if not self.isRide then
            self:_UpdateWrapperTarget(assetObj)
        else
            self.modelTable[ModelType.Horse]:AddChild(modelNode)
            if self.modelTable[ModelType.Horse]:IsLoaded() then
                self:_UpdateWrapperTarget(self.modelTable[ModelType.Horse].assetObj)
            end
        end
    elseif modelType == ModelType.Wing or modelType == ModelType.Weapon then
        self.modelTable[ModelType.Model]:AddChild(modelNode)
        if self.modelTable[ModelType.Model]:IsLoaded() then
            self:_UpdateWrapperTarget(self.modelTable[ModelType.Model].assetObj)
        end
    end
end

function DisplayActorCompt:_CheckModel(modelType, resId)
    if resId == nil then
        return false
    end
    if self.modelTable[modelType] ~= nil then
        if self.modelTable[modelType].resId == resId then
            return false
        else
            self:DisposeModel(modelType)
        end
    end
    return true
end

function DisplayActorCompt:DisposeModel(modelType)
    if modelType == ModelType.Weapon then
        for k, v in pairs(self.weaponTable) do
            v:Dispose()
        end
        self.weaponTable = {}
        return
    end
    if self.modelTable[modelType] ~= nil then
        if self.modelTable[modelType].assetObj ~= nil and self.modelTable[modelType].assetObj.CachedGameObject == self.goWrapper.wrapTarget then
            self.goWrapper.wrapTarget = nil
        end
        self.modelTable[modelType]:Dispose()
        self.modelTable[modelType] = nil
    end
    if modelType == ModelType.Horse then
        self.isRide = false
    end
end

function DisplayActorCompt:Dispose()
    if self.goWrapper ~= nil then
        self.goWrapper.wrapTarget = nil
        self.goWrapper:Dispose()
        self.goWrapper = nil
    end
    for k, v in pairs(self.modelTable) do
        v:Dispose()
    end
    for k, v in pairs(self.weaponTable) do
        v:Dispose()
    end
    if self.touchObj ~= nil then
        self.touchObj:Dispose()
        self.touchObj = nil
    end
    if self.rootWorldObj ~= nil then
        self.rootWorldObj.transform:DetachChildren()
        GameObject.Destroy(self.rootWorldObj)
        self.rootWorldObj = nil
    end
    self.rootNode = nil
    self.modelTable = {}
    self.weaponTable = {}
    self.isRide = false
    self.onRootNodeCb = nil

    self.super.Dispose(self)
end

function DisplayActorCompt:_GetEquipmentId(equipmentId)
    if equipmentId <= 0 then
        if self.jobType and self.jobType >= 0 then
            equipmentId = DBLogic.GetPlayerResource(self.jobType)
        end
    end
    return equipmentId
end

function DisplayActorCompt:_GetWeaponId(weaponId)
    if weaponId <= 0 then
        if self.jobType and self.jobType >= 0 then
            weaponId = DBLogic.QueryValue(DBNames.PlayerBorn,self.jobType,DBLogic.PlayerBorn.weapon)
        end
    end
    return weaponId
end

function DisplayActorCompt:_GetEquipmentResPath(equipmentId)
    return DBLogic.QueryValue(DBNames.Equipment, self:_GetEquipmentId(equipmentId), DBLogic.Equipment.resource)
end

function DisplayActorCompt:_GetWeaponResPath(equipmentId)
    local weaponPathTable = {}
    local weaponPosTable = {}
    equipmentId = self:_GetWeaponId(equipmentId)
    local weaponPathStr = DBLogic.QueryValue(DBNames.Equipment, equipmentId, DBLogic.Equipment.weaponPath)
    local weaponPositionStr = DBLogic.QueryValue(DBNames.Equipment, equipmentId, DBLogic.Equipment.weaponPosition)
    if weaponPathStr ~= "" and weaponPositionStr ~= "" then
        weaponPathTable = string.split(weaponPathStr, ";")
        weaponPosTable = string.split(weaponPositionStr, ";")
    end
    return weaponPathTable, weaponPosTable
end

function DisplayActorCompt:_UpdateWrapperTarget(rootNode)
    if self.onRootNodeCb ~= nil then
        if rootNode ~= self.rootNode then
            self.onRootNodeCb(rootNode)
        end
    end
    self.rootNode = rootNode
    if self.rootWorldObj ~= nil then
        rootNode.CachedTransform:SetParent(self.rootWorldObj.transform)
    else
        if self.goWrapper == nil then
            logError("如果是ui 需要调用SetWrapperParent方法")
        else
            self.goWrapper.wrapTarget = rootNode.CachedGameObject
        end
    end
end

return DisplayActorCompt