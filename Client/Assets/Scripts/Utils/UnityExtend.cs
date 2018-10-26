using UnityEngine;
using System.Collections.Generic;
using System;

public static class UnityExtend
{
    /// <summary>
    /// 改变世界坐标X
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="x"></param>
    public static void SetPositionX(this Transform trans,float x)
    {
        Vector3 pos = trans.position;
        pos.x = x;
        trans.position = pos;
    }

    /// <summary>
    /// 改变世界坐标Y
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="y"></param>
    public static void SetPositionY(this Transform trans,float y)
    {
        Vector3 pos = trans.position;
        pos.y = y;
        trans.position = pos;
    }

    /// <summary>
    /// 改变世界坐标Z
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="z"></param>
    public static void SetPositionZ(this Transform trans, float z)
    {
        Vector3 pos = trans.position;
        pos.z = z;
        trans.position = pos;
    }

    /// <summary>
    /// 改变本地坐标X
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="x"></param>
    public static void SetLocalPositionX(this Transform trans, float x)
    {
        Vector3 pos = trans.localPosition;
        pos.x = x;
        trans.localPosition = pos;
    }

    /// <summary>
    /// 改变本地坐标Y
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="y"></param>
    public static void SetLocalPositionY(this Transform trans, float y)
    {
        Vector3 pos = trans.localPosition;
        pos.y = y;
        trans.localPosition = pos;
    }

    /// <summary>
    /// 改变本地坐标Z
    /// </summary>
    /// <param name="trans"></param>
    /// <param name="z"></param>
    public static void SetLocalPositionZ(this Transform trans, float z)
    {
        Vector3 pos = trans.localPosition;
        pos.z = z;
        trans.localPosition = pos;
    }

    public static void SetLocalSacelX(this Transform trans,float x)
    {
        Vector3 scale = trans.localScale;
        scale.x = x;
        trans.localScale = scale;
    }

    public static void SetLocalSacelY(this Transform trans, float y)
    {
        Vector3 scale = trans.localScale;
        scale.y = y;
        trans.localScale = scale;
    }

    public static void SetLocalSacelZ(this Transform trans, float z)
    {
        Vector3 scale = trans.localScale;
        scale.z = z;
        trans.localScale = scale;
    }

    public static void SetActiveEx(this GameObject go,bool active)
    {
        if(go != null)
        {
            go.SetActive(active);
        }
    }


    public static float DisSqrOnlyXZ(this Vector3 vec)
    {
        return vec.x * vec.x + vec.z * vec.z;
    }

    public static float DistanceOnlyXZ(this Vector3 vec)
    {
        return Mathf.Sqrt(vec.DisSqrOnlyXZ());
    }

    public static void SetTransformValue(this Transform self, Transform tran)
    {
        self.position = tran.position;
        self.rotation = tran.rotation;
        self.localScale = tran.localScale;
    }

    public static void SetParentAndInitPos(this Transform self, Transform parent)
    {
        self.parent = parent;
        self.localPosition = Vector3.zero;
        self.localScale = Vector3.one;
    }

    public static bool GetBool(this int intValue)
    {
        return intValue > 0;
    }

    public static Transform GetChildTransform(this GameObject objs, string name)
    {
        Transform[] trans = objs.GetComponentsInChildren<Transform>();
        for (int i = 0; i < trans.Length; i++)
        {
            if (trans[i].name == name)
            {
                return trans[i];
            }
        }
        return null;
    }

    /// <summary>
    /// key如果是类，请重载操作符
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    /// <param name="dic"></param>
    /// <param name="t"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public static bool TryAddValue<T, V>(this Dictionary<T, V> dic, T key, V value,bool overwrite = true)
    {
        if (dic.ContainsKey(key))
        {
            if(overwrite)
            {
                dic[key] = value;
            }
            
            return false;
        }

        dic.Add(key, value);
        return true;
    }

    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static int ToInt(this string str)
    {
        int value = 0;
        int.TryParse(str, out value);

        return value;
    }


    public static T GetValue<T>(this string str)
    {
        object Value = null;
        Type type = typeof(T);
        if(type == typeof(int))
        {
            int value = 0;
            int.TryParse(str, out value);
            Value = value;
        }
        else if(type == typeof(float))
        {
            float value = 0;
            float.TryParse(str, out value);
            Value = value;
        }
        else if(type == typeof(byte))
        {
            byte value = 0;
            byte.TryParse(str, out value);
            Value = value;
        }

        return (T)Value;
    }

    public static T AddUniqueCompoment<T>(this GameObject obj) where T : Component
    {
        T ins = obj.GetComponent<T>();
        if (ins == null)
        {
            ins = obj.AddComponent<T>();
        }
        return ins;
    }

    public static AudioSource AddAudioSource(this GameObject go)
    {
        AudioSource audio  = go.GetComponent<AudioSource>();
        if(audio == null)
        {
            audio = go.AddComponent<AudioSource>();
            audio.playOnAwake = false;
            audio.pitch = 1;
        }
        audio.volume = 1;

#if MusicMute && UNITY_EDITOR
        audio.volume = 0;
#endif
        return audio;
    }
}
