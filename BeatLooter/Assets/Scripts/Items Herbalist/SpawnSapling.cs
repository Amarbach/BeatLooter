using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnSapling : MonoBehaviour
{
    private bool isOnCooldown=false;
    [SerializeField]
    private GameObject tomatoe;
    [SerializeField]
    private GameObject potatoe;
    private int cooldown = 0; //no cooldown
    public void PlantSapling(ItemDefinition item)
    {
        if(!isOnCooldown)
        {
            isOnCooldown=true;
            if(item.itemType == ItemDefinition.ItemType.PotatoeSeed)
            {
                PlantPotatoe();
            }
            else if (item.itemType == ItemDefinition.ItemType.TomatoeSeed)
            {
                PlantTomatoe();
            }
            else
            {
                Debug.LogError("Not yet implementet for: " + item.itemType);
            }
            StartCoroutine(Wait());
        }
    }

    void PlantTomatoe()
    {
        Instantiate(tomatoe, transform.position, Quaternion.identity); 
    }

    void PlantPotatoe()
    {
        Instantiate(potatoe, transform.position, Quaternion.identity);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
    
}
