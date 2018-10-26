local LoginLogic = class(require("Logic.BaseLogic"))

local _ELoginState = {
	none = 0,
	waitSocket = 1,
	waitLogin = 2,
	sucess = 3,
}

function LoginLogic:Init()
	self.account = nil
	self.loginState = _ELoginState.none

	Event.AddListener(Msg.Id.AckLogin, handler(self.OnAckLogin, self))
	PgEventDispatch.RegisterCallback(PgEventID.NetConnected, handler(self.OnNetConnected, self))
	PgEventDispatch.RegisterCallback(PgEventID.NetError, handler(self.OnNetError, self))
end

function LoginLogic:OnNetConnected()
	log("LoginLogic:OnNetConnected1")
	if self.loginState == _ELoginState.waitSocket then
		log("LoginLogic:OnNetConnected2")
		self:ReqLogin(self.account)
	end
end

function LoginLogic:OnNetError()
	self.loginState = _ELoginState.none
	UIManager.ShowPanel(UIPanelType.CheckBox, function (ui)
		ui:UpdateUI("断开连接！", 0)
	end)
end

function LoginLogic:OnAckLogin(msg)
	log("OnAckLogin:"..msg.errorCode..";"..msg.uid)
	if msg.errorCode == 0 then
		ConstMgr.uid = msg.uid
		self.loginState = _ELoginState.sucess		
		self.super.DoEvent(self, "LOGINSUCESS")
	else
		self.loginState = _ELoginState.none
		log("login fail:"..msg.errorCode)
	end
end

function LoginLogic:ReqLogin(account)
	self.account = account
	if Network.GetConnectType() == EConnectType.connected then
		if self.loginState == _ELoginState.waitSocket then
			self.loginState = _ELoginState.waitLogin
			log("Msg_ReqLogin:"..self.account)
			Msg_ReqLogin(self.account)
		end
	else
		if self.loginState == _ELoginState.none then
			self.loginState = _ELoginState.waitSocket
			Network.ConnectServer(ConstMgr.serverIp, ConstMgr.serverPort)
		end
	end
end

return LoginLogic