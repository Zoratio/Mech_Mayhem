using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public Waypoint exploredFrom;


    public bool isPlaceable = true;
    public bool towerPlaced = false;

    Vector2Int gridPos;

    const int gridSize = 10;    //this is constant, so the value in which the cubes can move cant be changed anywhere

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(  //rounds the position to the closest 10th
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)    //technically this value is classified as the y value but we can just grab and edit the z instead with this y value holder
        );
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))    //left click
        {
            if (isPlaceable)// && !towerPlaced)    //if its a valid location && and there isn't a tower in that location already
            {
                FindObjectOfType<TowerFactory>().AddTower(this);    //towerfactory script
            }
            else
            {
                print("Can't place tower here");
            }
        }
    }
}
