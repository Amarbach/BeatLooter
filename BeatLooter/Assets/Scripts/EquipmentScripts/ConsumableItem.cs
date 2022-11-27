
public class ConsumableItem : Item
{
    private static int maxQuantity = 10;

    private Attributes effectModifier;
    private int duration;
    private int quantity;
    public int Quantity { get => quantity; }

    public ConsumableItem(string name, int value, ItemRarity rarity, Attributes effectModifier) : base(name, value, rarity)
    {
        this.effectModifier = effectModifier;
        this.quantity = 1;
        this.duration = 3;
    }

    public void changeQuantity(int value)
    {
        if (quantity < maxQuantity) quantity += value;
    }

    public override Attributes GetAttributes()
    {
        return effectModifier;
    }

    public override void BePickedBy(Character character)
    {
        character.GetConsumables().Add(this);
    }

    public override void BeUsedBy(Character character)
    {
        character.AddAttributes(effectModifier);
    }
}

