using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Item;

public class Inventory
{
    private List<Item2> itemList;

    public Inventory()
    {
        itemList = new List<Item2>();
        //AddItem(new Item { itemType = ItemType.Tomatoe, amount = 1 });
        //AddItem(new Item { itemType = ItemType.Potatoe, amount = 1 });
        Debug.Log("chyba dzia³a");
    }

    public void AddItem(Item2 item)
    {
        itemList.Add(item);
    }

    public List<Item2> GetItemList()
    {
        return itemList;
    }
}
