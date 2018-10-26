using UnityEngine;
using System.Collections;
using FairyGUI;

namespace LuaFramework {

    /// <summary>
    /// </summary>
    public class Main : MonoBehaviour {

        public static string updatePer = "";

        void Start() {
            PgJoystick.GetInstance().SetCam(Camera.main.transform);
            GameObject.DontDestroyOnLoad(Camera.main.gameObject);
            InitUI();
            AppFacade.Instance.StartUp();   //启动游戏

            AppFacade.Instance.RegisterCommand(NotiConst.UPDATE_MESSAGE, typeof(UpdateCommand));

//#if UNITY_EDITOR
//            Application.targetFrameRate = 60;
//#else
//            Application.targetFrameRate = 30;
//#endif
        }

        void InitUI()
        {
            GRoot.inst.SetContentScaleFactor(1334, 750);
            UIConfig.defaultFont = "afont";
            FontManager.RegisterFont(FontManager.GetFont("txjlzy"), "Tensentype JiaLiZhongYuanJ");
            FontManager.RegisterFont(FontManager.GetFont("STXINWEI_1"), "华文新魏");

            UIObjectFactory.SetLoaderExtension(typeof(pg.MyGLoader));
        }

        //void OnGUI()
        //{
        //    GUI.Label(new Rect(100, Screen.height / 2, Screen.width - 100, 80), updatePer);
        //}
    }    
}

public class UpdateCommand : ControllerCommand
{
    public override void Execute(IMessage message)
    {
        string str = (string)message.Body;
        if ("解包完成!!!" == str)
        {
            LuaFramework.Main.updatePer = "";
        }
        else
            LuaFramework.Main.updatePer = str;
    }
}