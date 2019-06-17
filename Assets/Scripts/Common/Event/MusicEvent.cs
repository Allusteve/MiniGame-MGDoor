using UnityEngine;

public class MusicEvent : MGEvent
{
    private string audioClipName;
    private float audioClipVolume;

    public static void PlayAudio(MGEvent mgEvent)
    {
        MusicEvent musicEvent = (MusicEvent)mgEvent;
        GameObject.Find("Manager").GetComponent<MusicController>().PlayAudio(musicEvent.audioClipName,musicEvent.audioClipVolume);
    }

    public static void StopAudio(MGEvent mgEvent)
    {
        MusicEvent musicEvent = (MusicEvent)mgEvent;
        GameObject.Find("Manager").GetComponent<MusicController>().StopAudio(musicEvent.audioClipName);
    }

    // 构造时记得将remainTime设为音效长度
    public MusicEvent(string clipName,float audioVolume,float startTime,float remainTime,int loopNum)
        : base(startTime, remainTime, loopNum, PlayAudio, StopAudio)
    {
        audioClipName = clipName;
        audioClipVolume = audioVolume;
    }
}
