using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip finishEnemySFX;



    private void Start()
    {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.4f);  //make this the same as the enemymovement speed
        health--;
        healthText.text = health.ToString();
        GetComponent<AudioSource>().PlayOneShot(finishEnemySFX);
    }
}
