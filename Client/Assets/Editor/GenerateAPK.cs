using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



public class GenerateAPK : MonoBehaviour {

    [MenuItem("Custom/GenerateAPK")]
    static void OnGenerateAPK()
    {
		string szApkName = PlayerSettings.productName + "_" + System.DateTime.Now.ToString("MMdd-hhmm") +  ".apk";
		Generate (BuildTarget.Android, szApkName);

        System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
        psi.Arguments = "/e,/select," + szApkName;
        System.Diagnostics.Process.Start(psi);
    }

	[MenuItem("Custom/GenerateXcode")]
	static void OnGenerateXcode()
	{
		string szApkName = PlayerSettings.productName + "_" + System.DateTime.Now.ToString("MMdd-hhmm") +  "_xcode";
		Generate (BuildTarget.iOS, szApkName);
	}

	static void Generate(BuildTarget target, string locationPathName)
	{
		BuildOptions op = BuildOptions.None;

		List<string> allScene = new List<string>();
		allScene.Add("Assets/Scenes/main.unity");
		allScene.Add("Assets/Scenes/empty.unity");

		PlayerSettings.productName = "boom";
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "AB_MODE");
		PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, "AB_MODE");
		BuildPipeline.BuildPlayer(allScene.ToArray(), locationPathName, target, op);
	}
}
