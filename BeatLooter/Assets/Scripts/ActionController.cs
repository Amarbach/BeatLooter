using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldResult
{
    Empty,
    Patient,
    Item,
    Obstacle
}

public class ActionController : MonoBehaviour
{
    private float speed = 3f;
    public Grid grid;
    public Vector3 targetPointV;
    [SerializeField]
    private UI_Inventory inventoryUI;
    private Inventory inventory;
    bool isLocked = false;
    float actionIntensity = 1f;

    void Start()
    {
        inventory = new Inventory(inventoryUI.X, inventoryUI.Y);
        inventoryUI.SetInventory(inventory);
        targetPointV = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
        transform.position = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
        //grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
    }

    void Update()
    {
        var displacement = speed * Time.deltaTime;
        if (Vector3.Distance(transform.position, targetPointV) > displacement)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPointV, displacement);
        } 
        else
        {
            transform.position = targetPointV;
        }
    }

    public void initiateMove(Vector3 direction, float intensity)
    {
        actionIntensity = intensity;
        if (direction.x != 0 && direction.y != 0) return;
        if (direction.x > 0)
        {
            var destination = grid.GetCellCenterLocal(grid.LocalToCell(transform.position) + new Vector3Int(1, 0, 0));
            var result = CheckDestination(destination);
            TakeAction(destination, result);
        }
        else if (direction.x < 0)
        {
            var destination = grid.GetCellCenterLocal(grid.LocalToCell(transform.position) + new Vector3Int(-1, 0, 0));
            var result = CheckDestination(destination);
            TakeAction(destination, result);
        }
        else if (direction.y > 0)
        {
            var destination = grid.GetCellCenterLocal(grid.LocalToCell(transform.position) + new Vector3Int(0, 1, 0));
            var result = CheckDestination(destination);
            TakeAction(destination, result);
        }
        else if (direction.y < 0)
        {
            var destination = grid.GetCellCenterLocal(grid.LocalToCell(transform.position) + new Vector3Int(0, -1, 0));
            var result = CheckDestination(destination);
            TakeAction(destination, result);
        }
    }

    private FieldResult CheckDestination(Vector3 destination)
    {
        Collider2D other = Physics2D.OverlapCircle(new Vector2(destination.x, destination.y), 0.45f);
        if (other != null)
        {
            switch (other.tag)
            {
                default: return FieldResult.Empty;
                case "Obstacle": return FieldResult.Obstacle;
                case "Item":
                    ItemWorld itemWorld;
                    int curIntensity = (int)(actionIntensity);
                    if (other.TryGetComponent<ItemWorld>(out itemWorld) && itemWorld != null && !isLocked)
                    {
                        for(int i = curIntensity; i > 0; i--)
                        {
                            if (inventory.GetSpaceLeft() >= i)
                            {
                                for (int j = 0; j < i; j++) inventory.AddItem(itemWorld.GetItem());
                                break;
                            }
                        }
                        //isLocked = true;
                        //for(int j= 0; j < curIntensity; j++) inventory.AddItem(itemWorld.GetItem());
                        inventoryUI.RefreshInventoryItems();
                        itemWorld.DestroySelf();
                    }
                    return FieldResult.Item;
                case "Patient":
                    PatientController patient = null;
                    patient = other.GetComponent<PatientController>();
                    ItemDefinition held = inventory.GetEquippedItem();
                    CraftingRecepie needRecepie;
                    CraftingRecepie hadRecepie;
                    if(patient != null && held != null)
                    {
                        needRecepie = RecepieDict.GetValue(patient.Needed);
                        hadRecepie = RecepieDict.GetValue(held.itemType);
                        if (held.itemType == patient.Needed)
                        {
                            patient.Heal(actionIntensity);
                            patient.React(Emotion.HAPPY);
                            inventoryUI.DestroyEquipped();
                        }
                        else if (hadRecepie != null && needRecepie != null)
                        {
                            if(hadRecepie.Ingredient1 == needRecepie.Ingredient1 || hadRecepie.Ingredient1 == needRecepie.Ingredient2 ||
                                hadRecepie.Ingredient2 == needRecepie.Ingredient1 || hadRecepie.Ingredient2 == needRecepie.Ingredient2)
                            {
                                patient.React(Emotion.MIXED);
                            }
                            else
                            {
                                patient.React(Emotion.ANNOYED);
                            }
                        }
                        else if (needRecepie != null)
                        {
                            if (held.itemType == needRecepie.Ingredient1 || held.itemType == needRecepie.Ingredient2)
                            {
                                patient.React(Emotion.GOOD);
                            }
                            else
                            {
                                patient.React(Emotion.BAD);
                            }
                        }
                    }
                    return FieldResult.Patient;
            }
        }
        return FieldResult.Empty;
    }

    private void TakeAction(Vector3 destination, FieldResult destContent)
    {
        switch (destContent)
        {
            default: targetPointV = destination; break;
            case FieldResult.Empty: targetPointV = destination; break;
            case FieldResult.Obstacle: transform.position = destination; break;
            case FieldResult.Patient: transform.position = destination; break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ItemWorld itemWorld;
        //if (collision.TryGetComponent<ItemWorld>(out itemWorld) && itemWorld != null && !isLocked && inventory.GetSpaceLeft() >=1 )
        //{
        //    isLocked = true;
        //    inventory.AddItem(itemWorld.GetItem());
        //    inventoryUI.RefreshInventoryItems();
        //    itemWorld.DestroySelf();
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isLocked = false;
    }
}