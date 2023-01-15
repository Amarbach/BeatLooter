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
    public void PlantSapling(Image img)
    {
        if(!isOnCooldown)
        {
            isOnCooldown=true;
            if(img.sprite== ItemAssets.Instance.Potatoe)
            {
                PlantPotatoe();
            }
            else
            {
                PlantTomatoe();
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
