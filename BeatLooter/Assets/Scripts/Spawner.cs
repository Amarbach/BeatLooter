using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] private float spawnerRadius;
    private float tickTimer;
    [SerializeField] private float tickRate;  //Tick Rate for Spawning Objects

    private GameObject spawnedObject;
    private readonly List<ItemDefinition.ItemType> availableCures = new List<ItemDefinition.ItemType>()
    {
        ItemDefinition.ItemType.HeadacheMixture,
        ItemDefinition.ItemType.PotatoeMixture,
        ItemDefinition.ItemType.BeetAndMintSoup,
        ItemDefinition.ItemType.NutritiousPotatoe,
        ItemDefinition.ItemType.BloodPotatoe,
        ItemDefinition.ItemType.BruisesOintment,
        ItemDefinition.ItemType.ElixisForMycosis
    };
     
    
    void Awake()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedObject is null)
            tickTimer += Time.deltaTime;
        if(tickTimer >= tickRate)
        {
            tickTimer -= tickRate;
            Spawn();
        }
    }
    private void Spawn()
    {
        float radiusX = Random.Range(-1 * spawnerRadius, spawnerRadius);
        float radiusY = Random.Range(-1 * spawnerRadius, spawnerRadius);
        Vector3 position = transform.position + new Vector3(radiusX, radiusY, 0);
        spawnedObject = Instantiate(objectToBeSpawned, position, Quaternion.identity, transform); 
        if( spawnedObject.TryGetComponent<PatientController>(out PatientController patientController) )
        {
            int index = Random.Range(0, availableCures.Count);
            patientController.SetCure(availableCures[index]);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if(spawnerRadius > 0)
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnerRadius*2, spawnerRadius*2, spawnerRadius*2));
    }
}
