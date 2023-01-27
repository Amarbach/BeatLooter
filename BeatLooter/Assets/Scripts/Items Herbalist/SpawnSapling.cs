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
    [SerializeField]
    private GameObject beetroot;
    [SerializeField]
    private GameObject sage;
    [SerializeField]
    private GameObject mint;
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
            else if (item.itemType == ItemDefinition.ItemType.BeetrootSeed)
            {
                Instantiate(beetroot, transform.position, Quaternion.identity);
            }
            else if (item.itemType == ItemDefinition.ItemType.SageSeed)
            {
                Instantiate(sage, transform.position, Quaternion.identity);
            }
            else if (item.itemType == ItemDefinition.ItemType.MintSeed)
            {
                Instantiate(mint, transform.position, Quaternion.identity);
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
