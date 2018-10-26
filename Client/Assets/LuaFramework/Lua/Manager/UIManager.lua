require "Common/define"
require "View/UIDefine"

UIManager = {};
local this = UIManager;
local panelList = {};	--控制器列表--

local _windowList = {}
local _basePanelList = {}
local _tipList = {}

local _baseBasePanelIndex = 0
local _baseWindowIndex = 100
local _baseTipIndex = 1000

local _modalLayer = nil

local _blackLayer = nil

local _modalShowCount = 0

local _pkgCachedTable = {}
local _pkgPanelRefCountTable = {}
local _pkgNameRefCountTable = {}

	--预加载图集，公共资源库
local preLoadPackages = {"Common",}

function UIManager.Init()
	logWarn("UIManager.Init----->>>")
	return this
end

function UIManager.LoadPackage(pkgs, callback)
	if #pkgs == 0 then
		callback()
		return
	end
	local loadEndCount = 0
	local totalCount = #pkgs
	local function _checkFinish()
		loadEndCount = loadEndCount + 1
		if totalCount == loadEndCount then
			callback()
		end
	end

	for i,v in ipairs(pkgs) do
		if _pkgCachedTable[v] == nil then
			local function _loadEnd(pkgName)
				log("add package:"..v)
				_pkgCachedTable[v] = true
				_checkFinish()
			end
			resMgr:LoadPackage("UI/Package/&"..v, nil, _loadEnd)
		else
			_checkFinish()
		end
	end
end

-- 添加panel对pkg的引用
function UIManager.AddPackageRef(panelName, pkgs)
	if _pkgPanelRefCountTable[panelName] == nil then
		_pkgPanelRefCountTable[panelName] = {}
	end
	for i,v in ipairs(pkgs) do
		table.insert(_pkgPanelRefCountTable[panelName], v)
		
		if _pkgNameRefCountTable[v] == nil then
			_pkgNameRefCountTable[v] = 1
		else
			_pkgNameRefCountTable[v] = _pkgNameRefCountTable[v] + 1
		end
	end
end

function UIManager.RemovePackage(panelName)
	if _pkgPanelRefCountTable[panelName] == nil then
		return
	end
	for i,v in ipairs(_pkgPanelRefCountTable[panelName]) do
		if _pkgNameRefCountTable[v] then
			_pkgNameRefCountTable[v] = _pkgNameRefCountTable[v] - 1
			if _pkgNameRefCountTable[v] == 0 then
				UIPackage.RemovePackage(v)
				_pkgCachedTable[v] = nil
			end
		end
	end
	_pkgPanelRefCountTable[panelName] = nil
end

--添加控制器--
function UIManager.AddPanel(panelName, ctrlObj)
	panelList[panelName] = ctrlObj;
end

--获取控制器--
function UIManager.GetPanel(panelName)
	return panelList[panelName];
end

--移除控制器--
function UIManager.RemovePanel(panelName)
	panelList[panelName] = nil;
end

-- ！！！！！慎用 还没测试 从内存中删除 所有的panel要从内存移除已经定要走这里，在自己的模块内处理好相关的东西的清除 主要包括unity的clone资源
function UIManager.DestoryPanel(panelName)
	if not panelList[panelName] then logError("Not panel:"..panelName.."Ctrl!") end
	local panelCtrl = panelList[panelName]
	if panelCtrl.panelState == UIPanelState.Loading then
		panelCtrl.panelState = UIPanelState.NeedDestory
	else
		panelCtrl:OnDestroy()
		this.RemovePackage(panelName)
		this.RemovePanel(panelName)
	end
end


function UIManager.HidePanel(panelType, callback)
	local panelCtrl = this.GetPanel(panelType.name)
	if panelCtrl ~= nil then
		if panelCtrl.panelState == UIPanelState.Loaded then
			panelCtrl:hide()
		end
	end
end

-- 这里是从window类的hide方法里面调过来的 别的地方禁止调用
function UIManager.HideWindow(win)
	if win.parent == GRoot.inst then
		GRoot.inst:RemoveChild(win)
	end
end
-- 这里是从window类的show方法调用过来 别的地方禁止调用
function UIManager.ShowWindow(win)
	if win.parent ~= GRoot.inst then
		GRoot.inst:AddChild(win)
	end
end

function UIManager.PreLoadPanel(panelType, callback)
	local panelCtrl = this.GetPanel(panelType)
	if panelCtrl ~= nil then
		callback(panelCtrl)
	else
		this._CreatePanel(panelType, callback)
	end
end

