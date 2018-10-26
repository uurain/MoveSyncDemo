using UnityEngine;
using System.Collections;
using UnityEditor;

public class AudioTo2D : AssetPostprocessor {
		public void OnPreprocessAudio () {
            AudioImporter audioImporter = assetImporter as AudioImporter;
            audioImporter.threeD = false;
            //Debug.Log("set "+assetPath + " to 2D audio.");
		}

    void OnPreprocessAnimation()
    {
        var modelImporter = assetImporter as ModelImporter;
        if (modelImporter.clipAnimations.Length <= 0 && modelImporter.defaultClipAnimations.Length > 0)
        {
            modelImporter.clipAnimations = modelImporter.defaultClipAnimations;
            modelImporter.SaveAndReimport();
        }
        //modelImporter.SaveAndReimport();
        //Debug.Log("set " + assetPath + " to OnPreprocessAnimation.");
    }
}
