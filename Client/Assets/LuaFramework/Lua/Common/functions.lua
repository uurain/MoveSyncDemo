
--输出日志--
function log(str)
    msg = debug.traceback()
    Util.Log(str.."\n"..msg);
end

--错误日志--
function logError(str) 
	Util.LogError(str);
end

--警告日志--
function logWarn(str) 
	Util.LogWarning(str);
end

function logTable(t)
    log(table.tostring(t))
end

function getTextLen(str)
  
end

--查找对象--
function find(str)
	return GameObject.Find(str);
end

function destroy(obj)
	GameObject.Destroy(obj);
end

function newObject(prefab)
	return GameObject.Instantiate(prefab);
end


function child(str)
	return transform:FindChild(str);
end

function subGet(childNode, typeName)		
	return child(childNode):GetComponent(typeName);
end

function SetActiveGO(go, show)
  if go.activeSelf ~= show then 
    go:SetActive(show)
  end   
end

function findPanel(str) 
	local obj = find(str);
	if obj == nil then
		error(str.." is null");
		return nil;
	end
	return obj:GetComponent("BaseLua");
end

--深度拷贝表
function table.deepcopy(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end
        local new_table = {}
        lookup_table[object] = new_table
        for index, value in pairs(object) do
            new_table[_copy(index)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end
    return _copy(object)

end

function table.tostring(tbl, indent, limit, depth, jstack)
  limit   = limit  or 1000
  depth   = depth  or 7
  jstack  = jstack or {}
  local i = 0

  local output = {}
  if type(tbl) == "table" then
    -- very important to avoid disgracing ourselves with circular referencs...
    for i,t in ipairs(jstack) do
      if tbl == t then
        return "<self>,\n"
      end
    end
    table.insert(jstack, tbl)

    table.insert(output, "{\n")
    for key, value in pairs(tbl) do
      local innerIndent = (indent or " ") .. (indent or " ")
      table.insert(output, innerIndent .. tostring(key) .. " = ")
      table.insert(output,
        value == tbl and "<self>," or table.tostring(value, innerIndent, limit, depth, jstack)
      )

      i = i + 1
      if i > limit then
        table.insert(output, (innerIndent or "") .. "...\n")
        break
      end
    end

    table.insert(output, indent and (indent or "") .. "},\n" or "}")
  else
    if type(tbl) == "string" then tbl = string.format("%q", tbl) end -- quote strings
    table.insert(output, tostring(tbl) .. ",\n")
  end

  return table.concat(output)
end

function clone(object)
    local lookup_table = {}
    local function _copy(object)
        if type(object) ~= "table" then
            return object
        elseif lookup_table[object] then
            return lookup_table[object]
        end
        local new_table = {}
        lookup_table[object] = new_table
        for key, value in pairs(object) do
            new_table[_copy(key)] = _copy(value)
        end
        return setmetatable(new_table, getmetatable(object))
    end
    return _copy(object)
end

function class(baseClass)
    -- 一个类模板
    local class_type = { }
    class_type.super = baseClass
    class_type.ctor = function() end
    class_type.new = function(...)
        -- 对一个新建的表，递归执行构造函数
        local instObj = { }
        instObj.super = baseClass
        instObj.ctor = function() end
        -- 递归函数
        local CallCtor
        CallCtor = function(curClassType, ...)
            if curClassType.super ~= nil then
                CallCtor(curClassType.super, ...)
            end
            if curClassType.ctor ~= nil then
                curClassType.ctor(instObj, ...)
            end

        end

        -- 调用递归
        CallCtor(class_type, ...)

        -- 设置元表
        setmetatable(instObj, { __index = class_type })

        return instObj
    end

    setmetatable(class_type, { __index = baseClass })
    return class_type 
end

function handler(method, obj)
    return function(...)
        return method(obj, ...)
    end
end

--  可以用来判断对象是否为空，包括unity的对象，引用的对象如果被destory 可以通过此接口判断
function IsNull(val)
  return val == nil or tolua.isnull(val)
end

function string.join(strArr, delimiter)
    delimiter = tostring(delimiter)
    if (delimiter=='') then return false end

    local str = ""
    for k,v in pairs(strArr) do
        if v ~= nil and v ~= '' then
            str = str .. delimiter .. tostring(v)
        end
    end

    str = string.sub(str, string.len(delimiter) + 1)
    return str
end