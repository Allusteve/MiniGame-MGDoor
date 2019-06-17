
public enum ITEM_TYPES
{
    ITEM_IN_BAG_GRID = -1,
    ITEM_IN_COMPOSE_GRID = 1
}

public enum ITEM_ID
{
    NULL = 0,
    // 关卡1
    EXHAUSTED_FIRE_EXTINGUISHER = 1,
    DRY_TOWEL = 2,
    WET_TOWEL = 3,
    FIRE_HAMMER = 4,
    NORMAL_CLOTHES_1 = 5,
    ROPE_MADE_FROM_2_CLOTHES = 6,
    ROPE_MADE_FROM_3_CLOTHES = 7,
    ROPE_MADE_FROM_4_CLOTHES = 8,
    COLA = 9,
    WET_TOWEL_WITH_COLA = 10,
    ALUMINUM_LONG_LADDER = 11,
    // 关卡2
    PAPER1 = 12,
    PAPER2 = 13,
    PAPER3 = 14,
    PAPER4 = 15,
    ALL_PAPER = 16,
    FROST_PORK = 17,
    HEAT_PORK = 18,
    CAT_FOOD = 19,
    KEY = 20,
    // 关卡1补充道具
    NORMAL_CLOTHES_2 = 21,
    NORMAL_CLOTHES_3 = 22,
    NORMAL_CLOTHES_4 = 23,
    // 关卡2彩蛋
    ADMISSION_TICKET = 24
}

public class Item
{
    public ITEM_ID ID;
    public ITEM_TYPES type;
    public int count;
    public string name;
    public string description;

    public Item(ITEM_ID ID, ITEM_TYPES type, int count, string name, string description)
    {
        this.ID = ID;
        this.type = type;
        this.count = count;
        this.name = name;
        this.description = description;
    }

    public Item(Item other)
    {
        ID = other.ID;
        type = other.type;
        count = other.count;
        name = other.name;
        description = other.description;
    }

    public static bool IsInBag(ITEM_TYPES type)
    {
        return type < 0;
    }

    public static ITEM_TYPES SwitchGridType(ITEM_TYPES type)
    {
        return (ITEM_TYPES)(-(int)type);
    }
}
