using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPathing : EnemyPathing
{

    //method to move our enemy through the path by making go towards each waypoint in order
    protected override void MoveEnemy()
    {
        //make sure we don't go out of bounds of the list
        if (waypointIndex < waypoints.Count)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            //we multiply by Time.deltaTime to be frame rate independent
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            //move toward is a useful method to move towards another object or position in a straight line
            transform.position = Vector2.MoveTowards(transform.position, targetPos,
                movementThisFrame);
            //once we reach target position, we increase the waypoint index so that we move towards the next waypoint
            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            //once we reach the last waypoint, we move towards a random waypoint in the path
            waypointIndex = Random.Range(1, waypoints.Count - 1);
            
        }
    }

}

