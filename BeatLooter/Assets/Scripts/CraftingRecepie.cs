using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class CraftingRecepie
{
    private ItemDefinition.ItemType result;
    public ItemDefinition.ItemType Result => result;
    private ItemDefinition.ItemType ingredient1;
    public ItemDefinition.ItemType Ingredient1 => ingredient1;
    private ItemDefinition.ItemType? ingredient2;
    public ItemDefinition.ItemType? Ingredient2 => ingredient2;

    public CraftingRecepie
        (
            ItemDefinition.ItemType result,
            ItemDefinition.ItemType ingredient1,
            ItemDefinition.ItemType? ingredient2 = null
        )
    {
        this.result = result;
        this.ingredient1 = ingredient1;
        this.ingredient2 = ingredient2;
    }
}

public static class RecepieDict
{
    static public Dictionary<ItemDefinition.ItemType, CraftingRecepie> craftingDict = new Dictionary<ItemDefinition.ItemType, CraftingRecepie>()
    {
        {ItemDefinition.ItemType.TomatoeSeed, new CraftingRecepie(ItemDefinition.ItemType.TomatoeSeed, ItemDefinition.ItemType.Tomatoe)  },
        {ItemDefinition.ItemType.PotatoeSeed, new CraftingRecepie(ItemDefinition.ItemType.PotatoeSeed, ItemDefinition.ItemType.Potatoe)  },
        {ItemDefinition.ItemType.BeetrootSeed, new CraftingRecepie(ItemDefinition.ItemType.BeetrootSeed, ItemDefinition.ItemType.Beetroot) },
        {ItemDefinition.ItemType.MintSeed, new CraftingRecepie(ItemDefinition.ItemType.MintSeed, ItemDefinition.ItemType.Mint) },
        {ItemDefinition.ItemType.SageSeed, new CraftingRecepie(ItemDefinition.ItemType.SageSeed, ItemDefinition.ItemType.Sage) },
        {ItemDefinition.ItemType.HeadacheMixture, new CraftingRecepie(ItemDefinition.ItemType.HeadacheMixture, ItemDefinition.ItemType.Tomatoe, ItemDefinition.ItemType.Beetroot) },
        {ItemDefinition.ItemType.PotatoeMixture,new CraftingRecepie(ItemDefinition.ItemType.PotatoeMixture, ItemDefinition.ItemType.Potatoe, ItemDefinition.ItemType.Potatoe) },
        {ItemDefinition.ItemType.BeetAndMintSoup, new CraftingRecepie(ItemDefinition.ItemType.BeetAndMintSoup, ItemDefinition.ItemType.Beetroot, ItemDefinition.ItemType.Mint) },
        {ItemDefinition.ItemType.NutritiousPotatoe, new CraftingRecepie(ItemDefinition.ItemType.NutritiousPotatoe, ItemDefinition.ItemType.Potatoe, ItemDefinition.ItemType.Sage) },
        {ItemDefinition.ItemType.BloodPotatoe, new CraftingRecepie(ItemDefinition.ItemType.BloodPotatoe, ItemDefinition.ItemType.Beetroot, ItemDefinition.ItemType.Potatoe) },
        {ItemDefinition.ItemType.BruisesOintment, new CraftingRecepie(ItemDefinition.ItemType.BruisesOintment , ItemDefinition.ItemType.Mint, ItemDefinition.ItemType.Tomatoe) },
        {ItemDefinition.ItemType.ElixisForMycosis, new CraftingRecepie(ItemDefinition.ItemType.ElixisForMycosis, ItemDefinition.ItemType.Mint, ItemDefinition.ItemType.Sage) },
    };
    public static CraftingRecepie? GetValue(ItemDefinition.ItemType key, CraftingRecepie? defaultValue = null)
    {
        CraftingRecepie? value;
        return craftingDict.TryGetValue(key, out value) ? value : defaultValue;
    }
}

