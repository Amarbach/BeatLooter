using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    [SerializeField]
    private uint x;
    public uint X => x;
    [SerializeField] 
    private uint y;
    private float itemSlotCellSize = 80f;
    public uint Y => y;
    

    private void Awake()
    {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public uint GetX() { return x; }

    public uint GetY() { return y; }

    public uint GetCapacity() { return x*y; }

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

    public void RefreshInventoryItems()
    {
        float itemSlotCellSize = 80f;
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
                if (items[i, j] is not null)
                {
                    RectTransform itemSlotReactTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                    itemSlotReactTransform.gameObject.SetActive(true);
                    itemSlotReactTransform.anchoredPosition = new Vector2(i * itemSlotCellSize, -j * itemSlotCellSize);
                    Image image = itemSlotReactTransform.Find("Image").GetComponent<Image>();
                    image.sprite = items[i, j].GetSprite();
                }              
            }
        }
    }


}
