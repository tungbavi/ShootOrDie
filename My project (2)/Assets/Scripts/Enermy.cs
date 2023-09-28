using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enermy : MonoBehaviour,IDamgable
{
    private ObjectPool objectPool;
    public NavMeshAgent enemy;
    private GameObject player;
    public float health = 10f;
    
   
    void Start()
    {
        objectPool = FindAnyObjectByType<ObjectPool>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = gameObject.GetComponent<NavMeshAgent>();
        
    }
  
    private void Update()
    {
        enemy.SetDestination(player.transform.position);
        // transform.position = Vector3.MoveTowards(transform.position, player.position, 8 * Time.deltaTime);
    }
    
    public void TakeDamage(float amount)
    {

            health -= amount;
            Debug.Log("hihsdiihfi");
            if (health <= 0)
            {              
                objectPool.returnToPool(gameObject);
                player.GetComponent<PlayerMovement>().Kills++;
            // objectPool.SetActive(false);
        }
        
        
    }
  
}
