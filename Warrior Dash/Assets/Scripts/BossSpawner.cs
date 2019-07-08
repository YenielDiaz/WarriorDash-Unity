using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;
    [SerializeField] bool looping = false;

    [SerializeField] Level level;

    //We make Start method into a coroutine
    //And we continually spawn every wave while the looping atrribute is true with the do-while loop
    IEnumerator Start()
    {
        level = FindObjectOfType<Level>();
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    //coroutine to spawn every enemy in a given wave
    IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumOfEnemies(); i++)
        {
            var newEnemy = Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity
            );
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }

    //spawns all waves
    //this is an example of a coroutine whose yield condition is for another coroutine to finish running
    //in other words we are running a coroutine within another coroutine
    private IEnumerator SpawnAllWaves()
    {
        for (int i = 0; i < waveConfigs.Count; i++)
        {
            var currentWave = waveConfigs[i];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    //method we call to go to next level
    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(3);
        level.LoadNextLevel();
    }
}

