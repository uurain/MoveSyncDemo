using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public class CameraShakeUtil
{
    public static CameraShakeUtil Instance
    {
        get
        {
            return instance;
        }
    }
    private static CameraShakeUtil instance = new CameraShakeUtil();
    private const string m_FileName = "AnimationCurveInfo";
    private static Dictionary<int, CameraShakeData> m_Curves;
    public  TextAsset textAsset;
    //public DownLoadResource tempDLR;
    List<CameraShakeData> list = new List<CameraShakeData>();

    // public void DoShake(GameObject target, int curveIndex, System.Action callback)
    // {

    //     if (m_Curves == null)
    //     {
    //         m_Curves = new Dictionary<int, CameraShakeData>();
    //         //List<CameraShakeData> datas = CameraShakeUtil.Instance.ReadCameraShakeData();
    //         ResourcesManager.Instance.StartLoadResource(m_FileName, delegate(DownloadItem item, object[] anyDataArray)
    //         {
    //             TextAsset curAsset = item.GetLoadedTextAsset();
    //             if (curAsset == null)
    //             {
    //                 Debug.LogError("not find File " + m_FileName);
    //                 return;
    //             }

    //             string text = curAsset.text;
    //             string[] lines = text.Split('\n');

    //             char[] trimStart = { ' ', '\t' };
    //             char[] trimEnd = { ' ', '\r', '\n', '\t' };

    //             CameraShakeData data = null;
    //             foreach (string content in lines)
    //             {
    //                 string strLine = content.TrimStart(trimStart);
    //                 strLine = content.TrimEnd(trimEnd);
    //                 if (strLine == string.Empty)
    //                     continue;
    //                 if (strLine.StartsWith("index"))
    //                     data = new CameraShakeData();
    //                 if (strLine.StartsWith("index"))
    //                     data.m_Index = int.Parse(strLine.Split(':')[1]);
    //                 else if (strLine.StartsWith("name"))
    //                     data.m_Name = strLine.Split(':')[1];
    //                 else if (strLine.StartsWith("cameraTags"))
    //                 {
    //                     List<string> tags = new List<string>();
    //                     string[] strs = (strLine.Split(':')[1]).Split('|');
    //                     for (int i = 0; i < strs.Length; ++i)
    //                         tags.Add(strs[i]);
    //                     data.m_CameraTags = tags;
    //                 }
    //                 else if (strLine.StartsWith("shakeType"))
    //                     data.m_ShakeType = strLine.Split(':')[1];
    //                 else if (strLine.StartsWith("numOfShakes"))
    //                     data.m_NumOfShakes = int.Parse(strLine.Split(':')[1]);
    //                 else if (strLine.StartsWith("shakeAmount"))
    //                 {
    //                     string[] strs = (strLine.Split(':')[1]).Split('|');
    //                     data.m_ShakeAmount = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
    //                 }
    //                 else if (strLine.StartsWith("rotationAmount"))
    //                 {
    //                     string[] strs = (strLine.Split(':')[1]).Split('|');
    //                     data.m_RotationAmount = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
    //                 }
    //                 else if (strLine.StartsWith("distance"))
    //                     data.m_Distance = float.Parse(strLine.Split(':')[1]);
    //                 else if (strLine.StartsWith("speed"))
    //                     data.m_Speed = float.Parse(strLine.Split(':')[1]);
    //                 else if (strLine.StartsWith("decay"))
    //                     data.m_Decay = float.Parse(strLine.Split(':')[1]);
    //                 else if (strLine.StartsWith("guiShakeModifier"))
    //                     data.m_GuiShakeModifier = float.Parse(strLine.Split(':')[1]);
    //                 else if (strLine.StartsWith("timeScale"))
    //                 {
    //                     data.m_MultiplyByTimeScale = int.Parse(strLine.Split(':')[1]);
    //                     list.Add(data);
    //                 }
    //             }

    //             for (int i = 0; i < list.Count; ++i)
    //                 m_Curves.Add(list[i].m_Index, list[i]);


    //             if (m_Curves.ContainsKey(curveIndex))
    //             {
    //                 data = m_Curves[curveIndex];
    //                 CameraShake.ShakeType shakeType = CameraShake.ShakeType.LocalPosition;
    //                 if (data.m_ShakeType == "CameraMatrix")
    //                     shakeType = CameraShake.ShakeType.CameraMatrix;
    //                 bool isScale = true;
    //                 if (data.m_MultiplyByTimeScale == 0)
    //                     isScale = false;
    //                 CameraShake comp = target.ForceGetComponent<CameraShake>();

    //                 comp.shakeType = shakeType;
    //                 comp.numberOfShakes = data.m_NumOfShakes;
    //                 comp.shakeAmount = data.m_ShakeAmount;
    //                 comp.rotationAmount = data.m_RotationAmount;
    //                 comp.distance = data.m_Distance;
    //                 comp.speed = data.m_Speed;
    //                 comp.decay = data.m_Decay;
    //                 comp.guiShakeModifier = data.m_GuiShakeModifier;
    //                 comp.multiplyByTimeScale = isScale;
    //                 comp.Shake();
    //             }

    //         }, null);
    //    }
    //     else
    //     {
    //         CameraShakeData data = null;
    //         if (m_Curves.ContainsKey(curveIndex))
    //         {
    //             data = m_Curves[curveIndex];
    //             CameraShake.ShakeType shakeType = CameraShake.ShakeType.LocalPosition;
    //             if (data.m_ShakeType == "CameraMatrix")
    //                 shakeType = CameraShake.ShakeType.CameraMatrix;
    //             bool isScale = true;
    //             if (data.m_MultiplyByTimeScale == 0)
    //                 isScale = false;
    //             CameraShake comp = target.ForceGetComponent<CameraShake>();

    //             comp.shakeType = shakeType;
    //             comp.numberOfShakes = data.m_NumOfShakes;
    //             comp.shakeAmount = data.m_ShakeAmount;
    //             comp.rotationAmount = data.m_RotationAmount;
    //             comp.distance = data.m_Distance;
    //             comp.speed = data.m_Speed;
    //             comp.decay = data.m_Decay;
    //             comp.guiShakeModifier = data.m_GuiShakeModifier;
    //             comp.multiplyByTimeScale = isScale;
    //             comp.Shake();
    //         }
    //     }
    //}

    public static void LoadCurves()
    {
        if (m_Curves == null)
        {
            m_Curves = new Dictionary<int, CameraShakeData>();
            List<CameraShakeData> datas = CameraShakeUtil.Instance.ReadCameraShakeData();
            for (int i = 0; i < datas.Count; ++i)
                m_Curves.Add(datas[i].m_Index, datas[i]);
        }
    }

    public static void WriteCameraShakeData(List<CameraShakeData> items)
    {
        string filePath = Application.dataPath + "/ArtEdit/AllResources/Table/Resources/" + m_FileName+".txt";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        StreamWriter sw = new StreamWriter(filePath);
        foreach (CameraShakeData item in items)
        {
            sw.WriteLine(item.Serialize());
        }
        sw.Close();
        sw.Dispose();
    }

    public List<CameraShakeData> ReadCameraShakeData()
    {
        List<CameraShakeData> list = new List<CameraShakeData>();

        TextAsset textAsset = (TextAsset)Resources.Load("AnimationCurveInfo");
        string text = textAsset.text;
        string[] lines = text.Split('\n');

        char[] trimStart = { ' ', '\t' };
        char[] trimEnd = { ' ', '\r', '\n', '\t' };

        CameraShakeData data = null;
        foreach (string item in lines)
        {
            string strLine = item.TrimStart(trimStart);
            strLine = item.TrimEnd(trimEnd);
            if (strLine == string.Empty)
                continue;
            if (strLine.StartsWith("index"))
                data = new CameraShakeData();
            if (strLine.StartsWith("index"))
                data.m_Index = int.Parse(strLine.Split(':')[1]);
            else if (strLine.StartsWith("name"))
                data.m_Name = strLine.Split(':')[1];
            else if (strLine.StartsWith("cameraTags"))
            {
                List<string> tags = new List<string>();
                string[] strs = (strLine.Split(':')[1]).Split('|');
                for (int i = 0; i < strs.Length; ++i)
                    tags.Add(strs[i]);
                data.m_CameraTags = tags;
            }
            else if (strLine.StartsWith("shakeType"))
                data.m_ShakeType = strLine.Split(':')[1];
            else if (strLine.StartsWith("numOfShakes"))
                data.m_NumOfShakes = int.Parse(strLine.Split(':')[1]);
            else if (strLine.StartsWith("shakeAmount"))
            {
                string[] strs = (strLine.Split(':')[1]).Split('|');
                data.m_ShakeAmount = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
            }
            else if (strLine.StartsWith("rotationAmount"))
            {
                string[] strs = (strLine.Split(':')[1]).Split('|');
                data.m_RotationAmount = new Vector3(float.Parse(strs[0]), float.Parse(strs[1]), float.Parse(strs[2]));
            }
            else if (strLine.StartsWith("distance"))
                data.m_Distance = float.Parse(strLine.Split(':')[1]);
            else if (strLine.StartsWith("speed"))
                data.m_Speed = float.Parse(strLine.Split(':')[1]);
            else if (strLine.StartsWith("decay"))
                data.m_Decay = float.Parse(strLine.Split(':')[1]);
            else if (strLine.StartsWith("guiShakeModifier"))
                data.m_GuiShakeModifier = float.Parse(strLine.Split(':')[1]);
            else if (strLine.StartsWith("timeScale"))
            {
                data.m_MultiplyByTimeScale = int.Parse(strLine.Split(':')[1]);
                list.Add(data);
            }
        }
        return list;
    }

    public void DoShake1(GameObject target, int curveIndex)
    {
        LoadCurves();
        if (m_Curves.ContainsKey(curveIndex))
        {
            CameraShakeData data = m_Curves[curveIndex];
            CameraShake.ShakeType shakeType = CameraShake.ShakeType.LocalPosition;
            if (data.m_ShakeType == "CameraMatrix")
                shakeType = CameraShake.ShakeType.CameraMatrix;
            bool isScale = true;
            if (data.m_MultiplyByTimeScale == 0)
                isScale = false;
            CameraShake comp = target.AddUniqueCompoment<CameraShake>();

            comp.shakeType = shakeType;
            comp.numberOfShakes = data.m_NumOfShakes;
            comp.shakeAmount = data.m_ShakeAmount;
            comp.rotationAmount = data.m_RotationAmount;
            comp.distance = data.m_Distance;
            comp.speed = data.m_Speed;
            comp.decay = data.m_Decay;
            comp.guiShakeModifier = data.m_GuiShakeModifier;
            comp.multiplyByTimeScale = isScale;
            comp.Shake();
        }
    }
}