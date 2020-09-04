using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab; //having type of Tower protect the game against assigning something that doesn't have the tower script (only the tower prefab has this)

    //create an empty queue of towers
    Queue<Tower> queueOfTowers = new Queue<Tower>();

    public void AddTower(Waypoint baseWayPoint)
    {
        int towerCount = queueOfTowers.Count;

        if (towerCount < towerLimit)    //if there isnt a max amount of towers yet in the game
        {
            InstantiateNewTower(baseWayPoint);
        }
        else
        {
            MoveExistingTower(baseWayPoint);
        }
    }

    private void InstantiateNewTower(Waypoint newBaseWayPoint)
    {
        var newTower = Instantiate(towerPrefab, newBaseWayPoint.transform.position, Quaternion.identity);  //instantiate a towerprefab in the waypoint object location

        var parent = GameObject.Find("Towers"); //setting hierarchy parent
        newTower.transform.parent = parent.transform;   //setting hierarchy parent

        newBaseWayPoint.isPlaceable = false;   //this block now has a tower on it

        newTower.baseWaypoint = newBaseWayPoint;    //the tower is placed on this block - reference

        queueOfTowers.Enqueue(newTower);    //add this tower to the queue
    }

    private void MoveExistingTower(Waypoint newBaseWayPoint)
    {
        var oldTower = queueOfTowers.Dequeue(); //take the tower off the queue

        oldTower.baseWaypoint.isPlaceable = true;   //the original block it was on is now free again
        newBaseWayPoint.isPlaceable = false;   //the new block it is going to be placed on in a moment is now being set to not being free

        oldTower.baseWaypoint = newBaseWayPoint;    //it's location reference is now equal to the new location

        oldTower.transform.position = newBaseWayPoint.transform.position;   //it's actual location is now equal to it's reference

        queueOfTowers.Enqueue(oldTower);    //added back to the queue again
    }
}
