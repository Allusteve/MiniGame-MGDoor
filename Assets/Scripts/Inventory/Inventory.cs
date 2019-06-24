using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    /********************************数据********************************/

    // 动画控制
    float targetAlpha;
    const float ALPHA_SMOOTH = 8.0f;

    CanvasGroup inventoryCanvasGroup;
    CanvasGroup itemInfoCanvasGroup;
    Text itemInfoTitle;
    Text itemInfoDetail;

    // 记录当前选中物体的ID
    ITEM_ID selectedItemID;

    private const int BAG_CAPACITY = 20;
    private const int MAX_COMPOSE_COUNT = 4;

    private LevelController levelController;

    /*******************************内部运行*******************************/

    void Start()
    {
        inventoryCanvasGroup = GameObject.Find("Canvas/Inventory").GetComponent<CanvasGroup>();
        itemInfoCanvasGroup = GameObject.Find("Canvas/Inventory/ItemInfoPanel").GetComponent<CanvasGroup>();
        itemInfoTitle = GameObject.Find("Canvas/Inventory/ItemInfoPanel/ItemInfoTitle").GetComponent<Text>();
        itemInfoDetail = GameObject.Find("Canvas/Inventory/ItemInfoPanel/ItemInfoDetail").GetComponent<Text>();
        targetAlpha = inventoryCanvasGroup.alpha;

        levelController = GetComponent<LevelController>();

        Image emptyGridPrefab = ResourcesManager.getInstance().emptyGridPrefab;
        List<Image> bagGridList = InventoryManager.getInstance().bagGridList;
        List<Image> composeGridList = InventoryManager.getInstance().composeGridList;

        // 创建背包格
        Transform bagContainer = GameObject.Find("Canvas/Inventory/Bag/Container").transform;
        for (int i = 0; i < BAG_CAPACITY; ++i)
        {
            Image grid = Instantiate(emptyGridPrefab, bagContainer);
            grid.GetComponent<Button>().onClick.AddListener(ClickNullItem);
            bagGridList.Add(grid);
        }

        // 创建合成格
        Transform composeBoxContainer = GameObject.Find("Canvas/Inventory/ComposeBox/Container").transform;
        for (int i = 0; i < MAX_COMPOSE_COUNT; ++i)
        {
            Image grid = Instantiate(emptyGridPrefab, composeBoxContainer);
            grid.GetComponent<Button>().onClick.AddListener(ClickNullItem);
            composeGridList.Add(grid);
        }

        // 初始化合成格状态
        InventoryManager.getInstance().composeGridEmptyStateList = new bool[composeGridList.Count];
        for (int i = 0; i < InventoryManager.getInstance().composeGridEmptyStateList.Length; ++i)
        {
            InventoryManager.getInstance().composeGridEmptyStateList[i] = true;
        }
    }

    void Update()
    {
        if (targetAlpha != inventoryCanvasGroup.alpha)
        {
            inventoryCanvasGroup.alpha = Mathf.Lerp(inventoryCanvasGroup.alpha, targetAlpha, ALPHA_SMOOTH * Time.deltaTime);
            if (Mathf.Abs(inventoryCanvasGroup.alpha - targetAlpha) <= 0.1f)
                inventoryCanvasGroup.alpha = targetAlpha;
        }
    }

    /*******************************外部接口*******************************/

    // 打开背包
    public void OpenInventory()
    {
        if (levelController.cursorType == CURSOR_TYPE.NORMAL_CURSOR)
        {
            MusicEvent soundEvent = new MusicEvent("openBag", 0.8f, MGEventManager.getInstance().currTime, 0.3f, 1);
            MGEventManager.getInstance().AddEvent(soundEvent);

            targetAlpha = 1.0f;
            inventoryCanvasGroup.interactable = true;
            inventoryCanvasGroup.blocksRaycasts = true;
        }
    }

    // 关闭背包
    public void CloseInventory()
    {
        MusicEvent soundEvent = new MusicEvent("openBag", 0.8f, MGEventManager.getInstance().currTime, 0.3f, 1);
        MGEventManager.getInstance().AddEvent(soundEvent);

        inventoryCanvasGroup.interactable = false;
        inventoryCanvasGroup.blocksRaycasts = false;
        targetAlpha = 0.0f;

        InventoryManager.getInstance().RefreshInventory();
        ClickNullItem();
    }

    // 合成道具
    public void ComposeItem()
    {
        InventoryManager.getInstance().ComposeItem();
        ClickNullItem();
    }

    // 点击道具图片时显示信息
    public void ClickItem(Item item)
    {
        selectedItemID = item.ID;

        itemInfoTitle.text = item.name;
        itemInfoDetail.text = item.description;

        itemInfoCanvasGroup.interactable = true;
        itemInfoCanvasGroup.alpha = 1.0f;
    }

    // 点击空白格时隐藏信息栏
    public void ClickNullItem()
    {
        selectedItemID = ITEM_ID.NULL;
        itemInfoCanvasGroup.interactable = false;
        itemInfoCanvasGroup.alpha = 0.0f;
    }

    // 添加道具
    public void AddItem(ITEM_ID ID)
    {
        if (ID == ITEM_ID.NULL)
        {
            MGUGUIUtility.Toast.showToast("似乎什么都没有得到", MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
            return;
        }
        Item item = InventoryManager.getInstance().AddItem(ID);
        MGUGUIUtility.Toast.showToast("获得了" + item.name, MGUGUIUtility.Toast.REMAIN_SHORT, MGUGUIUtility.Toast.TOP_MSG);
    }

    // 使用道具
    public void UseItem()
    {
        MusicEvent soundEvent = new MusicEvent("useItem", 0.8f, MGEventManager.getInstance().currTime, 0.496f, 1);
        MGEventManager.getInstance().AddEvent(soundEvent);

        InventoryManager.getInstance().UseItem(selectedItemID);
        ClickNullItem();
        CloseInventory();
    }
}
