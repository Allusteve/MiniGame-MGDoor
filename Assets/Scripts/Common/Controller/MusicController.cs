using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private List<AudioSource> usedAudioSourceList;
    private List<AudioSource> freeAudioSourceList;
    private Dictionary<string, AudioClip> audioClipList;
    public const int AUDIO_SOURCE_MAX_COUNT = 8;

    void Start()
    {
        usedAudioSourceList = new List<AudioSource>();
        freeAudioSourceList = new List<AudioSource>();
        audioClipList = new Dictionary<string, AudioClip>();

        for (int i = 0; i < AUDIO_SOURCE_MAX_COUNT; ++i)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.enabled = true;
            audioSource.mute = true;
            audioSource.loop = false;
            freeAudioSourceList.Add(audioSource);
        }

        Initialize();
    }

    public void PlayAudio(string audioClipName, float audioVolume)
    {
        foreach (AudioSource audioSource in usedAudioSourceList)
        {
            if (audioSource.clip.name.Equals(audioClipName))
            {
                audioSource.mute = false;
                audioSource.Play();
                return;
            }
        }

        if (freeAudioSourceList.Count == 0)
        {
            Debug.Log("音源不足");
            return;
        }

        freeAudioSourceList[0].clip = audioClipList[audioClipName];
        freeAudioSourceList[0].mute = false;
        freeAudioSourceList[0].volume = audioVolume;
        freeAudioSourceList[0].Play();
        usedAudioSourceList.Add(freeAudioSourceList[0]);
        freeAudioSourceList.RemoveAt(0);
    }

    public void StopAudio(string audioClipName)
    {
        for (int i = 0; i < usedAudioSourceList.Count; ++i)
        {
            if (usedAudioSourceList[i].clip.name.Equals(audioClipName))
            {
                usedAudioSourceList[i].mute = true;
                usedAudioSourceList[i].Stop();
                freeAudioSourceList.Add(usedAudioSourceList[i]);
                usedAudioSourceList.RemoveAt(i);
                break;
            }
        }
    }

    private void Initialize()
    {
        List<string> clipList = new List<string>()
        {
            "tyrant-by-kevin-macleod","the-lift-by-kevin-macleod",
            "composeSuccess","composeFail","openBag","useItem","meow","dogHowl","bowwow"
        };

        foreach (string str in clipList)
        {
            audioClipList.Add(str, Resources.Load<AudioClip>("Musics/" + str));
        }
    }
}