function UIManager.ShowPanel( panelType, callback)
	local panelCtrl = this.GetPanel(panelType.name)
	local function tshow(ui)
		if ui.panelState == UIPanelState.Loaded then
            --打开任意非TIP类型窗口，关闭引导

			ui:show()
			if callback ~= nil then
				callback(ui)
			end
		end
	end
	if panelCtrl ~= nil then
		tshow(panelCtrl)
	else
		this._CreatePanel(panelType, tshow)
	end
end

function UIManager._CreatePanel(panelType, callback)
	local panelCtrl = require("View/UI"..panelType.name).new(panelType)
	panelCtrl.panelState = UIPanelState.Loading
	this.AddPanel(panelType.name, panelCtrl)
	if not panelCtrl then
		logError("Do not create: " ..panelType.."View!")
	else
		local dependPkgs = {}
		table.insert(dependPkgs, panelType.pkgName)
		if panelType.depPkg then
			for i,v in ipairs(panelType.depPkg) do
				table.insert(dependPkgs, v)
			end	
		end
		for i,v in ipairs(preLoadPackages) do
			table.insert(dependPkgs, v)
		end
		this.LoadPackage(dependPkgs, function ()
			this.AddPackageRef(panelType.name, dependPkgs)
			if panelCtrl.panelState == UIPanelState.NeedDestory then
				this.DestoryPanel(panelType.name)
				return
			end

			local view = UIPackage.CreateObject(panelType.pkgName, panelType.name)

			if panelType.fullScreen == true then
				view:SetSize(GRoot.inst.width, GRoot.inst.height)
			end

			if panelType.uType == UIType.window then
				view.sortingOrder = _baseWindowIndex
			elseif panelType.uType == UIType.tip then
				view.sortingOrder = _baseTipIndex
			end

			-- 这里暂时只支持屏幕中间的位置
			if panelType.anchor then
				if panelType.anchor == UIPanelAnchor.center then
					view:Center(true)
				end
			end
			
			GRoot.inst:AddChild(view)

			panelCtrl:Awake(view)
			panelCtrl.panelState = UIPanelState.Loaded
			panelCtrl:hide()
			if callback ~= nil then
				callback(panelCtrl)
			end
		end)
	end
end

--窗体是否已经显示
function UIManager.PanelIsActive(panelType)
	if panelType == nil then
		return false
	end

    if panelList[panelType.name] == nil then
        return false
    end

	local pnl = UIManager.GetPanel(panelType.name)
	if pnl == nil then
		return false
	elseif pnl.view.visible == true and  pnl.view.parent ~= nil then
		return true
	end
	return false
end

function UIManager.CreateModalLayer()
	_modalLayer = GGraph.New()
	_modalLayer:DrawRect(GRoot.inst.width, GRoot.inst.height, 0, Color.white, Color.New(0,0,0,0))
	_modalLayer:AddRelation(GRoot.inst, RelationType.Size)
	_modalLayer.name = "ModalLayerLua"
	_modalLayer.gameObjectName = "ModalLayerLua"
	_modalLayer:SetHome(GRoot.inst);
end

function UIManager.ShowModalWait()
	if _modalLayer == nil then
		this.CreateModalLayer()
		_modalLayer.sortingOrder = 3000
	end
	_modalShowCount = _modalShowCount+1
	GRoot.inst:AddChild(_modalLayer)
end

function UIManager.HideModalWait()
	_modalShowCount = _modalShowCount-1
	if _modalShowCount == 0 and _modalLayer ~= nil then
		GRoot.inst:RemoveChild(_modalLayer)
	end
end

function UIManager.ShowBlackModal(color)
	if _blackLayer == nil then
		_blackLayer = GGraph.New()
		_blackLayer:DrawRect(GRoot.inst.width, GRoot.inst.height, 0, Color.white, Color.New(0,0,0,0))
		_blackLayer:AddRelation(GRoot.inst, RelationType.Size)
		_blackLayer.name = "ModalBlackLua"
		_blackLayer.gameObjectName = "ModalBlackLua"
		_blackLayer:SetHome(GRoot.inst);
	end
	if color ~= nil then
		_blackLayer.color = color
	else
		_blackLayer.color = Color.black
	end
	GRoot.inst:AddChild(_blackLayer)
end

function UIManager.HideBlackModal()
	if _blackLayer ~= nil then
		GRoot.inst:RemoveChild(_blackLayer)
	end
end

function UIManager.CreateHeadBar()
	local view = UIPackage.CreateObject("Common", "HeadBar")
	local ui = require("View.UIHeadBar").new()
	ui:Awake(view)
	GRoot.inst:AddChild(view)
	return ui
end