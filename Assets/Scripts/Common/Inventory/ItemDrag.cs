using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    void Start()
    {
        rect = GetComponent<RectTransform>();
        moveParent = GameObject.Find("Canvas/Inventory").transform;
        inventoryInstance = GameObject.Find("Manager").GetComponent<Inventory>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryInstance.ClickNullItem();

        // 每次移动时都将图片从列表中移出

        // 若原先位于背包格
        if (Item.IsInBag(item.type))
        {
            List<Image> bagItemList = InventoryManager.getInstance().bagItemList;
            List<Image> bagGridList = InventoryManager.getInstance().bagGridList;
            for (int i = 0; i < bagItemList.Count; ++i)
            {
                if (bagItemList[i].GetComponent<ItemDrag>().item.ID == item.ID)
                {
                    if (item.count > 1)
                    {
                        Image remainImage = Instantiate(ResourcesManager.getInstance().itemPrefab, transform.parent) as Image;
                        Item remainItem = new Item(item.ID, item.type, item.count - 1, item.name, item.description);
                        remainImage.GetComponent<ItemDrag>().Initialize(remainItem);
                        bagItemList[i] = remainImage;

                        item.count = 1;
                        UpdateText();
                    }
                    else
                    {
                        bagItemList.RemoveAt(i);

                        // 背包格里的道具一律前移
                        for (int j = 0; j < bagItemList.Count; ++j)
                        {
                            bagItemList[j].transform.position = bagGridList[j].transform.position;
                            bagItemList[j].transform.SetParent(bagGridList[j].transform);
                        }
                    }
                    break;
                }
            }
        }
        // 若原先位于合成格
        else
        {
            List<Image> composeItemList = InventoryManager.getInstance().composeItemList;
            List<Image> composeGridList = InventoryManager.getInstance().composeGridList;
            bool[] stateList = InventoryManager.getInstance().composeGridEmptyStateList;
            for (int i = 0; i < composeItemList.Count; ++i)
            {
                // 因为合成格不会合并ID相同的道具，因此需加入额外判断
                if (composeItemList[i].GetComponent<ItemDrag>().item.ID== item.ID && composeItemList[i].transform == transform)
                {
                    composeItemList.RemoveAt(i);

                    // 合成格里的道具一律前移并重置状态
                    for (int j = 0; j < composeItemList.Count; ++j)
                    {
                        composeItemList[j].transform.position = composeGridList[j].transform.position;
                        composeItemList[j].transform.SetParent(composeGridList[j].transform);
                        stateList[j] = false;
                    }
                    for (int j= composeItemList.Count; j< stateList.Length; ++j)
                    {
                        stateList[j] = true;
                    }
                    break;
                }
            }
        }

        isDrag = false;
        transform.SetParent(moveParent);
        SetPos(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        SetPos(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryInstance.ClickNullItem();
        InventoryManager.getInstance().MoveItem(GetComponent<Image>(), eventData.position);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventoryInstance.ClickItem(item);
    }

    private void SetPos(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect,eventData.position,eventData.pressEventCamera,out tempPos))
        {
            if (isDrag)
            {
                rect.position = tempPos + offset;
            }
            else
            {
                offset = rect.position - tempPos;
            }
        }
    }

    public void Initialize(Item item)
    {
        this.item = new Item(item);

        GetComponent<Image>().sprite = ItemManager.getInstance().GetSprite(item.ID);
        UpdateText();
    }

    public void Refresh()
    {
        UpdateText();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void UpdateText()
    {
        if (item.count == 1)
        {
            transform.Find("CountText").GetComponent<Text>().text = null;
        }
        else
        {
            transform.Find("CountText").GetComponent<Text>().text = item.count.ToString();
        }
    }

    public Item item;

    private bool isDrag;
    private RectTransform rect;

    private Transform moveParent;
    private Inventory inventoryInstance;

    private Vector3 tempPos;
    private Vector3 offset = Vector3.zero;
}
