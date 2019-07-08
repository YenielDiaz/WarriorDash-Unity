using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//line below makes sure the option to create this object appears in the unity menu when we click new
[CreateAssetMenu(menuName = "Enemy Wave Config")] 
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    /*
     * 
     * We make this scriptable object in order to easily choose how different waves will work
     * */

     //gets the enemy prefab that is being used for current wave
    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    //gets all the wavepoints of the path being used in the form of a list
    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform waypoint in pathPrefab.transform)
        {
            waveWaypoints.Add(waypoint);
        }
        return waveWaypoints;
    }
    //gets the time that has to pass before another enemy is spawned
    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    //gets the random factor which keeps time between spawns slightly different
    public float GetSpawnRandFactor() { return spawnRandomFactor; }

    //gets the number of enemies that will be spawned
    public int GetNumOfEnemies() { return numberOfEnemies; }

    //gets the speed at which the enemies will move
    public float GetMoveSpeed() { return moveSpeed; }


}
