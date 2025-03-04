using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private StageManager stageManager;

    [SerializeField] private int enemiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxLanes = 5;

    private int currentWave = 1;
    private bool waveStarted = false;
    private Coroutine waveCoroutine;

    public void Initialize(StageManager manager)
    {
        stageManager = manager;
    }

    public void StartWaves()
    {
        if (!waveStarted)
        {
            waveCoroutine = StartCoroutine(SpawnWaves());
            waveStarted = true;
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            Debug.Log($"Enemy Wave {currentWave} starting!");

            for (int i = 0; i < enemiesPerWave; i++)
            {
                int randomLane = Random.Range(0, maxLanes);
                string enemyType = GetEnemyTypeForWave(currentWave);
                stageManager.SpawnEnemyAtLane(randomLane, enemyType);
                yield return new WaitForSeconds(spawnInterval);
            }

            Debug.Log($"Enemy Wave {currentWave} complete! Next wave in {timeBetweenWaves} seconds.");
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
            enemiesPerWave += 2;
        }
    }

    private string GetEnemyTypeForWave(int waveNumber)
    {
        if (waveNumber < 3)
            return Random.value < 0.5f ? NameHelper.ZombieBasic : NameHelper.ZombieCone;
        if (waveNumber < 6)
            return Random.value < 0.8f ? NameHelper.ZombieBasic : NameHelper.ZombieCone;
        return Random.value < 0.5f ? NameHelper.ZombieBasic : NameHelper.ZombieCone;
    }

    public void StopWaves()
    {
        if (waveCoroutine != null)
        {
            StopCoroutine(waveCoroutine);
            waveCoroutine = null;
        }
        waveStarted = false;
    }
}
