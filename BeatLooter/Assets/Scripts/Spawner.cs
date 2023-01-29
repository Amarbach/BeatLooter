using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] private int spawnerRadius;
    private float tickTimer;
    [SerializeField] private float tickRate;  //Tick Rate for Spawning Objects
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tickTimer += Time.deltaTime;
        if(tickTimer >= tickRate)
        {
            tickTimer -= tickRate;
            int radiusX = Random.Range(-1* spawnerRadius, spawnerRadius);
            int radiusY = Random.Range(-1* spawnerRadius, spawnerRadius);
            Vector3 position = transform.position + new Vector3(radiusX, radiusY, 0);
            Instantiate(objectToBeSpawned, position, Quaternion.identity); //
        }
    }
}
