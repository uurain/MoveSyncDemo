using UnityEngine;
using System.Collections;

public class VoiceCached : MonoBehaviour {

    private AudioSource audioSrc;

	void Awake()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.playOnAwake = false;
    }

    public void play(AudioClip audioClip)
    {
        gameObject.SetActive(true);
        audioSrc.volume = 1;
        audioSrc.clip = audioClip;
        audioSrc.Play();

        Invoke("cached", audioClip.length + 0.1f);
    }

    void cached()
    {
        gameObject.SetActive(false);
        VoiceManager.Instance.cached(this);
    }
}
