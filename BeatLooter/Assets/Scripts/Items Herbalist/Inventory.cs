using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static Item;
using static UnityEditor.Experimental.GraphView.Port;

public class Inventory
{
    private ItemDefinition[,] itemList;
    private uint capacity;
    private uint _x;
    private uint _y;
    public uint Capacity => capacity;

    public Inventory(uint x=4, uint y=1)
    {
        _y = y;
        _x = x;
        capacity = x * y;
        itemList = new ItemDefinition[x,y];
    }

    public void AddItem(ItemDefinition item)
    {
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

    public void DestorySlotAt(uint x, uint y)
    {
        itemList[x, y] = null;
    }

    public ItemDefinition[,] GetItemArray()
    {
        return itemList;
    }
}
