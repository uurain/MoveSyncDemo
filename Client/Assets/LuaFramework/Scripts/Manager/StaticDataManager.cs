using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using LuaFramework;

public class StaticDataManager : Singleton<StaticDataManager>
{
    Dictionary<int, LuaTable> actorDic = new Dictionary<int, LuaTable>();
    Dictionary<int, LuaTable> skillDic = new Dictionary<int, LuaTable>();

    public void InitData()
    {
        Debug.Log("StaticDataManager init data");
        LuaTable actorLuaTable = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua).GetStaticDataTable("actorTable");
        actorLuaTable = (LuaTable)actorLuaTable["actor"];
        object[] actorList = actorLuaTable.ToArray();
        for (int i = 0; i < actorList.Length; ++i)
        {
            int actorId = System.Convert.ToInt32(((LuaTable)actorList[i])["id"]);
            actorDic.Add(actorId, (LuaTable)actorList[i]);
        }

        LuaTable skillLuaTable = AppFacade.Instance.GetManager<LuaManager>(ManagerName.Lua).GetStaticDataTable("skillTable");
        skillLuaTable = (LuaTable)skillLuaTable["skill"];
        object[] skillList = skillLuaTable.ToArray();
        for (int i = 0; i < skillList.Length; ++i)
        {
            int skillId = System.Convert.ToInt32(((LuaTable)skillList[i])["id"]);
            skillDic.Add(skillId, (LuaTable)skillList[i]);
        }
    }

    public LuaTable GetActorTable(int actorId)
    {
        LuaTable table = null;
        if(actorDic.TryGetValue(actorId, out table))
        {
            return table;
        }
        return null;
    }

    public LuaTable GetSkillTable(int skillId)
    {
        LuaTable table = null;
        if (skillDic.TryGetValue(skillId, out table))
        {
            return table;
        }
        return null;
    }

    void OnDestroy()
    {
        foreach(var val in actorDic)
        {
            val.Value.Dispose();
        }
        foreach (var val in skillDic)
        {
            val.Value.Dispose();
        }
    }
}

