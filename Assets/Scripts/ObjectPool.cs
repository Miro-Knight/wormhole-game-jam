using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    public GameObject planet;
   

    private void Awake()
    {
        instance = this;
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> pools;

    public Dictionary<string, Queue<GameObject>> poolDict;
    // Start is called before the first frame update
    void Start()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                Vector3 spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x / 2.06f) + pool.prefab.transform.localScale.y) + planet.transform.position;
                Quaternion spawnRotation = Random.rotation;
                if (pool.tag == "worm")
                {
                    spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x / 1.98f) + pool.prefab.transform.localScale.y) + planet.transform.position;
                   

                }
                if (pool.tag == "hole")
                {
                    spawnPosition = Random.onUnitSphere * ((planet.transform.localScale.x / 1.98f) + pool.prefab.transform.localScale.y) + planet.transform.position;


                }
                // Quaternion spawnRotation = Quaternion.identity;
                GameObject obj = Instantiate(pool.prefab, spawnPosition,spawnRotation);
                obj.AddComponent<BoxCollider>();
                obj.AddComponent<FauxGravityBody>();
                obj.GetComponent<FauxGravityBody>().attractor = planet.GetComponent<FauxGravityAttractor>();

                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDict.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesnt exist");
            return null;
        }

        GameObject objectToSpawn = poolDict[tag].Dequeue();

        objectToSpawn.SetActive(true);

        poolDict[tag].Enqueue(objectToSpawn);

        return objectToSpawn;

    }
}
