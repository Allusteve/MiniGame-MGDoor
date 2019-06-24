using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MGJsonUtility
{
    // 读取道具json数据
    public static void LoadItemDataFromJsonFile(List<Item> itemRepos, Dictionary<ITEM_ID, string> spritePathMap)
    {
        string itemText = Resources.Load<TextAsset>("Data/item").text;
        JArray itemArray = (JArray)JsonConvert.DeserializeObject(itemText);

        for (int i = 0; i < itemArray.Count; ++i)
        {
            ITEM_ID id = (ITEM_ID)(int)itemArray[i]["ID"];
            Item item = new Item(id, ITEM_TYPES.ITEM_IN_BAG_GRID, 1, itemArray[i]["Name"].ToString(), itemArray[i]["Detail"].ToString());
            itemRepos.Add(item);
            spritePathMap.Add(id, itemArray[i]["Path"].ToString());
        }
    }
}
