using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemDefinition
{
    public enum ItemType
    {
        Potatoe,
        Tomatoe,
        Beetroot,
        HeadacheMixture,
        PotatoeMixture,
        PotatoeSeed,
        TomatoeSeed,
        BeetrootSeed        
    }
    public ItemType itemType;
    public int amount=1;

    public static Sprite GetSpriteFromType(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Potatoe: return ItemAssets.Instance.Potatoe;
            case ItemType.HeadacheMixture: return ItemAssets.Instance.HeadacheMixture;
            case ItemType.PotatoeMixture: return ItemAssets.Instance.PotatoeMixture;
            case ItemType.PotatoeSeed: return ItemAssets.Instance.PotatoeSeed;
            case ItemType.TomatoeSeed: return ItemAssets.Instance.TomatoeSeed;
            case ItemType.Beetroot: return ItemAssets.Instance.Beetroot;
            case ItemType.BeetrootSeed: return ItemAssets.Instance.BeetrootSeed;
            default: return ItemAssets.Instance.Tomatoe;
        }
    }

    public Sprite GetSprite()
    {
        return GetSpriteFromType(itemType);
    }
}
