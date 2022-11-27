
using System;

public enum ItemRarity
{
    COMMON = 0,
    UNCOMMON = 1,
    RARE = 2
}

public abstract class Item
{
    private string name;
    public virtual string Name { get => name; }
    private int value;
    public int Value { get => value; }
    private ItemRarity rarity;
    public ItemRarity Rarity { get => rarity; set => rarity = value; }

    public Item(string name, int value, ItemRarity rarity)
    {
        this.name = name;
        this.value = value;
        this.rarity = rarity;
    }

    abstract public Attributes GetAttributes();

    abstract public void BePickedBy(Character character);

    abstract public void BeUsedBy(Character character);

    public static WearableItem CreateRandomWearable(int value)
    {
        ItemRarity rarity;
        ItemType type;
        Attributes stats;

        if(value < 50) rarity = ItemRarity.COMMON;
        else if (value < 250) rarity = ItemRarity.UNCOMMON;
        else rarity = ItemRarity.RARE;
        Random RNG = new Random((int)DateTime.Now.Ticks);
        var typeRand = RNG.Next(3);
        switch (typeRand)
        {
            default: type = ItemType.WEAPON; stats = new Attributes(0, 0, 5 + ((int)rarity * 5) + RNG.Next(((int)rarity + 1) * 10), 0); break;
            case 0: type = ItemType.WEAPON; stats = new Attributes(0, 0, 5 + ((int)rarity * 5) + RNG.Next(((int)rarity + 1) * 10), 0); break;
            case 1: type = ItemType.ARMOR; stats = new Attributes(0, 0, 0, 5 + ((int)rarity * 5) + RNG.Next(((int)rarity + 1) * 10)); break;
            case 2: type = ItemType.ACCESSORY; stats = new Attributes(5 + ((int)rarity * 5) + RNG.Next(((int)rarity + 1) * 10), 0, 0, 0); break;
        }
        WearableItem ret = new WearableItem("RandomItem#"+(RNG.Next(1000)+1000), value, rarity, stats, type);
        for(int i=0; i < (int)rarity; i++)
        {
            var values = Enum.GetValues(typeof(EffectType));
            float efValue;
            EffectType efType = (EffectType)values.GetValue(RNG.Next(values.Length));
            if ((int)efType == 0 || (int)efType == 100 || (int)efType == 200) efType += 1;
            if ((int)efType < 100) efValue = (int)(RNG.NextDouble() * 15 + 10);
            else if ((int)efType < 200) efValue = (int)(RNG.NextDouble() * 10 + 10);
            else efValue = (int)(RNG.NextDouble() * 3 + 1);
            Effect effect = new Effect(efType, efValue);
            effect.ShortDesc = Effect.GetEffectDesc(efType);
            ret.GetAllEffects().Add(effect);
        }
        return ret;
    }
}

