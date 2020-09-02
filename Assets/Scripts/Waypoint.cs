using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public bool isExplored = false;
    public Waypoint exploredFrom;

    [SerializeField] Tower towerPrefab; //having type of Tower protect the game against assigning something that doesn't have the tower script (only the tower prefab has this)

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
            if (isPlaceable && !towerPlaced)    //if its a valid location && and there isn't a tower in that location already
            {
                print(gameObject.name + " tower placement");
                PlaceTower();   //*to method*
            }
            else
            {
                print("Can't place tower here");
            }
        }
    }

    private void PlaceTower()
    {
        Instantiate(towerPrefab, transform.position, Quaternion.identity);  //instantiate a towerprefab in the waypoint object location
        towerPlaced = true; //stops it from being placed again while occupied
    }
}
