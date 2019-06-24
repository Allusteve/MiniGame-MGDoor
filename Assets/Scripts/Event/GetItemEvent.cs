using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItemEvent : MGEvent
{
    private ITEM_ID GetItemID;

    public static void GetItem(MGEvent mgEvent)
    {
        GetItemEvent itemEvent = (GetItemEvent)mgEvent;
        GameObject.Find("Manager").GetComponent<Inventory>().AddItem(itemEvent.GetItemID);
    }

    public GetItemEvent(ITEM_ID itemID) : base(MGEventManager.getInstance().currTime, 0, 1, GetItem, null)
    {
        GetItemID = itemID;
    }
}
