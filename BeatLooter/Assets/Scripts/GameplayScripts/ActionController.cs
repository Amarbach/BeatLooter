using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FieldResult
{
    Empty,
    Enemy,
    Item,
    Obstacle
}

public class ActionController : MonoBehaviour
{
    private float speed = 5f;
    public Grid grid;
    public Vector3 targetPointV;
    private Character character;
    // Start is called before the first frame update
    void Start()
    {
        targetPointV = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
        transform.position = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
        grid = GetComponentInParent<Grid>();
        transform.position = grid.GetCellCenterLocal(grid.LocalToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPointV, speed * Time.deltaTime);
        if (character.isDead)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void initiateMove(Vector3 direction)
    {
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
                case "Enemy": return FieldResult.Enemy;
                case "Obstacle": return FieldResult.Obstacle;
                case "Item": return FieldResult.Item;
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
            case FieldResult.Enemy: 
                transform.position = destination;
                Attack(destination);
                break;
            case FieldResult.Obstacle: transform.position = destination; break;
        }
    }

    private void Attack(Vector3 destination)
    {
        Collider2D other = Physics2D.OverlapCircle(new Vector2(destination.x, destination.y), 0.45f);
        Character otherCharacter = other.GetComponent<ActionController>().GetCharacter();
        otherCharacter.DealDamageBy(this.character);
    }
    public Character GetCharacter() => character;

    public void SetCharacter(Character character) => this.character = character;
}
