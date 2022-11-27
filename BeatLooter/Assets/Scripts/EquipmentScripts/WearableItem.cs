
using System.Collections.Generic;

public enum ItemType
{
    ARMOR,
    ACCESSORY,
    WEAPON
}

public class WearableItem : Item
{
    private List<Effect> effects;
    private Attributes stats;
    private ItemType type;
    public ItemType Type { get => type; }
    public override string Name { get 
        {
            switch (type)
            {
                case ItemType.ARMOR: return base.Name + "[A]";
                case ItemType.ACCESSORY: return base.Name + "[Ac]";
                case ItemType.WEAPON: return base.Name + "[W]";
                default: return base.Name;
            }
        } }
    
    public List<Effect> GetAllEffects() { return effects; }

    public override Attributes GetAttributes()
    {
        return stats;
    }

    public override void BePickedBy(Character character)
    {
        character.GetEquipment().Add(this);
    }

    public override void BeUsedBy(Character character)
    {
        switch (type)
        {
            default: return;
            case ItemType.ARMOR: character.CurArmor = (this); return;
            case ItemType.WEAPON: character.CurWeapon = (this); return;
            case ItemType.ACCESSORY: character.CurAccessory = (this); return;
        }
    }

    public WearableItem(string name, int value, ItemRarity rarity, Attributes stats, ItemType type) : base(name, value, rarity) 
    { 
        this.type = type;
        this.stats = stats;
        this.effects = new List<Effect>();
    }
}

