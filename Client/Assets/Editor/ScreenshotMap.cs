using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScreenshotMap : MonoBehaviour
{

    [MenuItem("Custom/ScreenshotMap")]
    static void Begin()
    {
        GameObject leftDown = GameObject.Find("left_down_pt");
        GameObject rightUp = GameObject.Find("right_up_pt");
        if (leftDown == null || rightUp == null)
            return;
        Camera cam = null;
        GameObject camGo = GameObject.Find("ScreenShotCam");
        if(camGo == null)
        {
            camGo = new GameObject("ScreenShotCam");
            camGo.AddComponent<Camera>();
            Debug.Log("can't find cam go");
        }
        cam = camGo.GetComponent<Camera>();
        cam.orthographic = true;
        camGo.transform.position = new Vector3((rightUp.transform.position.x + leftDown.transform.position.x)/2, 100, (rightUp.transform.position.z + leftDown.transform.position.z) / 2);
        camGo.transform.eulerAngles = new Vector3(90, 0, 0);
        int rangeX = Mathf.CeilToInt( rightUp.transform.position.x - leftDown.transform.position.x);
        int rangeZ = Mathf.CeilToInt(rightUp.transform.position.z - leftDown.transform.position.z);
        cam.orthographicSize = rangeZ / 2;

        rangeX *= 10;
        rangeZ *= 10;
        RenderTexture rt = new RenderTexture(rangeX, rangeZ, 24);

        cam.targetTexture = rt;
        Texture2D screenShot = new Texture2D(rangeX, rangeZ, TextureFormat.RGB24, false);
        cam.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, rangeX, rangeZ), 0, 0);
        cam.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        DestroyImmediate(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = "test.jpg";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));
    }
}
