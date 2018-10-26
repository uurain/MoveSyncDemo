using UnityEngine;
using System.Collections;

public class SaveDataManager : Manager
{

    public SaveDataManager()
    {

    }

    public float getFloat(string key, float defaultVal = 0.0f)
    {
        return PlayerPrefs.GetFloat(key, defaultVal);
    }

    public int getInt(string key, int defaultVal = 0)
    {
        return PlayerPrefs.GetInt(key, defaultVal);
    }

    public string getString(string key, string defaultVal = "")
    {
        return PlayerPrefs.GetString(key, defaultVal);
    }

    public bool getBool(string key, bool defaultVal = false)
    {
        return getInt(key, defaultVal ? 1 : 0) > 0;
    }

    public void setFloat(string key, float val)
    {
        PlayerPrefs.SetFloat(key, val);
    }

    public void setInt(string key, int val)
    {
        PlayerPrefs.SetInt(key, val);
    }

    public void setString(string key, string val)
    {
        PlayerPrefs.SetString(key, val);
    }

    public void setBool(string key, bool val)
    {
        setInt(key, val ? 1 : 0);
    }

    public void save()
    {
        PlayerPrefs.Save();
    }
}
