using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Hole : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            MGUGUIUtility.Toast.showToast("生命只有一次，游戏不是。大侠请重新来过！",
                    MGUGUIUtility.Toast.REMAIN_FOREVER, MGUGUIUtility.Toast.TOP_MSG);
            GameObject.Find("Manager").GetComponent<UIControllder>().ShowGameOverPage();
            Destroy(this);
        }
    }
}
