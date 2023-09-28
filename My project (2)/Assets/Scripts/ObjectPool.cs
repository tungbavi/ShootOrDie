using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
   
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
       
    }
   
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary ;
    private Queue<GameObject> destroyedObjectQueue = new Queue<GameObject>(); // Queue cho cac enermy died
    // Start is called before the first frame update
    void Start()
    {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject> ();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    public GameObject spawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "does'nt exist");
            return null;
        }
        GameObject objectToSpawn ;

        if (destroyedObjectQueue.Count > 0)
        {
            
            objectToSpawn = destroyedObjectQueue.Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        }
        else if (poolDictionary[tag].Count > 0)
        {
            
            objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        }
       
        else return null;


        return objectToSpawn;
    }
    public void returnToPool(GameObject obj)
    {
        obj.SetActive(false);
        destroyedObjectQueue.Enqueue(obj);    
    }

    // Update is called once per frame
   
}
