using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] ParticleSystem selfDestructParticlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();

        StartCoroutine(FollowPath(path));
        //*this is where the first FollowPath yield return will end up, then after 1 second itll iterate once again then yield back to wherever the codes execution queue was currently at and repeat the process again after the 1 second*
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        //print("Starting patrol...");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1.5f);    //this will yield return back to just after the line 13 where it has started until 1 second where itll go back to executing this code again
        }
        //print("Ending patrol");
        SelfDestruct();
    }

    public void SelfDestruct()
    {
        var vfx = Instantiate(selfDestructParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();

        var parent = GameObject.Find("Enemies");    //setting hierarchy parent
        vfx.transform.parent = parent.transform;    //setting hierarchy parent

        Destroy(gameObject);
    }
}
