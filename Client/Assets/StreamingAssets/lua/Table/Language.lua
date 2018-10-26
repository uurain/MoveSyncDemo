Language = {}

local _languageTable = {
    global_text1 = "系统错误",
    global_text2 = "功能暂未开放，敬请期待",
    global_text3 = "无",
}

function Language.Get(ident)
	local text = _languageTable[ident]
	if text == nil then
		text = ""
	end
	return text
end
