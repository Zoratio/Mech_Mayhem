using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FollowPath());
        //*this is where the first FollowPath yield return will end up, then after 1 second itll iterate once again then yield back to wherever the codes execution queue was currently at and repeat the process again after the 1 second*
    }

    IEnumerator FollowPath()
    {
        print("Starting patrol...");
        foreach (Waypoint waypoint in path)
        {
            print("Visiting block: " + waypoint.name);
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);    //this will yield return back to just after the line 13 where it has started until 1 second where itll go back to executing this code again
        }
        print("Ending patrol");
    }
}
