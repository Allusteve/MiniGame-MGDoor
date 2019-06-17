using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public bool isPutOnWetTowel;

    void Start()
    {
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        isPutOnWetTowel = false;

        LevelController levelController = GetComponent<LevelController>();
        levelController.CurrentLevel = "Level_1";

        BeginTextPrintEvent storyPrintEvent =
            new BeginTextPrintEvent("我是门捷列夫，从小就很倒霉，经常碰上各种莫名其妙的意外。这一天，当我独自在电影院座位上睡着的时候，突如其来的火灾发生了……",
            12.0f, "tyrant-by-kevin-macleod", 341.734f);
        MGEventManager.getInstance().AddEvent(storyPrintEvent);

        // 全局扣血
        ChangeBloodEvent globalChangeBloodEvent = new ChangeBloodEvent(-5, 14.0f, 20.0f);
        MGEventManager.getInstance().AddEvent(globalChangeBloodEvent);
    }

    void Update()
    {

    }

    public void PlayerSwitchWetTowel()
    {
        playerMovement.OnTowelShow();
        isPutOnWetTowel = !isPutOnWetTowel;
    }

    public void PlayerSwitchHammer()
    {
        playerMovement.OnAxeShow();
    }

    public void PlayerPoundWithHammer()
    {
        playerMovement.OnBreaking();
    }
}
