using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using static Item;
using static ItemDefinition;
using static UnityEditor.Experimental.GraphView.Port;
public enum InventoryAction
{
    Equip, Plant, Trow
}

public class Inventory
{
    private ItemDefinition equippedItemDefinition;
    private ItemDefinition[,] itemList;
    private uint capacity;
    private uint _x;
    private uint _y;
    private Dictionary<ItemDefinition.ItemType, int> itemCount;
    public uint Capacity => capacity;

    public Inventory(uint x=4, uint y=1)
    {
        _y = y;
        _x = x;
        capacity = x * y;
        itemCount=new Dictionary<ItemDefinition.ItemType, int>();
        foreach(ItemDefinition.ItemType itemType in Enum.GetValues(typeof(ItemDefinition.ItemType)))
        {
            itemCount.Add(itemType, 0);
        }
        itemList = new ItemDefinition[x, y];
        AddItem(new ItemDefinition() { itemType = ItemDefinition.ItemType.Potatoe, amount = 1 });
        AddItem(new ItemDefinition() { itemType = ItemDefinition.ItemType.Beetroot, amount = 1 });
        AddItem(new ItemDefinition() { itemType = ItemDefinition.ItemType.Potatoe, amount = 1 });
    }

    public int GetCountOfType(ItemDefinition.ItemType type)
    {
        return itemCount[type];
    }

    void AddToItemCountByTypes(ItemDefinition item)
    {
        int currentCount;
        itemCount.TryGetValue(item.itemType, out currentCount);
        itemCount[item.itemType] = currentCount + 1;
    }

    void SubToItemCountByTypes(ItemDefinition item)
    {
        if(item is not null)
        {
            int currentCount;
            itemCount.TryGetValue(item.itemType, out currentCount);
            itemCount[item.itemType] = currentCount - 1;
        }
    }
 
    public void AddItem(ItemDefinition item)
    {
        AddToItemCountByTypes(item);
        for (uint j = 0; j < _y; j++)
        {
            for (uint i = 0; i < _x; i++)
            {
                if (itemList[i, j] is null)
                {
                    itemList[i, j] = new ItemDefinition()
                    {
                        itemType= item.itemType,
                        amount= item.amount
                    };
                    return;
                }
            }
        }
    }

    public uint GetSpaceLeft()
    {
        uint leftSpace = 0;
        for (uint i = 0; i < _x; i++)
        {
            for (uint j = 0; j < _y; j++)
            {
                if (itemList[i, j] is null)
                {
                    leftSpace++;
                }
            }
        }
        return leftSpace;
    }

    public ItemDefinition GetEquippedItem()
    {
        return equippedItemDefinition;
    }

    public void SwapAt(uint x1, uint y1, uint x2, uint y2)
    {
        var swap = itemList[x1, y1];
        itemList[x1, y1] = itemList[x2, y2];
        itemList[x2, y2] = swap;
    }

    public void SwapEqAt(uint x1, uint y1)
    {
        var swap = itemList[x1, y1];
        itemList[x1, y1] = equippedItemDefinition;
        equippedItemDefinition = swap;
    }

    public void DestorySlotAt(uint x, uint y)
    {
        SubToItemCountByTypes(itemList[x, y]);
        itemList[x, y] = null;
    }

    public ItemDefinition GetAt(uint x, uint y)
    {
        return itemList[x, y];
    }

    public InventoryAction UseItem(uint x, uint y)
    {
        if(itemList[x, y].itemType == ItemDefinition.ItemType.TomatoeSeed || itemList[x, y].itemType == ItemDefinition.ItemType.PotatoeSeed || itemList[x, y].itemType == ItemDefinition.ItemType.BeetrootSeed)
        {
            return InventoryAction.Plant;
        }
        else
        {
            return InventoryAction.Equip;
        }
    }

    public ItemDefinition[,] GetItemArray()
    {
        return itemList;
    }

    public bool TryCraftRecepie(CraftingRecepie recepie)
    {
        Debug.Log("Crafting: " +recepie.Result.ToString() + " = " + recepie.Ingredient1.ToString() + " + " + recepie.Ingredient2.ToString());
        Vector2? ing1 = findItemLocation(recepie.Ingredient1, 1);
        if(recepie.Ingredient2 is null)
        {
            if(ing1 is not null)
            {
                DestorySlotAt((uint)ing1.Value.x, (uint)ing1.Value.y);
                AddItem(new ItemDefinition { amount = 1, itemType = recepie.Result });
                return true;
            }
            else
            {
                if(equippedItemDefinition is not null && equippedItemDefinition.itemType == recepie.Ingredient1)
                {
                    SubToItemCountByTypes(equippedItemDefinition);
                    equippedItemDefinition = null;
                    AddItem(new ItemDefinition { amount = 1, itemType = recepie.Result });
                    return true;
                }
                return false;
            }
        }
        Vector2? ing2 = null;
        if (recepie.Ingredient1 == recepie.Ingredient2)
            ing2 = findItemLocation((ItemDefinition.ItemType)recepie.Ingredient2,2);
        else
            ing2 = findItemLocation((ItemDefinition.ItemType)recepie.Ingredient2, 1);
        if (ing1 is null && ing2 is null)
            return false;
        else if (ing1 is not null && ing2 is null)
        {
            if (equippedItemDefinition is not null && equippedItemDefinition.itemType == recepie.Ingredient2)
            {
                SubToItemCountByTypes(equippedItemDefinition);
                equippedItemDefinition = null;
                DestorySlotAt((uint)ing1.Value.x, (uint)ing1.Value.y);
                AddItem(new ItemDefinition { amount = 1, itemType = recepie.Result });
                return true;
            }
            else 
                return false;
        }
        else if (ing1 is null && ing2 is not null)
        {
            if (equippedItemDefinition is not null && equippedItemDefinition.itemType == recepie.Ingredient1)
            {
                SubToItemCountByTypes(equippedItemDefinition);
                equippedItemDefinition = null;
                DestorySlotAt((uint)ing2.Value.x, (uint)ing2.Value.y);
                AddItem(new ItemDefinition { amount = 1, itemType = recepie.Result });
                return true;
            }
            else 
                return false;
        }
        else
        {
            DestorySlotAt((uint)ing1.Value.x, (uint)ing1.Value.y);
            DestorySlotAt((uint)ing2.Value.x, (uint)ing2.Value.y);
            AddItem(new ItemDefinition { amount = 1, itemType = recepie.Result });
            return true;
        }
    }

    Vector2? findItemLocation(ItemDefinition.ItemType type, int whichInTurn )
    {
        int which = 0;
        for(int i = 0; i < _x; i++)
        {
            for(int j = 0; j < _y; j++)
            {
                if (itemList[i,j] is not null && itemList[i,j].itemType == type)
                {
                    which++;
                    if(which == whichInTurn)
                        return new Vector2(i, j);   
                }
            }
        }
        return null;
    }
}
