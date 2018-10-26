using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraShakeData
{
    public int m_Index;
    public string m_Name;
    public List<string> m_CameraTags = new List<string>();
    public string m_ShakeType;
    public int m_NumOfShakes;
    public Vector3 m_ShakeAmount = Vector3.one / 2;
    public Vector3 m_RotationAmount = Vector3.one / 2;
    public float m_Distance = 00.10f;
    public float m_Speed = 50.00f;
    public float m_Decay = 00.20f;
    public float m_GuiShakeModifier = 01.00f;
    public int m_MultiplyByTimeScale = 1;

    public string Serialize()
    {
        string str = "index:" + m_Index.ToString() + "\n";
        string tags = "cameraTags:";
        for (int i = 0; i < m_CameraTags.Count; ++i)
        {
            tags += m_CameraTags[i];
            if (i < m_CameraTags.Count - 1)
                tags += "|";
        }
        str += tags + "\n";
        str += "name:" + m_Name + "\n";
        str += "shakeType:" + m_ShakeType + "\n";
        str += "numOfShakes:" + m_NumOfShakes + "\n";
        str += "shakeAmount:" + m_ShakeAmount.x + "|" + m_ShakeAmount.y + "|" + m_ShakeAmount.z + "\n";
        str += "rotationAmount:" + m_RotationAmount.x + "|" + m_RotationAmount.y + "|" + m_RotationAmount.z + "\n";
        str += "distance:" + m_Distance + "\n";
        str += "speed:" + m_Speed + "\n";
        str += "decay:" + m_Decay + "\n";
        str += "guiShakeModifier:" + m_GuiShakeModifier + "\n";
        str += "timeScale:" + m_MultiplyByTimeScale.ToString() + "\n";
        return str;
    }
}
