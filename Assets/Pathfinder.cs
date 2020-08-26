using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] Waypoint startWayPoint, endWayPoint;   //these 2 cubes will have their own unique colours

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColourStartEnd();
    }

    private void ColourStartEnd()
    {
        startWayPoint.SetTopColour(Color.green);
        endWayPoint.SetTopColour(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();               
            if (grid.ContainsKey(gridPos))  //checks if this key (position) already exists - ie a overlap in position so no point counting it as it won't matter
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);  //adding the cube to the dictionary with the key (index) being it's position as it'll be unique without overlapping another cube/block
            }
        }
    }
}
