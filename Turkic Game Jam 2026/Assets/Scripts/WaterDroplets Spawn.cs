using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDropletsSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;

    public float spawnCooldown;
    private float timeToRespawn;

    public GameObject waterDropletObj;

    public Transform parentObj;

    void Start() {
        timeToRespawn = spawnCooldown;    
    }

    void Update()
    {
        timeToRespawn -= Time.deltaTime;

        if (timeToRespawn <= 0)
        {
            SpawnDroplet();
        }
    }

    void SpawnDroplet()
    {
        int rand = Random.Range(0, spawnPoints.Length);

        Instantiate(waterDropletObj, spawnPoints[rand].position, Quaternion.identity, parentObj);

        timeToRespawn = spawnCooldown; 
    }
}
