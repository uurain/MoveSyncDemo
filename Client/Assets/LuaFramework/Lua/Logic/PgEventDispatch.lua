PgEventDispatch = {}

PgEventID = {
	NetConnected = 1,
	NetError = 2,
-- c# 调用lua的 ，上面相反
	SceneClick = 10001,
}

local mhtEvent = {}

function PgEventDispatch.RegisterCallback(eventId, handle)
	if mhtEvent[eventId] == nil then
		mhtEvent[eventId] = {}
	end
	table.insert(mhtEvent[eventId], handle)
end

function PgEventDispatch.DoEvent(eventId, ...)
	log("sdfsdfsdf:"..eventId)
	local events = mhtEvent[eventId]
	if events ~= nil then
		for i,v in ipairs(events) do
			v(...)
		end
	end
end

function PgEventDispatch.RemoveCallBackById(eventId )
	mhtEvent[eventId] = nil
end

function PgEventDispatch.RemoveCallBack(eventId, handle)
	local events = mhtEvent[eventId]
	if events ~= nil then
		for i,v in ipairs(events) do
			if v == handle then
				table.remove(events, i)
				break
			end
		end
	end
end

function PgEventDispatch.CallPGEvent(eventId, ...)
	gameSceneMgr:OnReceiveLuaTable(eventId, ...)
end