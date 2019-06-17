using System.Collections.Generic;
using UnityEngine;

public class MGEventManager
{
    public float currTime;
    private List<MGEvent> eventList;

    private static MGEventManager instance;

    private MGEventManager()
    {
        
    }

    public static MGEventManager getInstance()
    {
        if (instance == null)
            instance = new MGEventManager();
        return instance;
    }

    public void Initialize()
    {
        currTime = 0.0f;
        eventList = new List<MGEvent>();
    }

    public void AddEvent(MGEvent mgEvent)
    {
        eventList.Add(mgEvent);
    }

    public void Update()
    {
        currTime += Time.deltaTime;

        int iter = 0;
        while (iter < eventList.Count)
        {
            if (eventList[iter].loopNum == 0)
            {
                eventList.RemoveAt(iter);
                continue;
            }
            else if (currTime>= eventList[iter].startTime)
            {
                if (!eventList[iter].isExecuted)
                {
                    eventList[iter].Start();
                    eventList[iter].isExecuted = true;
                }
                else if (currTime>= eventList[iter].startTime+ eventList[iter].remainTime)
                {
                    eventList[iter].Finish();
                    eventList[iter].isExecuted = false;
                    eventList[iter].startTime = currTime;
                    --eventList[iter].loopNum;
                }
            }
            ++iter;
        }
    }
}
