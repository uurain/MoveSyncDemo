local BaseCompt = class("BaseCompt")

function BaseCompt:ctor()
	self.onwer = nil
end

function BaseCompt:Init(o)
	self.onwer = o
end

function BaseCompt:Dispose()
	
end

return BaseCompt