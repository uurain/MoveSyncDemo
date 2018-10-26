PreloadMgr = {}

local _loadTable = {}
local _isLoading = false
local _loadCount = 0
local _cmpHandle = nil
local _cachedTable = {}

function PreloadMgr.Init()

end

function PreloadMgr.AddPathTable(path)
	if _isLoading then

	else
		table.insert(_loadTable, path)
		-- for i,v in ipairs(path) do
		-- 	table.insert(_loadTable, v)
		-- end
	end
end

function PreloadMgr.AddActorRes()
	-- body
end

function PreloadMgr.GetObj(path, func)
	local go = _cachedTable[path]
	if go ~= nil then
		_cachedTable[path] = nil
		return go
	else
		resMgr:LoadGameObject(path, nil,nil, func)
	end
end

function PreloadMgr.Begin(ct)
	_isLoading = true
	_loadCount = 0
	_cmpHandle = ct
	for i,v in ipairs(_loadTable) do
		local resPath = v
		function onLoadEnd(go)
			_cachedTable[resPath] = go 
			_loadCount = _loadCount + 1
			if _loadCount == #_loadTable then
				PreloadMgr.Complete()
			end
		end
		resMgr:LoadGameObject(resPath, nil,nil, onLoadEnd)
	end
end

function PreloadMgr.Complete()
	_isLoading = false
	if _cmpHandle then
		_cmpHandle()
	end
end

function PreloadMgr.GetLoadProcess()
	if _isLoading then
		local totalCount = #_loadTable
		return _loadCount/totalCount
	else
		return 1
	end
end
