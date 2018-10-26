-- 创建一个用于显示特效在ui上

local DisplayObject = class("DisplayObject")

function DisplayObject:ctor(name)
	self.graph = GGraph.New()
	self.graph.name = name
	self.graph.gameObjectName = name


	self.cached = false
	self.name = name
	self.isFullScreen = false
	self.duration = 0
	self.loadCmpt = false
	self.needDestory = false
	self.renderMatPath = nil
	self.mat = nil
end

function DisplayObject:Show(parent, fullScreen, duration, cached)
	if parent == nil then
		GRoot.inst:AddChild(self.graph)
	else
		parent:AddChild(self.graph)
	end
	if fullScreen ~= nil then
		self.graph:Center(true)
		self.isFullScreen = fullScreen
	end	
	if cached ~= nil then
		self.cached = cached
	end
	if duration and duration > 0 then
		local timeHandle = Timer.New(handler(self, self.Destory), duration)
		timeHandle:Start()
	end
	self:CreateEffect(self.name)
end

function DisplayObject:Hide()
	self.graph.visible = false
end

function DisplayObject:Replay()
	self:Hide()
	self.graph.visible = true
end

function DisplayObject:GetVisible()
	return self.graph.visible
end

function DisplayObject:CreateEffect(effectPath)
    local onLoadEnd = function(go)
    	self.loadCmpt = true		
    	self.goWrapper = FairyGUI.GoWrapper.New(go)
    	self.graph:SetNativeObject(self.goWrapper)
    	go.transform.localPosition = Vector3.New(0, 0, 100)
    	if self.isFullScreen then    		
    		go.transform.localScale = Vector3.New(GRoot.inst.width/10, GRoot.inst.height/10, 1)
    	end

        if self.targetPos ~= nil then
            go.transform.localPosition = go.transform.localPosition + self.targetPos
        end

    	if self.needDestory then
    		self:Destory()
    	else
    		go:SetActive(true)

    		if self.renderMatPath ~= nil then
    			self.mat = go.transform:FindChild(self.renderMatPath):GetComponent("Renderer").sharedMaterial
    		end
    	end
    end
    resMgr:LoadGameObject(effectPath, nil,nil, onLoadEnd)
end

function DisplayObject:Destory()
	if self.loadCmpt then
		if self.cached then
			self:Hide()
		else
			self.graph:Dispose()
			DisplayObjectMgr:Remove(self.name,self)
		end
	else
		self.needDestory = true
	end
end

function DisplayObject:SetMatFLoat(pName, val)
	if self.mat ~= nil then
		self.mat:SetFloat(pName, val)
	end
end

return DisplayObject