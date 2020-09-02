using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // paramater of each tower
    [SerializeField] Transform objectToPan;
    float attackRange = 30f;
    [SerializeField] ParticleSystem ps;

    // state of each tower
    Transform targetObject;

    private void Update()
    {
        SetTargetEnemy();
        if (targetObject)
        {
            objectToPan.LookAt(targetObject);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
        if (sceneEnemies.Length == 0) { return; }

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (EnemyDamage testEnemy in sceneEnemies)
        {
            closestEnemy = GetClosest(closestEnemy, testEnemy.transform);
        }

        targetObject = closestEnemy;
    }

    private Transform GetClosest(Transform closestEnemy, Transform testEnemy)
    {
        if ((Vector3.Distance(transform.position, closestEnemy.position)) >= (Vector3.Distance(transform.position, testEnemy.position)))
        {
            return testEnemy;
        }
        return closestEnemy;
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(targetObject.transform.position, gameObject.transform.position); 
        if (distanceToEnemy <= attackRange)
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool isActive)
    {
        ParticleSystem.EmissionModule em = GetComponentInChildren<ParticleSystem>().emission;
        em.enabled = isActive;
    }
}
