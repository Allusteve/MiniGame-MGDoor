using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearTrigger : MonoBehaviour
{
    public MG_EVENT_ID TriggerEventID;
    public int HappenCount;
    // 仅在上述事件ID为“获得物品”事件时下述变量有效
    public ITEM_ID GetItemID;
    // 上述事件ID为“信息显示”事件时只显示以下文本
    public string MessageText;
    // 仅在上述事件ID为“物体出现”事件时下述变量有效
    public GameObject apearObject;

    private new SpriteRenderer renderer;
    private bool canTrigger;

    private LevelController levelController;
    private GameObject player;
    private PlayerMovement playerMovement;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
        canTrigger = false;

        levelController = GameObject.Find("Manager").GetComponent<LevelController>();
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerView"))
        {
            renderer.enabled = true;
            canTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PlayerView"))
        {
            renderer.enabled = false;
            canTrigger = false;
        }
    }

    private void OnMouseDown()
    {
        if (canTrigger)
        {
            CURSOR_TYPE formerCursor = levelController.cursorType;

            // 调bug很蛋疼，我只能瞎改了
            // 后续此处必须重构
            if ((formerCursor == CURSOR_TYPE.DRY_TOWEL_CURSOR && GetItemID == ITEM_ID.WET_TOWEL) ||
                (formerCursor == CURSOR_TYPE.ROPE_CURSOR && TriggerEventID == MG_EVENT_ID.OBJECT_APEAR) ||
                (formerCursor == CURSOR_TYPE.FROST_PORK_CURSOR && GetItemID == ITEM_ID.HEAT_PORK && GameObject.Find("Manager").GetComponent<Level2Controller>().electricOn)||
                (formerCursor == CURSOR_TYPE.HEAT_PORK_CURSOR && TriggerEventID == MG_EVENT_ID.LEVEL_2_FEED_DOG) ||
                (formerCursor == CURSOR_TYPE.LADDER_CURSOR && TriggerEventID == MG_EVENT_ID.OBJECT_APEAR && apearObject.name.Equals("LadderInFirstFloorCrack")) ||
                (formerCursor == CURSOR_TYPE.LADDER_CURSOR && TriggerEventID == MG_EVENT_ID.LEVEL_2_UP_ATTIC))
            {
                levelController.RecoverCursor(false);
            }
            else
            {
                levelController.RecoverCursor(true);
            }

            switch (TriggerEventID)
            {
                case MG_EVENT_ID.GET_ITEM:
                    {
                        // 仍需要针对某些物品进行特化
                        switch (GetItemID)
                        {
                            // 厕所里润湿湿毛巾
                            case ITEM_ID.WET_TOWEL:
                                if (formerCursor != CURSOR_TYPE.DRY_TOWEL_CURSOR)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                break;
                            // 获得铝制长梯后场景里的长梯消失
                            case ITEM_ID.ALUMINUM_LONG_LADDER:
                                switch (levelController.CurrentLevel)
                                {
                                    case "Level_1":
                                        GameObject.Find("Building/fruit_shop/ladder").SetActive(false);
                                        break;
                                    case "Level_2":
                                        GameObject.Find("Building/storage/ladder1").SetActive(false);
                                        break;
                                }
                                break;
                            // 获得毛巾后场景里的毛巾消失
                            case ITEM_ID.DRY_TOWEL:
                                GameObject.Find("Building/daily_shop/towel").SetActive(false);
                                break;
                            // 获得可乐后场景里的可乐消失
                            case ITEM_ID.COLA:
                                GameObject.Find("Building/McDonald/cola").SetActive(false);
                                break;
                            // 加热猪肉，因为猪肉只在第2关出现所以可以像下面这样写
                            case ITEM_ID.HEAT_PORK:
                                if (GameObject.Find("Manager").GetComponent<Level2Controller>().electricOn == false)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                else
                                {
                                    if (formerCursor != CURSOR_TYPE.FROST_PORK_CURSOR)
                                    {
                                        MessageText = "把你的脑子放进微波炉加热一下它就变成热腾腾的猪脑子了";
                                        MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                                        return;
                                    }
                                }
                                break;
                            // 第4张纸条
                            case ITEM_ID.PAPER4:
                                if (GameObject.Find("Manager").GetComponent<Level2Controller>().dogEatPork == false)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                HappenCount = 1;
                                break;
                            // 猫粮
                            case ITEM_ID.CAT_FOOD:
                                if (GameObject.Find("Manager").GetComponent<Level2Controller>().knowTheKey == false)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                HappenCount = 1;
                                break;
                            // 钥匙
                            case ITEM_ID.KEY:
                                if (formerCursor != CURSOR_TYPE.CAT_FOOD_CURSOR)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                GameObject.Find("Cat").GetComponent<CatMovement>().OnIdle();
                                MusicEvent soundEvent = new MusicEvent("meow", 0.8f, MGEventManager.getInstance().currTime, 0.418f, 1);
                                MGEventManager.getInstance().AddEvent(soundEvent);
                                HappenCount = 1;
                                break;
                        }
                    }
                    GetItemEvent getItemEvent = new GetItemEvent(GetItemID);
                    MGEventManager.getInstance().AddEvent(getItemEvent);
                    break;
                case MG_EVENT_ID.SHOW_MESSAGE:
                    MessageEvent msgEvent = new MessageEvent(MessageText, MGEventManager.getInstance().currTime, 0, 1);
                    MGEventManager.getInstance().AddEvent(msgEvent);
                    break;
                case MG_EVENT_ID.LEVEL_1_BREAKING_DOOR:
                    if (formerCursor == CURSOR_TYPE.HAMMER_CURSOR)
                    {
                        GameObject.Find("Manager").GetComponent<Level1Controller>().PlayerPoundWithHammer();
                        MGUGUIUtility.Toast.showToast("你脱离了危险", MGUGUIUtility.Toast.REMAIN_FOREVER, MGUGUIUtility.Toast.TOP_MSG);

                        GameObject.Find("Building/door/door_close").GetComponent<SpriteRenderer>().enabled = false;

                        levelController.PassLevel();
                    }
                    else
                    {
                        MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                        return;
                    }
                    break;
                case MG_EVENT_ID.LEVEL_1_CLIMB_TO_PIPE:
                    if (playerMovement.IsJumping())
                    {
                        MGUGUIUtility.Toast.showToast("你试图在空中抓住管道口一跃而上，不幸扭到了腰",
                        MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                        return;
                    }
                    else
                    {
                        MGUGUIUtility.Toast.showToast("你爬上了通风管道……这得感谢大三那年的体测，让你练就了一副好腰",
                        MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);

                        playerMovement.CanStand = false;
                        player.transform.position = new Vector3(7.5f, 5.0f, -5.0f);
                        GameObject.Find("Building/PipeCollider").GetComponent<BoxCollider2D>().enabled = true;
                    }
                    break;
                case MG_EVENT_ID.OBJECT_APEAR:
                    {
                        switch (apearObject.name)
                        {
                            case "RopeToFirstFloor":
                                if (formerCursor != CURSOR_TYPE.ROPE_CURSOR)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                MGUGUIUtility.Toast.showToast("你把绳子绑在了栏杆上，身手敏捷的爬了下去",
                                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                                PlayerAnimationEvent downEvent = new PlayerAnimationEvent("down", 6.0f);
                                MGEventManager.getInstance().AddEvent(downEvent);
                                break;
                            case "LadderInFirstFloorCrack":
                                if (formerCursor != CURSOR_TYPE.LADDER_CURSOR)
                                {
                                    MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                                    return;
                                }
                                MGUGUIUtility.Toast.showToast("你把梯子架在了裂缝上",
                                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                                break;
                        }
                    }
                    apearObject.SetActive(true);
                    break;
                case MG_EVENT_ID.LEVEL_2_DOWNSTAIR:
                    {
                        GameObject.Find("Player").transform.position = new Vector3(10.0f, -16.0f, GameObject.Find("Player").transform.position.z);
                        MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                    }
                    break;
                case MG_EVENT_ID.LEVEL_2_UPSTAIR:
                    {
                        GameObject.Find("Player").transform.position = new Vector3(9.0f, -5.69f, GameObject.Find("Player").transform.position.z);
                        MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                    }
                    break;
                case MG_EVENT_ID.LEVEL_2_DOWN_ATTIC:
                    {
                        GameObject.Find("Player").transform.position = new Vector3(21.0f, -5.69f, GameObject.Find("Player").transform.position.z);
                        MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                    }
                    break;
                case MG_EVENT_ID.LEVEL_2_UP_ATTIC:
                    {
                        if (GameObject.Find("Manager").GetComponent<Level2Controller>().putLadder == false)
                        {
                            if (formerCursor != CURSOR_TYPE.LADDER_CURSOR)
                            {
                                MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                                return;
                            }
                            GameObject.Find("Building/balcony/ladder2").SetActive(true);
                            GameObject.Find("Manager").GetComponent<Level2Controller>().putLadder = true;
                        }
                        else
                        {
                            GameObject.Find("Player").transform.position = new Vector3(12.0f, 3.62f, GameObject.Find("Player").transform.position.z);
                            MGUGUIUtility.Toast.showToast("爬上长梯来到了阁楼里", MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                        }
                    }
                    break;
                // 喂狗
                case MG_EVENT_ID.LEVEL_2_FEED_DOG:
                    if (GameObject.Find("Manager").GetComponent<Level2Controller>().dogEatPork == false)
                    {
                        if (formerCursor != CURSOR_TYPE.HEAT_PORK_CURSOR)
                        {
                            MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);

                           MusicEvent soundEvent = new MusicEvent("dogHowl", 0.8f, MGEventManager.getInstance().currTime, 3.175f, 1);
                           MGEventManager.getInstance().AddEvent(soundEvent);

                            return;
                        }
                        MGUGUIUtility.Toast.showToast("恶狗跑出狗窝去吃狗肉", MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);

                        FeedDogEvent feedDogEvent = new FeedDogEvent();
                        MGEventManager.getInstance().AddEvent(feedDogEvent);

                        MusicEvent soundEvent2 = new MusicEvent("bowwow", 0.8f, MGEventManager.getInstance().currTime, 2.10f, 1);
                        MGEventManager.getInstance().AddEvent(soundEvent2);
                        GameObject.Find("Manager").GetComponent<Level2Controller>().dogEatPork = true;
                    }
                    else
                    {
                        MGUGUIUtility.Toast.showToast("恶狗吃着香喷喷的猪肉", MGUGUIUtility.Toast.REMAIN_SHORT,
                                        MGUGUIUtility.Toast.TOP_MSG);
                    }
                    break;

                case MG_EVENT_ID.LEVEL_2_COMPUTER:
                    {
                        if (GameObject.Find("Manager").GetComponent<Level2Controller>().electricOn == false)
                        {
                            MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                            return;
                        }
                        MGUGUIUtility.Toast.showToast("打开电脑", MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                        //此处应实现打开电脑界面
                        GameObject.Find("Player").GetComponent<PlayerMovement>().OnLocking();
                        GameObject.Find("Manager").GetComponent<Level2Controller>().computerOpen = true;
                    }
                    break;
                case MG_EVENT_ID.LEVEL_2_CONNECT_ELECTRIC:
                    {
                        Level2Controller level2Controller = GameObject.Find("Manager").GetComponent<Level2Controller>();
                        if (level2Controller.isPlayingWireGame)
                        {
                            return;
                        }

                        if (!level2Controller.electricOn)
                        {
                          //  GameObject.Find("Manager").GetComponent<Level2Controller>().electricOn = true;
                         //   MGUGUIUtility.Toast.showToast("你拉上了电闸", MGUGUIUtility.Toast.REMAIN_SHORT,
                           //                 MGUGUIUtility.Toast.TOP_MSG);
                            // 先禁止玩家操作
                                       playerMovement.OnLocking();
                            // 接电线小游戏
                                        SetWire.ResetWire();
                            level2Controller.isPlayingWireGame = true;
                            return;
                        }
                        MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                    }
                    break;
                case MG_EVENT_ID.LEVEL_2_OPEN_THE_DOOR:
                    {
                        if (formerCursor != CURSOR_TYPE.KEY_CURSOR)
                        {
                            MGUGUIUtility.Toast.showToast(MessageText, MGUGUIUtility.Toast.REMAIN_SHORT,
                                            MGUGUIUtility.Toast.TOP_MSG);
                            return;
                        }
                        MGUGUIUtility.Toast.showToast("高考加油，小伙子！", MGUGUIUtility.Toast.REMAIN_FOREVER,
                                            MGUGUIUtility.Toast.TOP_MSG);
                        //增加事件实现
                        levelController.PassLevel();
                    }
                    break;
            }

            if (HappenCount == 1)
            {
                Destroy(gameObject);
            }
            else
            {
                --HappenCount;
            }
        }
    }

}
