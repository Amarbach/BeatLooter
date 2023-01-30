using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    [Serializable]
    public class RefreshOther : UnityEvent { }
    public RefreshOther refresh;
    private Inventory inventory;
    public Inventory Inventory => inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private uint x;
    public uint X => x;
    [SerializeField] 
    private uint y;
    private float itemSlotCellSize = 64f;
    public uint Y => y;
    Transform allEqBackground;
    Transform onlyToolbarEqBackground;
    [SerializeField]
    private Transform eqSlot;
    [Serializable]
    public class PlantEvent : UnityEvent<ItemDefinition> {}
    public PlantEvent PlantResult = new PlantEvent();

    private void Awake()
    {
        allEqBackground = transform.Find("Background");
        onlyToolbarEqBackground = transform.Find("BackgroundToolbar");
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Item1"))
        {
            uint x = 0, y =0;
            UseItemAtXY(x, y);
        }
        else if (Input.GetButtonDown("Item2"))
        {
            uint x = 1, y = 0;
            UseItemAtXY(x, y);
        }
        else if (Input.GetButtonDown("Item3"))
        {
            uint x = 2, y = 0;
            UseItemAtXY(x, y);
        }
        else if (Input.GetButtonDown("Item4"))
        {
            uint x = 3, y = 0;
            UseItemAtXY(x, y);
        }
        else if (Input.GetButtonDown("Item5"))
        {
            uint x = 4, y = 0;
            UseItemAtXY(x, y);
        }
        else if (Input.GetButtonDown("Item6"))
        {
            uint x = 5, y = 0;
            UseItemAtXY(x, y);
        }
        else if (Input.GetButtonDown("Equipped"))
        {
            if (Inventory.GetEquippedItem() is not null  && Inventory.GetSpaceLeft() >= 1)
            {
                Inventory.AddItem(Inventory.GetEquippedItem());
                Inventory.DestroyEquipped();
                RefreshInventoryItems();
            }
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void CraftFromRecepie(CraftingRecepie craftingRecepie)
    {
        if(inventory.TryCraftRecepie(craftingRecepie))
            RefreshInventoryItems();
    }

    public uint GetX() { return x; }

    public uint GetY() { return y; }

    public uint GetCapacity() { return x*y; }

    public void SwapHideOrShowAllEq()
    {
        allEqBackground.gameObject.SetActive(!allEqBackground.gameObject.activeSelf);
        onlyToolbarEqBackground.gameObject.SetActive(!allEqBackground.gameObject.activeSelf);
        RefreshInventoryItems();
    }

    public void Swap(Vector2 source, Vector2 target)
    {
        if (source == eqSlot.GetComponent<RectTransform>().anchoredPosition)
        {
            SwapEq(target, target);
            RefreshInventoryItems();
            return;
        }
        float i1 = source.x / itemSlotCellSize;
        float j1 = -source.y / itemSlotCellSize;

        float i2 = target.x / itemSlotCellSize;
        float j2 = -target.y / itemSlotCellSize;

        inventory.SwapAt((uint)i1, (uint)j1, (uint)i2, (uint)j2);
        RefreshInventoryItems();
    }

    public void SwapEq(Vector2 source, Vector2 target)
    {
        float i1 = source.x / itemSlotCellSize;
        float j1 = -source.y / itemSlotCellSize;

        inventory.SwapEqAt((uint)i1, (uint)j1);
        RefreshInventoryItems();
    }


    private void Plant(uint x, uint y)
    {
        PlantResult.Invoke(inventory.GetAt(x, y));
        inventory.DestorySlotAt(x, y);
    }

    private void Equip(uint x, uint y)
    {
        inventory.SwapEqAt(x, y);
    }

    private void Throw(uint x, uint y)
    {
        //TODO *or not :P*
    }

    public void UseItemAtXY(uint x, uint y)
    {
        InventoryAction? inventoryAction = inventory.UseItem(x, y);
        if (inventoryAction is null) return;
        if (inventoryAction == InventoryAction.Equip)
        {
            Equip(x, y);
            RefreshInventoryItems();
        }
        else if (inventoryAction == InventoryAction.Plant)
        {
            Plant(x, y);
            RefreshInventoryItems();
        }
        else
        {
            Throw(x, y);
            RefreshInventoryItems();
        }
    }

    public void UseItemByInventoryPosition(Vector2 item)
    {
        float i1 = item.x / itemSlotCellSize;
        float j1 = -item.y / itemSlotCellSize;
        UseItemAtXY((uint)i1, (uint)j1);
    }

    public void DestroySlot(GameObject gameObject)
    {
        RectTransform itemSlotReactTransform;
        if (gameObject.TryGetComponent<RectTransform>(out itemSlotReactTransform))
        {
            float i = itemSlotReactTransform.anchoredPosition.x / itemSlotCellSize;
            float j = -itemSlotReactTransform.anchoredPosition.y / itemSlotCellSize;
            inventory.DestorySlotAt((uint)i, (uint)j);
            RefreshInventoryItems();
        }
    }

    public void DestroyEquipped()
    {
        if (inventory.GetEquippedItem() != null)
        {
            inventory.DestroyEquipped();
            RefreshInventoryItems();
        }
    }

    public void RefreshInventoryItems()
    {
        refresh.Invoke();
        var equippedItem = inventory.GetEquippedItem();
        if (equippedItem == null)
        {
            var img = eqSlot.Find("Image");
            img.gameObject.SetActive(false);
        }
        else
        {
            var img = eqSlot.Find("Image");
            img.gameObject.SetActive(true);
            var image = img.GetComponent<Image>();
            image.sprite = equippedItem.GetSprite();
            DragDrop dragDrop = img.GetComponent<DragDrop>();
            dragDrop.Canvas = canvas;
        }
        foreach(Transform c in itemSlotContainer)
        {
            if (c == itemSlotTemplate) continue;
            Destroy(c.gameObject);
        }
        var items = inventory.GetItemArray();
        
        for (uint i = 0; i < X; i++)
        {
            for (uint j = 0; j < Y; j++)
            {
                bool draw = true;
                if(onlyToolbarEqBackground.gameObject.activeSelf && j>0)
                {
                    draw = false;
                }
                if(draw)
                {
                    RectTransform itemSlotReactTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                    itemSlotReactTransform.gameObject.SetActive(true);
                    itemSlotReactTransform.anchoredPosition = new Vector2(i * itemSlotCellSize, -j * itemSlotCellSize);
                    if (items[i, j] is not null)
                    {
                        var img = itemSlotReactTransform.Find("Image");
                        Image image = img.GetComponent<Image>();
                        image.sprite = items[i, j].GetSprite();
                        DragDrop dragDrop = img.GetComponent<DragDrop>();
                        dragDrop.Canvas = canvas;
                    }
                    else
                    {
                        var image = itemSlotReactTransform.Find("Image").GetComponent<Image>();
                        //var button = itemSlotReactTransform.Find("Button").GetComponent<Button>();
                        Destroy(image);
                        //Destroy(button);
                    }
                }
            }
        }
    }
}
