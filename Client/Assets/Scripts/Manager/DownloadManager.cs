using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DownloadManager : Manager
{
    private const int TimeOut = 5;
    private const int MaxDownloadNumber = 5;

    private List<string> ListidleUrl;
    private List<string> ListDownloadIng;

    private static DownloadManager Sole;
    public static DownloadManager Instance
    {
        get
        {
            return Sole;
        }
    }
    // Use this for initialization
    private void Awake()
    {
        Sole = this;
        ListidleUrl = new List<string>();
        ListDownloadIng = new List<string>();
    }
    void Start()
    {

        StartCoroutine(SetList());
    }
    private IEnumerator SetList()
    {
        while (true)
        {
            if (ListDownloadIng.Count <MaxDownloadNumber)
            {
                if (ListidleUrl.Count > 0)
                {
                    string StrThisUrl = ListidleUrl[0];
                    ListDownloadIng.Add(StrThisUrl);
                    ListidleUrl.RemoveAt(0);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void DownloadFile(string url, System.Action<string, byte[], bool, string> OverFun, bool IsInsert)
    {
        StartCoroutine(IEDownloadFile(url, 1, delegate (object obj, bool IsError, string Error)
          {
              byte[] tmpBytes = obj as byte[];
              if (OverFun != null)
                  OverFun(url, tmpBytes, IsError, Error);

          }, IsInsert));

    }
    public void DownloadFile(string url, System.Action<string, AssetBundle, bool, string> OverFun, bool IsInsert)
    {

        StartCoroutine(IEDownloadFile(url, 2, delegate (object obj, bool IsError, string Error)
        {
            AssetBundle tmpAB = obj as AssetBundle;
            if (OverFun != null)
                OverFun(url, tmpAB, IsError, Error);

        }, IsInsert));
    }
    public void DownloadFile(string url, System.Action<string, string, bool, string> OverFun, bool IsInsert)
    {
        StartCoroutine(IEDownloadFile(url, 3, delegate (object obj, bool IsError, string Error)
        {
            string tmpStr = obj as string;
            if (OverFun != null)
                OverFun(url, tmpStr, IsError, Error);
        }, IsInsert));


    }
    public void DownloadFile(string url, System.Action<string, AudioClip, bool, string> OverFun, bool IsInsert)
    {
        StartCoroutine(IEDownloadFile(url, 4, delegate (object obj, bool IsError, string Error)
        {
            AudioClip tmpAudioClip = obj as AudioClip;
            if (OverFun != null)
                OverFun(url, tmpAudioClip, IsError, Error);
        }, IsInsert));

    }
    public void DownloadFile(string url, System.Action<string, Texture2D,bool,string> OverFun, bool IsInsert)
    {
        StartCoroutine(IEDownloadFile(url, 5, delegate (object obj, bool IsError, string Error)
        {
            Texture2D tmpTexture = obj as Texture2D;
            if (OverFun != null)
                OverFun(url, tmpTexture, IsError, Error);
        }, IsInsert));

    }

    private IEnumerator IEDownloadFile(string url, int Index, System.Action<object, bool, string> OverFun, bool IsInsert)
    {
        if (url != null && !ListidleUrl.Contains(url))
        {
            if (IsInsert) ListidleUrl.Insert(0, url);
            else ListidleUrl.Add(url);
        }
        while (!ListDownloadIng.Contains(url))
            yield return 0;
        object obj = null;
        UnityWebRequest WebRequest = UnityWebRequest.Get(url);
        WebRequest.timeout = TimeOut;
        yield return WebRequest.Send();
        if (WebRequest.isError)
        {
            if (OverFun != null)
                OverFun(obj, true, WebRequest.error);
            yield break;
        }
        switch (Index)
        {
            case 1:
                obj = WebRequest.downloadHandler.data;
                break;
            case 2:
                obj = DownloadHandlerAssetBundle.GetContent(WebRequest);
                break;
            case 3:
                obj = DownloadHandlerBuffer.GetContent(WebRequest);
                break;
            case 4:
                obj = DownloadHandlerAudioClip.GetContent(WebRequest);
                break;
            case 5:
                obj = DownloadHandlerTexture.GetContent(WebRequest);
                break;
        }
        WebRequest.Dispose();
        ListDownloadIng.Remove(url);
        if (OverFun != null)
            OverFun(obj, false, null);
    }



    // Update is called once per frame
    void Update()
    {

    }
}
