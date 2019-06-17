using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager
{
    // 背包格、合成格
    public List<Image> bagGridList;
    public List<Image> composeGridList;

    // 背包格道具、合成格道具
    public List<Image> bagItemList;
    public List<Image> composeItemList;

    public bool[] composeGridEmptyStateList;

    private static volatile InventoryManager instance;
    private static object syncRoot = new Object();

    private InventoryManager()
    {
        
    }

    public static InventoryManager getInstance()
    {
        if (instance == null)
        {
            lock (syncRoot)
            {
                if (instance == null)
                {
                    instance = new InventoryManager();
                }
            }
        }
        return instance;
    }

    public void Initialize()
    {
        bagGridList = new List<Image>();
        composeGridList = new List<Image>();

        bagItemList = new List<Image>();
        composeItemList = new List<Image>();
    }

    // 添加道具至背包格，不要轻易直接调用此函数
    public Item AddItem(ITEM_ID ID)
    {
        ItemDrag itemDrag = IsItemExist(bagItemList, ID);
        if (itemDrag != null)
        {
            ++itemDrag.item.count;
            itemDrag.Refresh();
            return itemDrag.item;
        }

        Image itemImage = GameObject.Instantiate(ResourcesManager.getInstance().itemPrefab, bagGridList[bagItemList.Count].transform);
        itemDrag = itemImage.GetComponent<ItemDrag>();
        itemDrag.Initialize(ItemManager.getInstance().FindItem(ID));
        bagItemList.Add(itemImage);
        return itemDrag.item;
    }

    // 使用道具
    public void UseItem(ITEM_ID ID)
    {
        // 为方便检测先整理背包
        RefreshInventory();

        for (int i = 0; i < bagItemList.Count; ++i)
        {
            ItemDrag itemDrag = bagItemList[i].GetComponent<ItemDrag>();
            Item item = itemDrag.item;

            if (item.ID == ID)
            {
                if (item.count > 1)
                {
                    --item.count;
                    itemDrag.Refresh();
                }
                else
                {
                    itemDrag.Destroy();
                    bagItemList.RemoveAt(i);
                    RefreshInventory();
                }

                ItemManager.getInstance().UseItem(item);
                break;
            }
        }
    }

    // 合成道具
    public void ComposeItem()
    {
        List<ITEM_ID> composedIDList = new List<ITEM_ID>();
        foreach (Image image in composeItemList)
        {
            ItemDrag itemDrag = image.GetComponent<ItemDrag>();
            composedIDList.Add(itemDrag.item.ID);
            itemDrag.Destroy();
        }
        composeItemList.Clear();
        // 重置合成格状态
        for (int i = 0; i < composeGridList.Count; ++i)
        {
            composeGridEmptyStateList[i] = true;
        }
        RefreshInventory();
        ItemManager.getInstance().ComposeItem(composedIDList);
    }

    // 用于关闭背包时的重置
    public void RefreshInventory()
    {
        // 背包格里的道具一律前移，防止出现空格的情况
        for (int i = 0; i < bagItemList.Count; ++i)
        {
            bagItemList[i].transform.position = bagGridList[i].transform.position;
            bagItemList[i].transform.SetParent(bagGridList[i].transform);
        }

        // 合成格里剩余的道具一律放入背包格，并重置状态
        foreach (Image image in composeItemList)
        {
            ItemDrag itemDrag = image.GetComponent<ItemDrag>();
            AddItem(itemDrag.item.ID);
            itemDrag.Destroy();
        }
        composeItemList.Clear();
        for (int i = 0; i < composeGridList.Count; ++i)
        {
            composeGridEmptyStateList[i] = true;
        }
    }

    // 移动道具
    public void MoveItem(Image image,Vector2 mousePos)
    {
        // 只有在道具格与合成格之间互相移动是有效的，其余均回复原位

        ItemDrag itemDrag = image.GetComponent<ItemDrag>();

        // 原先位于背包格
        if (Item.IsInBag(itemDrag.item.type))
        {
            for (int i = 0; i < composeGridList.Count; ++i)
            {
                Rect rect = MGUGUIUtility.RectTransformToSpaceRect(composeGridList[i].rectTransform);
                // 移动成功
                if (rect.Contains(mousePos) && composeGridEmptyStateList[i])
                {
                    composeGridEmptyStateList[i] = false;

                    itemDrag.item.type = Item.SwitchGridType(itemDrag.item.type);

                    image.transform.position = composeGridList[i].transform.position;
                    image.transform.SetParent(composeGridList[i].transform);
                    composeItemList.Add(image);
                    return;
                }
            }
            // 移动失败
            AddItem(itemDrag.item.ID);
            itemDrag.Destroy();
        }
        // 原先位于合成格
        else
        {
            Rect rect = MGUGUIUtility.RectTransformToSpaceRect(bagGridList[0].transform.parent.GetComponent<RectTransform>());
            // 移动成功
            if (rect.Contains(mousePos))
            {
                AddItem(itemDrag.item.ID);
                GameObject.Destroy(image);
            }
            // 移动失败
            else
            {
                composeGridEmptyStateList[composeItemList.Count] = false;
                image.transform.position = composeGridList[composeItemList.Count].transform.position;
                image.transform.SetParent(composeGridList[composeItemList.Count].transform);

                composeItemList.Add(image);
            }
        }
    }

    // 清空背包
    public void ClearInventory()
    {
        RefreshInventory();

        for (int i = 0; i < bagItemList.Count; ++i)
        {
            ItemDrag itemDrag = bagItemList[i].GetComponent<ItemDrag>();
            itemDrag.Destroy();
        }
        bagItemList.Clear();
    }

    private ItemDrag IsItemExist(List<Image> imageList, ITEM_ID ID)
    {
        foreach (Image image in imageList)
        {
            ItemDrag result = image.GetComponent<ItemDrag>();
            if (result.item.ID == ID)
                return result;
        }
        return null;
    }
}
