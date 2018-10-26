---
--- Created by Administrator.
--- DateTime: 2017/8/16 15:33
---
TimeMgr = {}

local _date = os.date

--转换为显示时间
function TimeMgr.ConvertToDateTime(t)
    return _date("%Y-%m-%d %H:%M:%S",t/1000)
end

function TimeMgr:ConvertToMinTime(t)
    return _date("%M:%S",t)
end

--转换成时间戳
function TimeMgr.ConvertToTimeStamp(year,month,day,hour,min,sec)
    local time = {day=1, month=1, year=1970, hour=0, minute=0, second=0}
    if year ~= nil then
        time.year = year
    end
    if month ~= nil then
        time.month = month
    end
    if day ~= nil then
        time.day = day
    end
    if hour ~= nil then
        time.hour = hour
    end
    if min ~= nil then
        time.minute = min
    end
    if sec ~= nil then
        time.second = sec
    end
    return os.time(time)
end

TimeMgr.SERVER_TIME_DIFF = 0
TimeMgr.SERVER_TIME_FLOAT = 0

--时间戳转换table
function TimeMgr.ConvertTimeStampToTable(timeInt)
    local dt = {}
    dt.year = _date("%Y", timeInt)
    dt.month = _date("%m", timeInt)
    dt.day = _date("%d", timeInt)
    dt.hour = _date("%H", timeInt)
    dt.minute = _date("%M", timeInt)
    dt.second = _date("%S", timeInt)
    dt.weekday = _date("%w", timeInt)
    return dt
end

function TimeMgr.GetServerTime()
    return TimeMgr.ConvertToDateTime(TimeMgr.GetServerTimeInt())
end

function TimeMgr.GetServerTimeInt()
    local timeStamp = os.time() + TimeMgr.SERVER_TIME_DIFF
    return timeStamp
end

--获取当前周开始时间戳
function TimeMgr.GetCurWeekStartStamp()
    local now = TimeMgr.GetServerTimeInt()
    local dt = TimeMgr.ConvertTimeStampToTable(now)
    dt.hour = 0
    dt.minute = 0
    dt.second = 0
    local timeStamp = os.time(dt)

    local wday = dt.weekday - 1
    if wday < 0 then
        wday = 6
    end

    timeStamp = timeStamp - wday * 86400
    return timeStamp
end

--获取今日0定时间戳
function TimeMgr.GetTodayStartStamp()
    local now = TimeMgr.GetServerTimeInt()
    local dt = TimeMgr.ConvertTimeStampToTable(now)
    dt.hour = 0
    dt.minute = 0
    dt.second = 0
    local timeStamp = os.time(dt)
    return timeStamp
end

--获取当天周几索引
function TimeMgr.GetTodayWeekDayIdx()
    return _date("%w", TimeMgr.GetServerTimeInt())
end

--获取当前时间戳，从当日0点开始算
function TimeMgr.GetTodayTimeInt()
    return TimeMgr.GetServerTimeInt() - TimeMgr.GetTodayStartStamp()
end

--秒数转换成周时间戳
function TimeMgr.ConvertWeekTimeStamp(timeInt)
    local wStart = TimeMgr.GetCurWeekStart()
    return wStart + timeInt
end

--转换为时间格式
function TimeMgr.ConverToTimeFromSecond(second)
    second = tonumber(second)
    local sec = math.fmod(second,60)
    local min = math.fmod(second - sec,3600) / 60
    local H = (second - min * 60 - sec) / 3600
    return string.format("%02d:%02d:%02d",H,min,sec)
end

--获取周几中文
function TimeMgr.GetWeekDayStr(wd)
    local weekday = tonumber(wd)
    if weekday == 1 then
        return Language.Get("timemgr_text1")
    elseif weekday == 2 then
        return Language.Get("timemgr_text2")
    elseif weekday == 3 then
        return Language.Get("timemgr_text3")
    elseif weekday == 4 then
        return Language.Get("timemgr_text4")
    elseif weekday == 5 then
        return Language.Get("timemgr_text5")
    elseif weekday == 6 then
        return Language.Get("timemgr_text6")
    else
        return Language.Get("timemgr_text7")
    end
end

--以;分割如0;1;3，代表周日，周一，周二,7天都有则显示每天
function TimeMgr.ConvertWeekDaysString(weekdays,splitChar)

    if splitChar == nil then
        splitChar = ","
    end

    local days = string.split(weekdays, ';')
    if #days == 7 then
        return Language.Get("timemgr_text8")
    end
    local wds = {}
    for k,v in ipairs(days) do
        table.insert(wds, TimeMgr.GetWeekDayStr(v))
    end

    return string.join(wds,',')
end

--将秒数时间段转换成时间，如0:86400 转换成00:00:00-24:00:00,可以多个用;分割
function TimeMgr.ConvertTimeString(openTimeStrs,splitChar)
    if splitChar == nil then
        splitChar = " "
    end

    local times = DBLogic.ConverSplitTable(openTimeStrs)
    local strs = {}
    for k,v in ipairs(times) do
        local startTime = v[1]
        local endTime = v[2]
        if startTime ~= nil and endTime ~= nil then
            local openTimeS = TimeMgr.ConverToTimeFromSecond(startTime)
            local endTimeS = TimeMgr.ConverToTimeFromSecond(endTime)
            table.insert(strs, openTimeS .. "-" ..endTimeS)
        end
    end

    if openTimeStrs == "0:86400" then
        return Language.Get("timemgr_text9")
    end

    return string.join(strs, splitChar)
end

local DelayTimers = {}
local TimerStartId = 0

function TimeMgr.DelayDo(func,delay)
    if func ~= nil and delay ~= nil then
        TimerStartId = TimerStartId + 1
        local timerId = TimerStartId
        local timer = Timer.New(function()
            DelayTimers[timerId].func()
            DelayTimers[timerId] = nil
        end, delay, false, false)
        DelayTimers[timerId] = {}
        DelayTimers[timerId].func = func
        DelayTimers[timerId].timer = timer
        timer:Start()
    end
end
