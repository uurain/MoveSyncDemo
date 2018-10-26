--主入口函数。从这里开始lua逻辑
function Main()					
	print("logic start")	 		
end

--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end

function Update(deltatime, unscaledDeltaTime)
	-- log("update:"..deltatime)
    Time:SetDeltaTime(deltatime, unscaledDeltaTime)

    -- if BattleManager ~= nil then
    --     BattleManager.Update(deltatime, unscaledDeltaTime)
    -- end

    -- if Network ~= nil then
    --     Network.OnGameHeart()
    -- end
end

function LateUpdate()
    -- CoUpdateBeat()
end