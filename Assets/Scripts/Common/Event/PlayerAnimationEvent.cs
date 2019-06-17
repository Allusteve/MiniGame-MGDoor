using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MGEvent
{
    private string AnimationStr;
    GameObject player;

    public static void startAnimation(MGEvent mgEvent)
    {

        PlayerAnimationEvent animationEvent = (PlayerAnimationEvent)mgEvent;
        animationEvent.player = GameObject.Find("Player");
        switch (animationEvent.AnimationStr)
        {
            case "down":
                animationEvent.player.AddComponent<PlayerDownToFirstFloor>();
                break;
        }
    }

    public static void finishAnimation(MGEvent mgEvent)
    {
        PlayerAnimationEvent animationEvent = (PlayerAnimationEvent)mgEvent;
        switch (animationEvent.AnimationStr)
        {
            case "down":
                animationEvent.player.GetComponent<PlayerDownToFirstFloor>().destroy();
                MGUGUIUtility.Toast.showToast("累死我了……你想起了当初爬二十层楼梯脸不红气不喘的自己，可那已经是十几年前的事了……",
                                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                break;
        }

    }

    public PlayerAnimationEvent(string str,float animationTime)
        : base(MGEventManager.getInstance().currTime, animationTime, 1, startAnimation, finishAnimation)
    {
        AnimationStr = str;
    }
}
