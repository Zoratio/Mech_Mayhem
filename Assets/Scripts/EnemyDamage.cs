using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    //*IGNORE THE COLLISIONMESH HERE*
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticlePrefab;
    [SerializeField] ParticleSystem deathParticlePrefab;

    Text score;

    private void Start()
    {
        score = GameObject.Find("Score Text").GetComponent<Text>();
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        hitPoints = hitPoints - 1;
        hitParticlePrefab.Play();   //using .Play is easier than turning the emission on then off again
    }
    private void KillEnemy()
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();

        var parent = GameObject.Find("Enemies");    //setting hierarchy parent
        vfx.transform.parent = parent.transform;    //setting hierarchy parent

        IncreaseScore();
        Destroy(gameObject);
    }

    private void IncreaseScore()
    {
        int num = int.Parse(score.text);
        num++;
        score.text = num.ToString();
    }
}
