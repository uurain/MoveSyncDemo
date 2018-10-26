using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using LuaFramework;

namespace pg
{
    public delegate void LoadCompleteCallback(NTexture texture);
    public delegate void LoadErrorCallback(string error);

    /// <summary>
    /// Use to load icons from asset bundle, and pool them
    /// </summary>
    public class IconManager : SingetonMono<IconManager>
    {
        public const int POOL_CHECK_TIME = 30;
        public const int MAX_POOL_SIZE = 10;

        private Dictionary<string, NTexture> texCached = new Dictionary<string, NTexture>();

        void Awake()
        {
            gameObject.name = "IconManager";
        }

        public void LoadIcon(string url,
                        LoadCompleteCallback onSuccess,
                        LoadErrorCallback onFail)
        {
            if(texCached.ContainsKey(url))
            {
                NTexture texture = texCached[url];
                texture.refCount++;
                if (onSuccess != null)
                    onSuccess(texture);
                return;
            }
            AppFacade.Instance.GetManager<ResManager>(ManagerName.Resource).Load(url, abInfo =>
            {
                NTexture texture = null;
                if (texCached.ContainsKey(url))
                {
                    texCached[url].refCount++;
                    texture = texCached[url];
                }
                else
                {
                    if (abInfo == null)
                    {
                        if (onFail != null)
                            onFail("");
                        return;
                    }
                    texture = new NTexture((Texture2D)abInfo.mainObject);
                    texture.refCount++;
                    texCached[url] = texture;
                }
                if (onSuccess != null)
                    onSuccess(texture);
            });
        }        

        public void CheckFree()
        {
            List<string> removeList = new List<string>();
            foreach(var val in texCached)
            {
                if(val.Value.refCount == 0)
                {
                    val.Value.Dispose();
                    removeList.Add(val.Key);
                }
            }
            for(int i = 0; i < removeList.Count; ++i)
            {
                texCached.Remove(removeList[i]);
            }
        }
    }
}
