using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedDogEvent : MGEvent
{
    public FeedDogEvent() : base(MGEventManager.getInstance().currTime, 1, 1, run, eat)
    {

    }

    public static void run(MGEvent mgEvent)
    {
        GameObject.Find("Dog").GetComponent<DogMovement>().OnRunning();
    }

    public static void eat(MGEvent mgEvent)
    {
        GameObject.Find("Dog").GetComponent<DogMovement>().OnEating();
    }

}
