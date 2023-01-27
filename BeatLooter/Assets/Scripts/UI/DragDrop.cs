using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    public Canvas Canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField]
    private UI_Inventory inventory;
    [SerializeField]
    private bool isEqSlot;
    public bool IsEqSlot => isEqSlot;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        Canvas = GetComponent<Canvas>();
        canvasGroup= GetComponent<CanvasGroup>();
    }
  
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha= 0.55f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public void OnPointerDown(PointerEventData eventData) {}

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!isEqSlot)
            inventory.UseItemByInventoryPosition(transform.parent.GetComponent<RectTransform>().anchoredPosition);
        else if(inventory.Inventory.GetSpaceLeft()>=1)
        {
            inventory.Inventory.AddItem(inventory.Inventory.GetEquippedItem());
            inventory.Inventory.DestroyEquipped();
            inventory.RefreshInventoryItems();
        }         
    }
}
