using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public bool isPause;
    public bool isPass;
    private PlayerMovement playerMovement;

    public CURSOR_TYPE cursorType;

    public float PlayerBlood;
    private Slider bloodSlider;

    public string CurrentLevel;

    UIControllder uiController;

    void Start()
    {
        PlayerBlood = 100.0f;
        CurrentLevel = "";

        bloodSlider = GameObject.Find("Canvas/PlayerInfo/RedBar").GetComponent<Slider>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

        MGEventManager.getInstance().Initialize();
        ItemManager.getInstance().Initialize();
        ResourcesManager.getInstance().Initialize();
        InventoryManager.getInstance().Initialize();

        isPause = false;
        isPass = false;

        uiController = GetComponent<UIControllder>();

        cursorType = CURSOR_TYPE.NORMAL_CURSOR;
        ChangeCursor(CURSOR_TYPE.NORMAL_CURSOR);
    }

    void Update()
    {
        if (!isPause)
        {
            // 鼠标右键监测，游戏中涉及鼠标右键的只有使用物品后的恢复
            if (Input.GetMouseButtonDown(1))
            {
                RecoverCursor(true);
            }

            MGEventManager.getInstance().Update();

            bloodSlider.value = PlayerBlood;

            // 检测玩家是否已死亡
            if (PlayerBlood <= 0.0f)
            {
                playerMovement.OnDeath();
                GetComponent<UIControllder>().ShowGameOverPage();
            }
        }
    }

    public void Pause()
    {
        isPause = true;
        playerMovement.OnLocking();
    }

    public void Continue()
    {
        if (!isPass)
        {
            isPause = false;
            playerMovement.OnLocking();
        }
        else
        {
            NextLevel();
        }
    }

    public void ChangeCursor(CURSOR_TYPE type)
    {
        cursorType = type;
        Cursor.SetCursor(ResourcesManager.getInstance().cursorTextureList[cursorType], Vector2.zero, CursorMode.Auto);
    }

    public void PassLevel()
    {
        isPass = true;

        uiController.ShowPausePage();
    }

    public void NextLevel()
    {
        switch (CurrentLevel)
        {
            case "Level_1":
                SceneManager.LoadScene("Level_2");
                break;
            case "Level_2":
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }

    public void RecoverCursor(bool getItem)
    {
        if (getItem)
        {
            switch (cursorType)
            {
                case CURSOR_TYPE.DRY_TOWEL_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.DRY_TOWEL);
                    break;
                case CURSOR_TYPE.HAMMER_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.FIRE_HAMMER);
                    GameObject.Find("Manager").GetComponent<Level1Controller>().PlayerSwitchHammer();
                    break;
                case CURSOR_TYPE.LADDER_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.ALUMINUM_LONG_LADDER);
                    break;
                case CURSOR_TYPE.ROPE_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.ROPE_MADE_FROM_4_CLOTHES);
                    break;
                case CURSOR_TYPE.FIRE_EXTINGUISHER_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.EXHAUSTED_FIRE_EXTINGUISHER);
                    break;
                case CURSOR_TYPE.FROST_PORK_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.FROST_PORK);
                    break;
                case CURSOR_TYPE.HEAT_PORK_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.HEAT_PORK);
                    break;
                case CURSOR_TYPE.CAT_FOOD_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.CAT_FOOD);
                    break;
                case CURSOR_TYPE.KEY_CURSOR:
                    InventoryManager.getInstance().AddItem(ITEM_ID.KEY);
                    break;
            }
        }

        ChangeCursor(CURSOR_TYPE.NORMAL_CURSOR);
    }
}
