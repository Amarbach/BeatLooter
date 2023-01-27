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
        Sage,
        Mint,
        PotatoeSeed,
        TomatoeSeed,
        BeetrootSeed,
        SageSeed,
        MintSeed,
        HeadacheMixture,
        PotatoeMixture,
        BeetAndMintSoup,
        NutritiousPotatoe,
        BloodPotatoe,
        BruisesOintment,
        ElixisForMycosis
    }
    public ItemType itemType;
    public int amount=1;

    public static Sprite GetSpriteFromType(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.Potatoe: return ItemAssets.Instance.Potatoe;
            case ItemType.PotatoeSeed: return ItemAssets.Instance.PotatoeSeed;
            case ItemType.Sage: return ItemAssets.Instance.Sage;
            case ItemType.SageSeed: return ItemAssets.Instance.SageSeed;
            case ItemType.Mint: return ItemAssets.Instance.Mint;
            case ItemType.MintSeed: return ItemAssets.Instance.MintSeed;
            case ItemType.TomatoeSeed: return ItemAssets.Instance.TomatoeSeed;
            case ItemType.Beetroot: return ItemAssets.Instance.Beetroot;
            case ItemType.BeetrootSeed: return ItemAssets.Instance.BeetrootSeed;
            case ItemType.HeadacheMixture: return ItemAssets.Instance.HeadacheMixture;
            case ItemType.PotatoeMixture: return ItemAssets.Instance.PotatoeMixture;
            case ItemType.BeetAndMintSoup: return ItemAssets.Instance.BeetAndMintSoup;
            case ItemType.NutritiousPotatoe: return ItemAssets.Instance.NutritiousPotatoe;
            case ItemType.BloodPotatoe: return ItemAssets.Instance.BloodPotatoe;
            case ItemType.BruisesOintment: return ItemAssets.Instance.BruisesOintment;
            case ItemType.ElixisForMycosis: return ItemAssets.Instance.ElixisForMycosis;
            default: return ItemAssets.Instance.Tomatoe;
        }
    }

    public Sprite GetSprite()
    {
        return GetSpriteFromType(itemType);
    }
}
