using UnityEngine;
using System.Collections;
namespace dt
{
    public class fpsPrint : MonoBehaviour
    {
        /// <summary>
        /// 每次刷新计算的时间      帧/秒
        /// </summary>
        public float updateInterval = 1.0f;
        /// <summary>
        /// 最后间隔结束时间
        /// </summary>
        private double lastInterval;
        private int frames = 0;
        private float currFPS;

        string memoryStr;
        float getMemoryTimeDist;

        GUIStyle gStyle = new GUIStyle();

        // Use this for initialization
        //void Start()
        //{
        //    lastInterval = Time.realtimeSinceStartup;
        //    frames = 0;
        //    gStyle.fontSize = 50;
        //}


        //void Update()
        //{
        //    ++frames;
        //    float timeNow = Time.realtimeSinceStartup;
        //    if (timeNow > lastInterval + updateInterval)
        //    {
        //        currFPS = (float)(frames / (timeNow - lastInterval));
        //        frames = 0;
        //        lastInterval = timeNow;
        //    }
        //}
        //private void OnGUI()
        //{
        //    GUILayout.Label("FPS:" + currFPS.ToString("f2"), gStyle);

        //    //if (Game.isdbEditorMode != "")
        //    //{
        //    //    GUILayout.Label(Game.isdbEditorMode);
        //    //}  
        //}
    }
}