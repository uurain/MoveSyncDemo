using UnityEngine;
using FairyGUI;
using System.IO;

namespace pg
{
    /// <summary>
    /// Extend the ability of GLoader
    /// </summary>
    public class MyGLoader : GLoader
    {
        private bool beDestroy = false;
        protected override void LoadExternal()
        {
            beDestroy = false;
            IconManager.GetInstance().LoadIcon(this.url, OnLoadSuccess, OnLoadFail);
        }

        protected override void FreeExternal(NTexture texture)
        {
            beDestroy = true;
            texture.refCount--;
        }

        void OnLoadSuccess(NTexture texture)
        {
            if (string.IsNullOrEmpty(this.url))
                return;
            if (beDestroy)
            {
                texture.refCount--;
                return;
            }
            this.onExternalLoadSuccess(texture);
        }

        void OnLoadFail(string error)
        {
            Debug.LogError("load " + this.url + " failed: " + error);
            this.onExternalLoadFailed();
        }
    }
}