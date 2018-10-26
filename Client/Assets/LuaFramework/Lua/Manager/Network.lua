require "Common/define"
require "Common/functions"

require "Logic.PgEventDispatch"

require "Protocol.MsgDefine_pb"
require "Protocol.msg_c2s"
require "Protocol.msg_define"



Event = require 'events'

EConnectType = {
    none = 0,
    connecting = 1,
    connected = 2,
    error = 3,
}

Network = {};
local this = Network;

local _connectType = EConnectType.none
local _connectedIp = ""
local SocketEvent = {}

function Network.Start()
    _connectedIp = ""
    _connectType = EConnectType.none

    -- this.ConnectServer("127.0.0.1", 3563)
end

function Network.ConnectServer(ip, port)
    _connectType = EConnectType.connecting
    _connectedIp = ip
    networkMgr:ConnectServer(ip , port)
end

function Network.ShutDown()
    networkMgr:ShutDown()
end


function Network.BrocastMsg(msgId, msg)
    if not this.OnAckRespond(msgId, msg) then
        -- 如果在发送消息的时候没有注册 那么从这里走
        Event.Brocast(msgId, msg);
    end
end

--Socket消息 所有的消息都从这里回调--
function Network.OnSocket(key, data)
    log("OnSocket id:"..key)
    if Msg.Func[key] then
        local res = Msg.Func[key](data)
        Network.BrocastMsg(key, res)
    else
        logError("Protocol_ParseFunc nil:"..key)
    end
end

--当连接建立时--
function Network.OnConnected()
    log("lua OnConnected")
    _connectType = EConnectType.connected
    PgEventDispatch.DoEvent(PgEventID.NetConnected)
end


function Network.OnNetError(errorCode)
    _connectType = EConnectType.error
    PgEventDispatch.DoEvent(PgEventID.NetError, errorCode)
end

function Network.SendMessage(msgId, msg)
    -- log("SendMessage:"..msgId)
    if _connectType ~= EConnectType.connected then
        logError("server not connect")
        return
    end
    networkMgr:SendMsg(tonumber(msgId), msg)
end

-- 已注册的消息从这里回调
function Network.OnAckRespond(msgId, data)
    if SocketEvent[msgId] ~= nil then
        for i,v in ipairs(SocketEvent[msgId]) do
            v(data)
        end
        this.RemoveAckListener(msgId)
        return true
    end
    return false
end

function Network.AddAckListener(msgId, callback)
    if SocketEvent[msgId] == nil then
        SocketEvent[msgId] = {}
    end
    table.insert(SocketEvent[msgId], callback)
end

function Network.RemoveAckListener(msgId)
    if SocketEvent[msgId] ~= nil then
        SocketEvent[msgId] = nil
    end
end

function Network.GetConnectType()
    return _connectType
end

--卸载网络监听--
function Network.Unload()
    logWarn('Unload Network...');
end