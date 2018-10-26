using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;


public class DestroyCallBack : MonoBehaviour
{
    public float TimeOut = 1;
    public string Key = "";

    public delegate void DestroyDelegate(string key, GameObject go);

    public DestroyDelegate destroyDelegate = null;

    public LuaFunction DestroyDelegateLua = null;

    void Start()
    {

    }

    public void BeginNow()
    {
        CancelInvoke("DestroyBack");
        Invoke("DestroyBack", TimeOut);
    }

    public void BeginImmediately()
    {
        CancelInvoke("DestroyBack");
        DestroyBack();
    }

    public void DestroyBack()
    {
        if (null == destroyDelegate && null == DestroyDelegateLua)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            if (DestroyDelegateLua != null)
                DestroyDelegateLua.Call(Key, gameObject);
            else
                destroyDelegate(Key, gameObject);
        }
    }
}
