using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

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

    public void RefreshInventoryItems()
    {
        int x = 0;
        float itemSlotCellSize = 140f;
        foreach(Transform c in itemSlotContainer)
        {
            if (c == itemSlotTemplate) continue;
            Destroy(c.gameObject);
        }
        foreach(ItemDefinition item in inventory.GetItemList())
        {
            RectTransform itemSlotReactTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotReactTransform.gameObject.SetActive(true);
            itemSlotReactTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, 0f);
            Image image=itemSlotReactTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();
            x++;
        }
    }


}
