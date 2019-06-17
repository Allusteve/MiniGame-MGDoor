using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBloodEvent : MGEvent
{
    private float ChangeValue;

    public static void ChangeBlood(MGEvent mgEvent)
    {
        ChangeBloodEvent changeEvent = (ChangeBloodEvent)mgEvent;

        LevelController levelController = GameObject.Find("Manager").GetComponent<LevelController>();
        if (!levelController.isPause)
        {
            GameObject.Find("Manager").GetComponent<LevelController>().PlayerBlood += changeEvent.ChangeValue;
        }
    }

    public ChangeBloodEvent(float changeValue,float startTime,float interval)
        : base(startTime, interval, -1, ChangeBlood, null)
    {
        ChangeValue = changeValue;
    }
}
