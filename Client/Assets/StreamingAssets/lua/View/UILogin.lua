require "Common/define"
local UILogin = class(require("View.BasePanel"))

function UILogin:ctor()
	log("UILogin:ctor")
	-- self.super.ctor(self,UIPanelType.MainGame)
	-- self.btnBegin = nil
	self.btnLogin = nil
	self.tfUsername = nil
	self.tfPassword = nil
	self.groupLogin = nil
	self.serverList = nil
end

--初始化面板--
function UILogin:init()
	log("UILogin:init")
	self.super.init(self)

	self.btnLogin = self.view:GetChild("btnLogin")
	self.btnLogin.onClick:Add(self.onClickLogin, self)


	-- local bgGloader = self.view:GetChild("n31")
	-- bgGloader.width = GRoot.inst.width
	-- bgGloader.height = GRoot.inst.height
	-- bgGloader.url = "UI/BigPic/bg2.jpg"


	self.tfUsername = self.view:GetChild("inputUsername")

	local localName = self:GetUserName()
	if localName ~= "" then
		self.tfUsername.text = localName
	end
	
	self.loginLogic = LogicManager.Get(UIPanelType.Login.name)
	self.loginLogic:RegisterCallback("LOGINSUCESS", handler(self.hide, self))
	-- LogicManager.Get(UIPanelType.CreateRole.name):RegisterCallback("ROLELIST", self.roleListHandle)
end

--单击事件--
function UILogin:OnDestroy()
	logWarn("OnDestroy---->>>");
	self.super.OnDestroy(self)
end

function UILogin:onClickLogin()
	log("onClickLogin")
	self:hide()
	-- LogicManager.Get(LogicType.GameMgr):LocalTest()
	self.loginLogic:ReqLogin(self.tfUsername.text)
end

function UILogin:onClickUpdate()
	log("UILogin:onClickUpdate")	
end

function UILogin:GetUserName()
	local localName =  self.tfUsername.text
	if localName ~= '' then 
		saveDataMgr:setString("TempLogin", self.tfUsername.text)
		return localName
	end
	localName = saveDataMgr:getString("TempLogin", '')
	if localName ~= '' then
		return localName
	end
	localName = "Guest:"..os.time()
	saveDataMgr:setString("TempLogin", localName)
	return localName
end

return UILogin