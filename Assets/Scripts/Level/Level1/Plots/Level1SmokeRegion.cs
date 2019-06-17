using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1SmokeRegion : MonoBehaviour
{
    LevelController levelController;
    Level1Controller level1Controller;
    PlayerMovement playerMovement;
    bool isPlayerIn;

    float decreaseBloodValue = 35.0f;

    void Start()
    {
        levelController = GameObject.Find("Manager").GetComponent<LevelController>();
        level1Controller = GameObject.Find("Manager").GetComponent<Level1Controller>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        isPlayerIn = false;
    }

    private void Update()
    {
        if (isPlayerIn&&!levelController.isPause)
        {
            float change = decreaseBloodValue * Time.deltaTime;

            // 蹲下打5折
            if (playerMovement.crouch)
            {
                change *= 0.5f;
            }
            // 戴着湿毛巾再打1折
            if (level1Controller.isPutOnWetTowel)
            {
                change *= 0.1f;
            }

            levelController.PlayerBlood -= change;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerRegion"))
        {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerRegion"))
        {
            isPlayerIn = false;
        }
    }
}
