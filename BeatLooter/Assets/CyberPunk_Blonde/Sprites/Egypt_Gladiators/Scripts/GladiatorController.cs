using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorController : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    [SerializeField]
    Animator anim;
    [SerializeField]
    Renderer _renderer;
    private Inventory inventory;
    [SerializeField]
    private UI_Inventory inventoryUI;
    public float runSpeed = 20.0f;

    int up = Animator.StringToHash("WalkUp");
    int right = Animator.StringToHash("WalkRight");
    int left = Animator.StringToHash("WalkLeft");
    int down = Animator.StringToHash("WalkDown");
    int attack = Animator.StringToHash("Attack");
    int idle = Animator.StringToHash("Idle");
    bool isLocked = false;
    void Start()
    {
        inventory = new Inventory();
        body = GetComponent<Rigidbody2D>();
        inventoryUI.SetInventory(inventory);

        //ItemWorld.SpawnItemWorld(new Vector3(-5, 9, -2), new Item {itemType = Item.ItemType.Potatoe , amount =1});
        //ItemWorld.SpawnItemWorld(new Vector3(9.5f, 11.56f, -2), new Item { itemType = Item.ItemType.Tomatoe, amount = 1 });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld=collision.GetComponent<ItemWorld>();
        if(itemWorld!=null && !isLocked && inventory.GetItemList().Count<4)
        {
            isLocked = true;
            inventory.AddItem(itemWorld.GetItem());
            inventoryUI.RefreshInventoryItems();
            itemWorld.DestroySelf();
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isLocked = false;
    }
    void Update()
    {
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        if (horizontal > 0.02)
        {
            anim.SetBool("WalkRight", true);
            anim.SetBool("Idle", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkDown", false);
            anim.SetBool("WalkUp", false);
        }
        else if (horizontal < -0.02)
        {
            anim.SetBool("WalkLeft", true);
            anim.SetBool("Idle", false);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkDown", false);
            anim.SetBool("WalkUp", false);
        }
        else if (vertical > 0.02)
        {
            anim.SetBool("WalkUp", true);
            anim.SetBool("Idle", false);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkDown", false);
        }
        else if (vertical < -0.02)
        {
            anim.SetBool("WalkDown", true);
            anim.SetBool("Idle", false);
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkUp", false);
        }
        else
        {
            anim.SetBool("WalkLeft", false);
            anim.SetBool("WalkRight", false);
            anim.SetBool("Idle", true);
            anim.SetBool("WalkDown", false);
            anim.SetBool("WalkUp", false);

        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
