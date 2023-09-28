using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn ;
    private ObjectPool objectPool;
    [SerializeField]
    private GameObject prefab;
    private string tagg;
   
       // Start is called before the first frame update
    void Start()
    {
        objectPool = FindAnyObjectByType<ObjectPool>();
        StartCoroutine(SpawnEnemies());
        tagg = prefab.tag;
        
    }

    // Update is called once per frame
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToSpawn);
           
            prefab = objectPool.spawnFromPool(tagg, new Vector3(Random.Range(-50f, 50f), 3.51f, Random.Range(-50f, 50f)), Quaternion.identity);
           
        }

        // transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
