using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWayPoint, endWayPoint;   //these 2 cubes will have their own unique colours


    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>(); //dictionary of waypoints with a key & value

    Queue<Waypoint> queue = new Queue<Waypoint>();  //BFS queue

    bool isRunning = true; //if the end waypoint is currently being 'searched'

    Waypoint searchCenter;  //the current block being explored

    private List<Waypoint> path = new List<Waypoint>();  //this is the complete breadth first search that the enemies will follow

    Vector2Int[] directions =   //directions of where to search from each node
    {
        Vector2Int.up,      //= 0,1 coordinate
        Vector2Int.right,   //= 1,0 coordinate
        Vector2Int.down,    //etc
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count == 0)    //if the path hasnt been made yet, do so - recreating a new path from every enemymovement is breaking all the set values on the blocks etc
        {
            CalculatePath();
        }
        return path;    //*go to 'EnemyMovement' where it is used in the 'Start'*
    }

    private void CalculatePath()
    {
        LoadBlocks();   //creates the dictionary
        BreadthFirstSearch();   //finds the end waypoint and sets the 'exploredFrom' which is used in CreatePath
        CreatePath();   //using the exploredFrom variables, this will work it's way from end to start creating the 'path' list
    }

    private void CreatePath()
    {
        SetAsPath(endWayPoint);
        

        Waypoint previous = endWayPoint.exploredFrom;   //creates a reference to the block that found the endWayPoint
        while(previous != startWayPoint)    //while we haven't reached the start yet
        {
            SetAsPath(previous);                                                                                                            //!!!!!!!!!!!!THIS IS WHERE THE ISSUE WAS, THE 2 LINES OF CODE WERE SWITCHED ON THEIRS
            previous = previous.exploredFrom;   //now the new previous becomes the olds previous' previous
        }
        path.Add(startWayPoint);    //once the list start has been found, I need to add this outside of the while loop
        startWayPoint.isPlaceable = false;
        path.Reverse(); //this built-in function will reverse the list - before it began with the end value now it begins with the start value
    }

    private void SetAsPath(Waypoint waypoint)
    {
        path.Add(waypoint);  //add the end to the list first
        waypoint.isPlaceable = false;
    }

    private void BreadthFirstSearch()
    {
        queue.Enqueue(startWayPoint);   //add start block to the queue

        while (queue.Count > 0 && isRunning)    //while there are still blocks to be searched && the first block wasnt the end too
        {
            searchCenter = queue.Dequeue(); //take the block of the front of the queue and save its value
            HaltIfEndFound();   //*to method*
            ExploreNeighbours();    //*to method*
            searchCenter.isExplored = true; //this is set for in the Waypoint script
        }
    }   //*Script task complete*


    private void HaltIfEndFound()
    {
        if (searchCenter == endWayPoint)    //if the current block that is being explored is the end waypoint
        {
            isRunning = false;  //this will for the pathfind loop to stop iterating
        }
    }   //*back to 'BreadthFirstSearch'*


    private void ExploreNeighbours()
    {
        if (!isRunning) { return; } //this is needed as this runs in the same iteration as when the isRunning may be false (meaning it's been found)
        foreach(Vector2Int direction in directions) //go through all 4 directions (right, left, up, down)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction; //as the type is Vector2Int, it knows how to correctly add these 2 axis coordinates together without getting confused 
            if (grid.ContainsKey(neighbourCoordinates))   //without the 'if', this would return an key error for coordinate that dont exist (edge block) so putting it in 'if' will stop the problem
            {
                QueueNewNeighbours(neighbourCoordinates);   //*to method*
            }
        }
    }   //*back to 'BreadthFirstSearch'*


    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)    //this will be called up to 4 times per block (wont be called all 4 times if it is an edge block)
    {
        Waypoint neighbour = grid[neighbourCoordinates];    //dictionary key being the search position and its the direction to give the key of the neighbours coordinates
        if (neighbour.isExplored || queue.Contains(neighbour))  //if queue.Contains wasnt checked, blocks would be added to the queue multiple times before being searched 
        {
            //do nothing - I could just put the else in here and make the if use the '!'
        }
        else
        {
            //neighbour.SetTopColour(Color.blue); //*if i want to see what blocks have been queued*
            queue.Enqueue(neighbour);   //add to queue
            neighbour.exploredFrom = searchCenter;
        }
    }   //*back to 'ExploreNeighbours'*


    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();  //array of all the Waypoint scripts that are on every block
        foreach(Waypoint waypoint in waypoints) //going through all the waypoint scripts on each block
        {
            var gridPos = waypoint.GetGridPos();    //Vector2Int               
            if (grid.ContainsKey(gridPos))  //checks if this key (position) already exists - ie a overlap in position so no point counting it as it won't matter
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);  //adding the cube to the dictionary with the key (index) being it's position as it'll be unique without overlapping another cube/block and the script being the value
            }
        }
    }   //*back to 'Start'*
}
