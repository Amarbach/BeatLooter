using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Attributes
{
    private int maxHP;
    public int mHP { get => maxHP; private set => maxHP = value; }
    private int curHP;
    public int cHP { get => curHP; set { curHP = value; if (curHP > maxHP) curHP = maxHP; else if (curHP < 0) curHP = 0; } }
    private int atk;
    public int ATK { get => atk; private set => atk = value; }
    private int def;
    public int DEF { get => def; private set => def = value; }

    public Attributes(int maxHP, int curHP, int atk, int def)
    {
        this.maxHP = maxHP;
        this.curHP = curHP;
        this.atk = atk;
        this.def = def;
    }

    public static Attributes operator+(Attributes a, Attributes b) => new Attributes(a.mHP + b.mHP, a.cHP + b.cHP, a.ATK + b.ATK, a.DEF + b.DEF);
    public static Attributes operator-(Attributes a) => new Attributes(-a.mHP, -a.cHP, -a.ATK, -a.DEF);
    public static Attributes operator -(Attributes a, Attributes b) => a + (-b);

    public override string ToString()
    {
        return "HP:" + maxHP + " ATK:" + atk + " DEF:" + def;
    }
}

