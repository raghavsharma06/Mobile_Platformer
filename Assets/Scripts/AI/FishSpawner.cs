using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script spawns or shows the jumping fish, every n number of seconds
/// </summary>
public class FishSpawner : MonoBehaviour
{
    public GameObject fish;                     // the jumping fish gameobject which will be spawned
    public float spawnDelay;                    // time differences between two fishes

    bool canSpawn;                              // ensures that fishes are spawned after some delay

    void Start()
    {
        canSpawn = true;

    }

    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine("SpawnFish");
        }
    }

    IEnumerator SpawnFish()
    {
        Instantiate(fish, transform.position, Quaternion.identity); // spawns the jumping fish
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
