using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public bool electricOn;
    public bool dogEatPork;
    public bool knowTheKey;
    public bool putLadder;

    public bool computerOpen;
    public bool wireOpen;


    public string password;

    public bool isPlayingWireGame;

    void Start()
    {
        LevelController levelController = GetComponent<LevelController>();
        levelController.CurrentLevel = "Level_2";

        GameObject.Find("Cat").GetComponent<CatMovement>().OnWalking();

        electricOn = false;
        electricOn = false;
        knowTheKey = false;
        putLadder = false;
        computerOpen = false;
        wireOpen = false;

        isPlayingWireGame = false;

        BeginTextPrintEvent storyPrintEvent =
            new BeginTextPrintEvent("2016年，高考前夜，我借宿在朋友家。复习得太晚，第二天醒来时已经八点了......什么！！！已经八点了！！！",
            12.0f, "the-lift-by-kevin-macleod", 467.435f);
        MGEventManager.getInstance().AddEvent(storyPrintEvent);

        // 高考倒计时扣血
        ChangeBloodEvent globalChangeBloodEvent = new ChangeBloodEvent(-15, 13.0f, 30.0f);
        MGEventManager.getInstance().AddEvent(globalChangeBloodEvent);

        // 高考倒计时显示
        MessageEvent alarmTimeMsgEvent = new MessageEvent("假如生活欺骗了你，不要悲伤，只需做好复读的准备", 13.0f, 30.0f, -1);
        MGEventManager.getInstance().AddEvent(alarmTimeMsgEvent);

        // 往背包里添加准考证
        InventoryManager.getInstance().AddItem(ITEM_ID.ADMISSION_TICKET);

        SetPassword();
    }

    void Update()
    {

    }

    public void PutOnTheElectric()
    {
        electricOn = true;
    }

    public void FeedTheDog()
    {
        dogEatPork = true;
    }

    public void KnowWhereKey()
    {
        knowTheKey = true;
    }

    public void CloseComputer()
    {
        computerOpen = false;
    }

    private void SetPassword()
    {
        password = "";
        System.Random rd = new System.Random();
        for (int i = 0; i < 8; i++)
        {
            password += rd.Next(0, 9).ToString();
        }
        //设置物品显示的密码
        ItemManager.getInstance().ChangeLevel2Password("上面写着一串莫名其妙的数字:" + password);
    }
}
