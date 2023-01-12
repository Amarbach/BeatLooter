using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Item2
{
    public enum ItemType
    {
        Potatoe,
        Tomatoe
    }
    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Potatoe: return ItemAssets.Instance.Potatoe;
            default: return ItemAssets.Instance.Tomatoe;
        }
    }
}
