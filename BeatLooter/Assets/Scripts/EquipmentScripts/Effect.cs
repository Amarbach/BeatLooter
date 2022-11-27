
using System.Collections.Generic;

public enum EffectType: int
{
    RythmEffects = 0,
    PerfectHitAlter = 1,

    AttributesEffects = 100,
    AdditionalHealth = 101,
    AdditionalAtk = 102,
    AdditionalDef = 103,

    OnBeatEffects = 200,
    OnBeatHeal = 201
}

public class Effect
{
    static private Dictionary<EffectType, string> descriptions;
    private EffectType type;
    public EffectType Type { get { return type; } }
    private float value;
    public float Value { get { return value; } }
    private string shortDesc;
    public string ShortDesc { get => shortDesc; set => shortDesc = value; }

    public Effect(EffectType type, float value)
    {
        this.type = type;
        this.value = value;
    }

    public void Alter(Character v)
    {
        switch (type)
        {
            default: return;
            case EffectType.AdditionalHealth: v.TotalStatus += new Attributes((int)value, 0, 0, 0); return;
            case EffectType.AdditionalAtk: v.TotalStatus += new Attributes(0, 0, (int)value, 0); return;
            case EffectType.AdditionalDef: v.TotalStatus += new Attributes(0, 0, 0, (int)value); return;
        }
    }

    public static string GetEffectDesc(EffectType type)
    {
        if (descriptions == null)
        {
            descriptions = new Dictionary<EffectType, string>();
            descriptions.Add(EffectType.PerfectHitAlter, "Perfect beat is % easier");
            descriptions.Add(EffectType.AdditionalDef, "Additional defense");
            descriptions.Add(EffectType.AdditionalAtk, "Additional attack");
            descriptions.Add(EffectType.AdditionalHealth, "Additional health");
            descriptions.Add(EffectType.OnBeatHeal, "Perfect beat heals");
        }
        return descriptions[type];
    }
}

