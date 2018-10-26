local BaseLogic = class("BaseLogic")

function BaseLogic:ctor()
	self.mhtEvent = {}
end

function BaseLogic:RegisterCallback(eventId, handle)
	if self.mhtEvent[eventId] == nil then
		self.mhtEvent[eventId] = {}
	end
	table.insert(self.mhtEvent[eventId], handle)
end

function BaseLogic:DoEvent(eventId, ...)
	local events = self.mhtEvent[eventId]
	if events ~= nil then
		for i,v in ipairs(events) do
			v(...)
		end
	end
end

function BaseLogic:RemoveCallBackById(eventId )
	self.mhtEvent[eventId] = nil
end

function BaseLogic:RemoveCallBack(eventId, handle)
	local events = self.mhtEvent[eventId]
	if events ~= nil then
		for i,v in ipairs(events) do
			if v == handle then
				table.remove(events, i)
				break
			end
		end
	end
end

function BaseLogic:Init()
	
end

function BaseLogic:Reset()
	
end

function BaseLogic:Dispose()
	self.mhtEvent = {}
end

return BaseLogic