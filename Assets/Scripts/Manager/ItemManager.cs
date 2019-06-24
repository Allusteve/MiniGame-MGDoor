using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    List<Item> itemRepos;
    Dictionary<ITEM_ID, Sprite> spriteMap;
    Dictionary<ITEM_ID, string> spritePathMap;

    Dictionary<int, ITEM_ID> composedItemWeightMap;

    private static ItemManager instance;

    private ItemManager()
    {
        itemRepos = new List<Item>();
        spritePathMap = new Dictionary<ITEM_ID, string>();
        MGJsonUtility.LoadItemDataFromJsonFile(itemRepos, spritePathMap);

        // 初始化合成权值表
        composedItemWeightMap = new Dictionary<int, ITEM_ID>
        {
            {2,ITEM_ID.ROPE_MADE_FROM_2_CLOTHES },
            {3,ITEM_ID.ROPE_MADE_FROM_3_CLOTHES },
            {4,ITEM_ID.ROPE_MADE_FROM_4_CLOTHES },
            {50,ITEM_ID.WET_TOWEL_WITH_COLA },
            {240,ITEM_ID.ALL_PAPER }
        };
    }

    public static ItemManager getInstance()
    {
        if (instance == null)
            instance = new ItemManager();
        return instance;
    }

    public void Initialize()
    {
        spriteMap = new Dictionary<ITEM_ID, Sprite>();
    }

    public Item FindItem(ITEM_ID ID)
    {
        foreach (Item item in itemRepos)
        {
            if (item.ID == ID)
            {
                return item;
            }
        }
        return null;
    }

    public void UseItem(Item item)
    {
        switch (item.ID)
        {
            // 已用尽的灭火器
            case ITEM_ID.EXHAUSTED_FIRE_EXTINGUISHER:
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.FIRE_EXTINGUISHER_CURSOR);
                MGUGUIUtility.Toast.showToast("使劲往头盖骨砸！",
                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                break;
            // 干毛巾
            case ITEM_ID.DRY_TOWEL:
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.DRY_TOWEL_CURSOR);
                MGUGUIUtility.Toast.showToast("似乎得靠近水源",
                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                break;
            // 湿毛巾
            case ITEM_ID.WET_TOWEL:
                MGUGUIUtility.Toast.showToast("你将湿毛巾捂在脸上", MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").AddComponent<WetTowelController>();
                break;
            // 消防锤
            case ITEM_ID.FIRE_HAMMER:
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.HAMMER_CURSOR);
                GameObject.Find("Manager").GetComponent<Level1Controller>().PlayerSwitchHammer();
                MGUGUIUtility.Toast.showToast("你握紧了消防锤", MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                break;
            // 破衣服
            case ITEM_ID.NORMAL_CLOTHES_1:
            case ITEM_ID.NORMAL_CLOTHES_2:
            case ITEM_ID.NORMAL_CLOTHES_3:
            case ITEM_ID.NORMAL_CLOTHES_4:
                MGUGUIUtility.Toast.showToast("你套上了一层遮羞布，但似乎不能为你带来什么益处",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                break;
            // 破绳子
            case ITEM_ID.ROPE_MADE_FROM_2_CLOTHES:
                MGUGUIUtility.Toast.showToast("你将绳子绑在了肚皮上，成功地伪装成了一个胖子",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                break;
            case ITEM_ID.ROPE_MADE_FROM_3_CLOTHES:
                MGUGUIUtility.Toast.showToast("你试图用绳子上吊，但没有找到挂绳子的地方",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                break;
            case ITEM_ID.ROPE_MADE_FROM_4_CLOTHES:
                MGUGUIUtility.Toast.showToast("你觉得这绳子足够结实，应该能帮你克服某些障碍",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.ROPE_CURSOR);
                break;
            // 肥宅快乐水
            case ITEM_ID.COLA:
                MGUGUIUtility.Toast.showToast("虽然觉得此刻不适合贪图口腹之欲，但你还是喝下了这罐快乐水",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().PlayerBlood += 20.0f;
                break;
            // 用可乐合成的湿毛巾
            case ITEM_ID.WET_TOWEL_WITH_COLA:
                MGUGUIUtility.Toast.showToast("你将湿毛巾捂在脸上，嗯，味道不错",
                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").AddComponent<WetTowelController>();
                break;
            // 铝制长梯
            case ITEM_ID.ALUMINUM_LONG_LADDER:
                MGUGUIUtility.Toast.showToast("该把它架在哪里好呢",
                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.LADDER_CURSOR);
                break;
            // 冻猪肉
            case ITEM_ID.FROST_PORK:
                MGUGUIUtility.Toast.showToast("除了热乎一点，你跟它没有更多的差异了",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.FROST_PORK_CURSOR);
                break;
            // 热猪肉
            case ITEM_ID.HEAT_PORK:
                MGUGUIUtility.Toast.showToast("狗会吃猪肉，只要它加热过——起码开发者是这么认为的",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.HEAT_PORK_CURSOR);
                break;
            // 猫粮
            case ITEM_ID.CAT_FOOD:
                MGUGUIUtility.Toast.showToast("秘制老鼠干，要不要尝上一口",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.CAT_FOOD_CURSOR);
                break;
            // 钥匙
            case ITEM_ID.KEY:
                MGUGUIUtility.Toast.showToast("你拿起了钥匙",
                    MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().ChangeCursor(CURSOR_TYPE.KEY_CURSOR);
                break;
            // 准考证
            case ITEM_ID.ADMISSION_TICKET:
                MGUGUIUtility.Toast.showToast("你怒吼一声，撕碎了准考证，高考再见！",
                    MGUGUIUtility.Toast.REMAIN_FOREVER, MGUGUIUtility.Toast.TOP_MSG);
                GameObject.Find("Manager").GetComponent<LevelController>().PlayerBlood -= 100.0f;
                break;
            // 纸条
            case ITEM_ID.PAPER1:
            case ITEM_ID.PAPER2:
            case ITEM_ID.PAPER3:
            case ITEM_ID.PAPER4:
                MGUGUIUtility.Toast.showToast("你把纸条吃进了肚子里，虽然感觉自己错过了什么，但你无所畏惧",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                break;
            case ITEM_ID.ALL_PAPER:
                MGUGUIUtility.Toast.showToast("是考验记忆力的时候了！",
                    MGUGUIUtility.Toast.REMAIN_LONG, MGUGUIUtility.Toast.TOP_MSG);
                break;
        }
    }

    public void ComposeItem(List<ITEM_ID> itemList)
    {
        ITEM_ID id = CalculateComposeResult(itemList);

        MusicEvent soundEvent;

        if (id != ITEM_ID.NULL)
        {
            soundEvent = new MusicEvent("composeSuccess", 0.8f, MGEventManager.getInstance().currTime, 1.019f, 1);

            Item item = FindItem(id);
            InventoryManager.getInstance().AddItem(id);
            MGUGUIUtility.Toast.showToast("获得了" + item.name, MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
        }
        // 合成失败
        else
        {
            soundEvent = new MusicEvent("composeFail", 0.8f, MGEventManager.getInstance().currTime, 0.235f, 1);

            foreach (ITEM_ID addID in itemList)
            {
                InventoryManager.getInstance().AddItem(addID);
            }
            MGUGUIUtility.Toast.showToast("合成失败", MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
        }

        MGEventManager.getInstance().AddEvent(soundEvent);
    }

    public Sprite GetSprite(ITEM_ID ID)
    {
        Sprite sprite;
        if (!spriteMap.TryGetValue(ID, out sprite))
        {
            string path;
            spritePathMap.TryGetValue(ID, out path);
            sprite = Resources.Load<Sprite>(path);
            spriteMap.Add(ID, sprite);
        }
        return sprite;
    }

    // 我想到了一个精妙的方案，但我不保证它是绝对正确的
    private ITEM_ID CalculateComposeResult(List<ITEM_ID> itemList)
    {
        int result = 0;

        foreach (ITEM_ID id in itemList)
        {
            result += GetItemWeight(id);
        }

        ITEM_ID composedID;
        if (!composedItemWeightMap.TryGetValue(result, out composedID))
        {
            return ITEM_ID.NULL;
        }
        return composedID;
    }

    private int GetItemWeight(ITEM_ID ID)
    {
        // 只有能参与合成的道具才具有权值
        switch (ID)
        {
            // 毛巾
            case ITEM_ID.DRY_TOWEL:
                return 20;
            // 衣服
            case ITEM_ID.NORMAL_CLOTHES_1:
            case ITEM_ID.NORMAL_CLOTHES_2:
            case ITEM_ID.NORMAL_CLOTHES_3:
            case ITEM_ID.NORMAL_CLOTHES_4:
                return 1;
            // 长度还不够的绳子
            case ITEM_ID.ROPE_MADE_FROM_2_CLOTHES:
                return 2;
            case ITEM_ID.ROPE_MADE_FROM_3_CLOTHES:
                return 3;
            // 可乐
            case ITEM_ID.COLA:
                return 30;
            // 纸条
            case ITEM_ID.PAPER1:
            case ITEM_ID.PAPER2:
            case ITEM_ID.PAPER3:
            case ITEM_ID.PAPER4:
                return 60;
            default:
                return -999999;
        }
    }

    // 关卡2专用，修改为随机密码
    public void ChangeLevel2Password(string desp)
    {
        for (int i = 0; i < itemRepos.Count; ++i)
        {
            if (itemRepos[i].ID == ITEM_ID.ALL_PAPER)
            {
                itemRepos[i].description = desp;
            }
        }
    }
}
