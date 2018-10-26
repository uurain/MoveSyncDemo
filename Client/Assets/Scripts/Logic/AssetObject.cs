using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class AssetObject : CachedMonoBehaviour
{
    private static Dictionary<string, int> _gAssetBundleRef = new Dictionary<string, int>();
    private List<Transform> _transforms = new List<Transform>();
    private TrailRenderer[] _trailRenders;

    public bool inited { get; private set; }
    public string bundleName { get; private set; }
    public string assetName { get; private set; }
    private Vector3 _originalScale = Vector3.one;
    public List<Transform> Transforms { get { return _transforms; } }

    public string key { get; set; }

    protected virtual void Awake()
    {
        var t = transform.Find("assetBundleName");
        var t2 = transform.Find("assetName");
        if (t != null && t2 != null)
        {
            Init(t.GetChild(0).name, t2.GetChild(0).name, transform.localScale);
        }
        _trailRenders = gameObject.GetComponentsInChildren<TrailRenderer>(true);
        gameObject.GetComponentsInChildren<Transform>(_transforms);
    }

    public void ResetLocalScale()
    {
        this.transform.localScale = this._originalScale;
    }
    protected virtual void OnEnable()
    {
        if (_trailRenders != null)
        {
            foreach (var tr in _trailRenders)
            {
                tr.Clear();
            }
        }
    }

    public AssetObject Init(string bundleName, string assetName, Vector3 bundleScale)
    {
        if (!inited)
        {
            inited = true;
            this.bundleName = bundleName;
            this.assetName = assetName;
            this._originalScale = bundleScale;
        }
        return this;
    }

    public GameObject GetNode(string name)
    {
        foreach (var trans in _transforms)
        {
            if (trans != null)
            {
                if (trans.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    if (trans.IsChildOf(gameObject.transform))
                    {
                        return trans.gameObject;
                    }
                    else
                    {
                        Debug.LogError("transform not a child of root");
                    }
                    break;
                }
            }
            else
            {
                Debug.LogError("transform has been destroyed");
            }
        }

        return null;
    }
}
