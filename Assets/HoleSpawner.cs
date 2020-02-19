using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    public float spawnEvery;

    ObjectPool objectPool;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.instance;
        InvokeRepeating("SpawnWithDelay", spawnEvery, spawnEvery);
    }
    void SpawnWithDelay()
    {
        objectPool.SpawnFromPool("hole");
    }
}
