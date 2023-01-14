using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class Inventory
{
    private List<ItemDefinition> itemList;

    public Inventory()
    {
        itemList = new List<ItemDefinition>();
        //AddItem(new Item { itemType = ItemType.Tomatoe, amount = 1 });
        //AddItem(new Item { itemType = ItemType.Potatoe, amount = 1 });
        Debug.Log("chyba dzia³a");
    }

    public void AddItem(ItemDefinition item)
    {
        itemList.Add(item);
    }

    public List<ItemDefinition> GetItemList()
    {
        return itemList;
    }
}
