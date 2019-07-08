using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //cached references
    protected WaveConfig waveConfig;
    protected List<Transform> waypoints;
    // [SerializeField] float moveSpeed = 2f;

    //config parameters
    protected int waypointIndex = 0;

    void Start()
    {
        //make sure we start at first waypoint in the path
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        MoveEnemy();
    }

    //setter to change the wave config of this enemy
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    //method to move our enemy through the path by making go towards each waypoint in order
    protected virtual void MoveEnemy()
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
            //once we reach the last waypoint, we delete the object
            Destroy(gameObject);
        }
    }

}
