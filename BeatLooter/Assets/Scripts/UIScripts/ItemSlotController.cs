using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlotController : MonoBehaviour
{
    public Item item;
    public TextMeshProUGUI TextName;
    public TextMeshProUGUI TextStats;

    void Awake()
    {
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        if (texts[0].text.Equals("Name\n"))
        {
            TextName = texts[0];
            TextStats = texts[1];
        }
        else
        {
            TextName = texts[1];
            TextStats = texts[0];
        }
    }

    public void SetItem(Item item)
    {
        this.item = item;
        switch (item.Rarity)
        {
            case ItemRarity.COMMON: TextName.color = Color.white; break;
            case ItemRarity.UNCOMMON: TextName.color = Color.green; break;
            case ItemRarity.RARE: TextName.color = Color.blue; break;
            default: TextName.color = Color.white; break;
        }
        TextName.text = item.Name;
        TextStats.text = item.GetAttributes().ToString();
    }
}
