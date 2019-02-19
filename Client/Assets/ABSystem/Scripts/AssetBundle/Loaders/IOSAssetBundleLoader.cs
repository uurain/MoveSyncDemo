using System.Collections;
using UnityEngine;

namespace Tangzx.ABSystem
{
    /// <summary>
    /// 在IOS下的加载
    /// 注意：
    /// IOS下加载可以进行优化：直接在raw目录里进行File读取
    /// </summary>
    public class IOSAssetBundleLoader : MobileAssetBundleLoader
    {
        protected override IEnumerator LoadFromPackage()
        {
            //兼容低版本API
#if UNITY_4 || UNITY_4_6 || UNITY_5_1 || UNITY_5_2
            _bundle = AssetBundle.CreateFromFile(_assetBundleSourceFile);
            yield return null;
#else
            _assetBundleSourceFile = bundleManager.pathResolver.GetBundleSourceFile(bundleName, false);
            AssetBundleCreateRequest req = AssetBundle.LoadFromFileAsync(_assetBundleSourceFile);
            yield return req;
            _bundle = req.assetBundle;
#endif
            if(loadAsset)
            {
#if UNITY_5 || UNITY_2018
                string[] names = _bundle.GetAllAssetNames();
                if (names.Length > 0)
                {
                    AssetBundleRequest req2 = _bundle.LoadAssetAsync(names[0]);
                    yield return req2;
                    loadedAsset = req2.asset;
                }
#else
                    loadedAsset = _bundle.mainAsset;
#endif
            }
            this.Complete();
        }
    }
}