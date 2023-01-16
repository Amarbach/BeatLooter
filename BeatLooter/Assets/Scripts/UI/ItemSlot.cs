using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [Serializable]
    public class DropEvent : UnityEvent<Vector2, Vector2> { }
    public DropEvent DropResult = new DropEvent();
    private RectTransform ownRectTransform;
    void Awake()
    {
        ownRectTransform= GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.TryGetComponent<DragDrop>(out var dropped);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = ownRectTransform.anchoredPosition;
            DropResult.Invoke(dropped.transform.parent.GetComponent<RectTransform>().anchoredPosition, ownRectTransform.anchoredPosition);
            dropped.OnEndDrag(eventData);
        }
    }
}
