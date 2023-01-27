using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.Events;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;
using static UnityEditor.Progress;

public class UI_Crafting : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    Transform allEqBackground;
    [SerializeField]
    private Transform eqSlot;

    [SerializeField]
    public GameObject content;
    int create = 5;
    [SerializeField]
    private UI_Inventory ui_inventory;
    private float setAlpha = 180f;

    List<CraftingRecepie> craftingRecepies = new List<CraftingRecepie>()
    {
        new CraftingRecepie(ItemDefinition.ItemType.TomatoeSeed, ItemDefinition.ItemType.Tomatoe),
        new CraftingRecepie(ItemDefinition.ItemType.PotatoeSeed, ItemDefinition.ItemType.Potatoe),
        new CraftingRecepie(ItemDefinition.ItemType.BeetrootSeed, ItemDefinition.ItemType.Beetroot),
        new CraftingRecepie(ItemDefinition.ItemType.MintSeed, ItemDefinition.ItemType.Mint),
        new CraftingRecepie(ItemDefinition.ItemType.SageSeed, ItemDefinition.ItemType.Sage),
        new CraftingRecepie(ItemDefinition.ItemType.HeadacheMixture, ItemDefinition.ItemType.Tomatoe, ItemDefinition.ItemType.Beetroot),
        new CraftingRecepie(ItemDefinition.ItemType.PotatoeMixture, ItemDefinition.ItemType.Potatoe, ItemDefinition.ItemType.Potatoe),
        new CraftingRecepie(ItemDefinition.ItemType.BeetAndMintSoup, ItemDefinition.ItemType.Beetroot, ItemDefinition.ItemType.Mint),
        new CraftingRecepie(ItemDefinition.ItemType.NutritiousPotatoe, ItemDefinition.ItemType.Potatoe, ItemDefinition.ItemType.Sage),
        new CraftingRecepie(ItemDefinition.ItemType.BloodPotatoe, ItemDefinition.ItemType.Beetroot, ItemDefinition.ItemType.Potatoe),
        new CraftingRecepie(ItemDefinition.ItemType.BruisesOintment , ItemDefinition.ItemType.Mint, ItemDefinition.ItemType.Tomatoe),
        new CraftingRecepie(ItemDefinition.ItemType.ElixisForMycosis, ItemDefinition.ItemType.Mint, ItemDefinition.ItemType.Sage),
    };

    private void Awake()
    {
        allEqBackground = transform.Find("BackgroundCrafting");
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("ItemSlotTemplate");
    }

    private void Start()
    {
        RefreshCrafting();
    }

    public void RefreshCrafting()
    {
        if (gameObject.activeSelf)
        {
            foreach (Transform c in content.transform)
            {
                if (c == itemSlotTemplate) continue;
                Destroy(c.gameObject);
            }

            foreach (var recepie in craftingRecepies)
            {
                Inventory inventory = ui_inventory.Inventory;
                RectTransform itemSlotReactTransform = Instantiate(itemSlotTemplate, content.transform).GetComponent<RectTransform>();
                itemSlotReactTransform.gameObject.SetActive(true);
                var crafted = itemSlotReactTransform.Find("Crafted");
                var ingredient1 = itemSlotReactTransform.Find("Ingredient1");
                var ingredient2 = itemSlotReactTransform.Find("Ingredient2");
                crafted.GetComponent<CraftSlot>().craftingRecepie = recepie;
                var imageCraft = crafted.Find("Image");
                var imageIngredient1 = ingredient1.Find("Image");
                var imageIngredient2 = ingredient2.Find("Image");
                var imageComponent = imageCraft.GetComponent<Image>();
                imageComponent.sprite = ItemDefinition.GetSpriteFromType(recepie.Result);
                imageComponent = imageIngredient1.GetComponent<Image>();
                imageComponent.sprite = ItemDefinition.GetSpriteFromType(recepie.Ingredient1);
                if (inventory.GetCountOfType(recepie.Ingredient1) > 0)
                {
                    imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, setAlpha / 255f);
                }
                if (recepie.Ingredient2 != null)
                {
                    imageComponent = imageIngredient2.GetComponent<Image>();
                    imageComponent.sprite = ItemDefinition.GetSpriteFromType((ItemDefinition.ItemType)recepie.Ingredient2);
                    if ((inventory.GetCountOfType((ItemDefinition.ItemType)recepie.Ingredient2) > 0
                        && (ItemDefinition.ItemType)recepie.Ingredient2 != recepie.Ingredient1) //if different the igr in inventrory>0
                        ||
                        (inventory.GetCountOfType((ItemDefinition.ItemType)recepie.Ingredient2) > 1
                        && (ItemDefinition.ItemType)recepie.Ingredient2 == recepie.Ingredient1)) //if the same the ingr in inventory must be at least 2
                    {
                        imageComponent.color = new Color(imageComponent.color.r, imageComponent.color.g, imageComponent.color.b, setAlpha / 255f);
                    }
                }
                else
                {
                    imageIngredient2.gameObject.SetActive(false);
                }
            }
        }
    }
}
public class CraftingRecepie
{
    private ItemDefinition.ItemType result;
    public ItemDefinition.ItemType Result => result;
    private ItemDefinition.ItemType ingredient1;
    public ItemDefinition.ItemType Ingredient1 => ingredient1;
    private ItemDefinition.ItemType? ingredient2;
    public ItemDefinition.ItemType? Ingredient2 => ingredient2;

    public CraftingRecepie
        (
            ItemDefinition.ItemType result,
            ItemDefinition.ItemType ingredient1,
            ItemDefinition.ItemType? ingredient2 = null
        )
    {
        this.result = result;
        this.ingredient1 = ingredient1;
        this.ingredient2 = ingredient2;
    }
}
