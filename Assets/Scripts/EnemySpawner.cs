using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)] [SerializeField] float spawnInterval = 15f;    
    [SerializeField] EnemyMovement enemy;   //having the type as enemymovement makes it so that anything that doesnt have this script attached to cannot be referenced in the inspector

    void Start() 
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)    //forever
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
