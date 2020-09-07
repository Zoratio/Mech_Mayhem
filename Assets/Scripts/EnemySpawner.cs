using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)] float spawnInterval = 3f;    
    [SerializeField] EnemyMovement enemy;   //having the type as enemymovement makes it so that anything that doesnt have this script attached to cannot be referenced in the inspector
    [SerializeField] AudioClip spawnedEnemySFX;

    void Start() 
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)    //forever
        {
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);

            var attacker = Instantiate(enemy, transform.position, Quaternion.identity);

            var parent = GameObject.Find("Enemies");    //setting hierarchy parent
            attacker.transform.parent = parent.transform;   //setting hierarchy parent

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
