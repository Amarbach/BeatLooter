using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CraftSlot : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField]
    private UI_Inventory inventory;
    public CraftingRecepie craftingRecepie;

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.CraftFromRecepie(craftingRecepie);
    }
}

