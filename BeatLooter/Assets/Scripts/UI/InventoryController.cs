using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public GameObject slot;
    public RectTransform itemSpace;
    public TextMeshProUGUI textArmor;
    public TextMeshProUGUI textAccessory;
    public TextMeshProUGUI textWeapon;
    public TextMeshProUGUI textATK;
    public TextMeshProUGUI textDEF;
    public TextMeshProUGUI textHP;

    public TextMeshProUGUI textItemName;
    public TextMeshProUGUI textItemType;
    public TextMeshProUGUI textItemStat;
    public TextMeshProUGUI textItemEffects;

    public Button equipBtn;
    public Button discardBtn;
    //private List<Button> itemButtons = new();

    CharacterCarrier carrier;

    [SerializeField] private WearableItem curItem;

    private Character character;
    // Start is called before the first frame update
    void Start()
    {
        equipBtn.onClick.AddListener(OnEquipBtn);
        discardBtn.onClick.AddListener(OnDiscardBtn);
        carrier = GameObject.Find("CharacterCarrier").GetComponent<CharacterCarrier>();
        character = carrier.GetCharacter();
        var equipment = carrier.GetCharacter().GetEquipment();

        if (equipment.Count == 0)
        {
            equipment.Add(new WearableItem("ComplementaryArmor", 1, ItemRarity.COMMON, new Attributes(0, 0, 0, 1), ItemType.ARMOR));
            equipment.Add(new WearableItem("ComplementaryWeapon", 1, ItemRarity.COMMON, new Attributes(0, 0, 3, 0), ItemType.WEAPON));
            equipment.Add(new WearableItem("ComplementaryAccessory", 1, ItemRarity.COMMON, new Attributes(20, 0, 0, 0), ItemType.ACCESSORY));
            equipment.Add(Item.CreateRandomWearable(300));
            equipment.Add(Item.CreateRandomWearable(20));
            equipment.Add(Item.CreateRandomWearable(200));
            equipment.Add(Item.CreateRandomWearable(100));
        }

        ArrangeEquipment();
        UpdateStatusText();
    }

    private void AddItemSlot(WearableItem item, int position)
    {
        GameObject instance = Instantiate(slot, itemSpace);
        instance.GetComponent<Button>().onClick.AddListener(delegate { SetCurItem(item); });
        RectTransform rect = instance.GetComponent<RectTransform>();
        rect.offsetMax = new Vector2(0, -position * 30);
        rect.offsetMin = new Vector2(0, itemSpace.rect.height-((position+1)*30));
        ItemSlotController curSlot = instance.GetComponent<ItemSlotController>();
        curSlot.SetItem(item);
    }
    private void UpdateStatusText()
    {
        if (character.CurWeapon != null) textWeapon.text = "Weapon: " + character.CurWeapon.Name;
        else textWeapon.text = "Weapon: None";
        if (character.CurAccessory != null) textAccessory.text = "Acc: " + character.CurAccessory.Name;
        else textAccessory.text = "Accessory: None";
        if (character.CurArmor != null) textArmor.text = "Armor:" + character.CurArmor.Name;
        else textArmor.text = "Armor: None";

        textATK.text = "ATK: " + character.TotalStatus.ATK;
        textDEF.text = "DEF: " + character.TotalStatus.DEF;
        textHP.text = "HP: " + character.TotalStatus.cHP + "/" + character.TotalStatus.mHP;
    }
    private void UpdateItemText()
    {
        textItemName.text = curItem.Name;
        string type;
        string stat;
        switch (curItem.Type)
        {
            default: type = "None"; stat = "None"; break;
            case ItemType.ARMOR: type = "Armor"; stat = "DEF: " + curItem.GetAttributes().DEF; break;
            case ItemType.WEAPON: type = "Weapon"; stat = "ATK: " + curItem.GetAttributes().ATK; break;
            case ItemType.ACCESSORY: type = "Accessory"; stat = "Max HP: " + curItem.GetAttributes().mHP; break;
        }
        textItemType.text = type;
        textItemStat.text = stat;
        string effects = "Effects:\n";
        foreach(Effect effect in curItem.GetAllEffects())
        {
            effects += effect.ShortDesc + " (" + effect.Value + ")\n";
        }
        textItemEffects.text = effects;
    }
    public void SetCurItem(WearableItem item)
    {
        this.curItem = item;
        UpdateItemText();
    }
    private void ArrangeEquipment()
    {
        var equipment = carrier.GetCharacter().GetEquipment();
        for (int i = 0; i < equipment.Count; i++)
        {
            AddItemSlot(equipment[i], i);
        }
    }
    public void OnEquipBtn()
    {
        if (curItem != null) character.UseItem(curItem);
        UpdateStatusText();
    }
    public void OnDiscardBtn()
    {
        if (curItem != null) character.DiscardItem(curItem);
        UpdateStatusText();
        ItemSlotController[] children = this.gameObject.GetComponentsInChildren<ItemSlotController>();
        for(int i =0; i< children.Length; i++)
        {
            Destroy(children[i].gameObject);
        }
        ArrangeEquipment();
    }
}
