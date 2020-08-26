using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //makes it so that this script will be running in editor mode
[SelectionBase] //makes it so that the parent object is much more likely to be selected in the scene when editing, not a child object
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;

    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();    //ensures that the link to the Waypoint reference is found ('RequireComponent' is at the top)
    }

    void Update()
    {
        SnapToGrid();   //makes sure the cubes move in sets of 10 pos
        UpdateLabel();  //updates the label and name of the cube to be equal to its current position
    }

    void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(   //snaps the cubes position to the correct transform
            waypoint.GetGridPos().x * gridSize, //(go to Waypoint script) - the '* gridSize' here is what is causing the snap
            0f, 
            waypoint.GetGridPos().y * gridSize //(go to Waypoint script) - the '* gridSize' here is what is causing the snap
        );
    }

    void UpdateLabel()
    {
        TextMesh txtMesh = GetComponentInChildren<TextMesh>();
        int gridSize = waypoint.GetGridSize();
        string labelText = 
            waypoint.GetGridPos().x + 
            "," + 
            waypoint.GetGridPos().y;
        txtMesh.text = labelText;
        gameObject.name = "Cube " + labelText;
    }
}
