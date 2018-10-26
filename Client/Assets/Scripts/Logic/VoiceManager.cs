using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VoiceManager : SingetonMono<VoiceManager>
{
    private List<VoiceCached> chatVoiceList = new List<VoiceCached>();

    public void playVoice(string audioPath)
    {
        Object voiceObj = Resources.Load(audioPath);
        if (voiceObj != null)
        {
            VoiceCached voiceCached = getCached();
            voiceCached.play(voiceObj as AudioClip);
        }
    }

    public VoiceCached getCached()
    {
        VoiceCached voiceCache = null;
        if (chatVoiceList.Count > 0)
        {
            voiceCache = chatVoiceList[0];
            chatVoiceList.RemoveAt(0);
        }
        if (voiceCache == null)
        {
            GameObject obj = new GameObject("voice");
            voiceCache = obj.AddComponent<VoiceCached>();
            voiceCache.transform.parent = transform;
        }
        return voiceCache;
    }

    public void cached(VoiceCached voiceCache)
    {
        chatVoiceList.Add(voiceCache);
    }
}