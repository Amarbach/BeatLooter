using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public bool isLocked=false;
    public static ItemWorld SpawnItemWorld(Vector3 position, Item2 item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity); ;
        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    private Item2 item;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItem(Item2 item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public Item2 GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}