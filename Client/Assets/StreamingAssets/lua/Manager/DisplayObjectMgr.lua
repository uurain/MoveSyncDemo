DisplayObjectMgr = {}

local displayTable = {}

function DisplayObjectMgr:Create(effectPath)
	local displayObj = require("View/DisplayObject").new(effectPath)

	if displayTable[effectPath] == nil then
		displayTable[effectPath] = {}
	end
	table.insert(displayTable[effectPath], displayObj)
	return displayObj
end


-- 从table中remove
function DisplayObjectMgr:Remove(effectPath, displayObj)
	if displayTable[effectPath] ~= nil then
		for i,v in ipairs(displayTable[effectPath]) do
			if v == displayObj then
				table.remove(displayTable[effectPath], i)
				break
			end
		end
	end
end

-- 通过路径直接删除
function DisplayObjectMgr:Delete(effectPath)
	if displayTable[effectPath] ~= nil then
		local allRes = {}
		for i,v in ipairs(displayTable[effectPath]) do
			table.insert(allRes, v)
		end
		-- destory会调用上面的remove 造成table index错误
		for i,v in ipairs(allRes) do
			v:Destory()
		end
	end
end