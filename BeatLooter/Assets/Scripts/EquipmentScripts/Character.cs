using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private Attributes status;
    private List<WearableItem> equipment;
    private List<ConsumableItem> consumables;
    private WearableItem curArmor;
    public WearableItem CurArmor { get { return curArmor; } set { curArmor = value; } }
    private WearableItem curAccessory;
    public WearableItem CurAccessory { get { return curAccessory; } set { curAccessory = value; } }
    private WearableItem curWeapon;
    public WearableItem CurWeapon { get { return curWeapon; } set { curWeapon = value; } }
    public bool isDead { get { return status.cHP >= 0; } }
    private Attributes totalStatus;
    public Attributes TotalStatus { get => totalStatus; set => totalStatus = value; }

    public Character(Attributes status)
    {
        this.status = status;
        this.totalStatus = status + new Attributes(0, 0, 0, 0);
        this.equipment = new List<WearableItem>();
        this.consumables = new List<ConsumableItem>();
        this.curAccessory = null;
        this.curArmor = null;
        this.curWeapon = null;
    }

    public void PickItem(Item item)
    {
        item.BePickedBy(this);
    }

    public void AddAttributes(Attributes stats)
    {
        this.status += stats;
    }

    public List<Effect> GetAllEffects()
    {
        List<Effect> ret = new List<Effect>();
        ret.AddRange(curArmor.GetAllEffects());
        ret.AddRange(curAccessory.GetAllEffects());
        ret.AddRange(curWeapon.GetAllEffects());
        return ret;
    }

    public List<WearableItem> GetEquipment()
    {
        return equipment;
    }

    public List<ConsumableItem> GetConsumables()
    {
        return consumables;
    }
    private void UpdateTotalStatus()
    {
        totalStatus = status + new Attributes(0, 0, 0, 0);
        List<Effect> testedEffects;
        if (curWeapon != null)
        {
            totalStatus += curWeapon.GetAttributes();
            testedEffects = curWeapon.GetAllEffects();
            if (testedEffects.Count > 0)
            {
                foreach (Effect effect in testedEffects)
                {
                    if (effect.Type > EffectType.AttributesEffects && effect.Type < EffectType.OnBeatEffects)
                    {
                        effect.Alter(this);
                    }
                }
            }
        }
        if (curArmor != null)
        {
            totalStatus += curArmor.GetAttributes();
            testedEffects = curArmor.GetAllEffects();
            if (testedEffects.Count > 0)
            {
                foreach (Effect effect in testedEffects)
                {
                    if (effect.Type > EffectType.AttributesEffects && effect.Type < EffectType.OnBeatEffects)
                    {
                        effect.Alter(this);
                    }
                }
            }
        }
        if (curAccessory != null)
        {
            totalStatus += curAccessory.GetAttributes();
            testedEffects = curAccessory.GetAllEffects();
            if (testedEffects.Count > 0)
            {
                foreach (Effect effect in testedEffects)
                {
                    if (effect.Type > EffectType.AttributesEffects && effect.Type < EffectType.OnBeatEffects)
                    {
                        effect.Alter(this);
                    }
                }
            }
        }
    }
    public void UseItem(Item item)
    {
        item.BeUsedBy(this);
        UpdateTotalStatus();
    }
    public void DiscardItem(WearableItem item)
    {
        equipment.Remove(item);
        if (curWeapon == item) curWeapon = null;
        else if (curArmor == item) curArmor = null;
        else if (curAccessory == item) curAccessory = null;
        UpdateTotalStatus();
    }
}
