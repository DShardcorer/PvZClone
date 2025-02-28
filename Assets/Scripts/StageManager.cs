using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private EnemyFactory enemyFactory;
    public static StageManager Instance { get; private set; }
    [SerializeField] private int zombiesPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private int maxLanes = 5;

    [SerializeField] private GameObject gameOverScreen;
    private int currentWave = 1;
    private bool waveStarted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // public Bullet GetBullet(){
    //     return BulletManager.ReturnBullet();
    // }

    private void Start()
    {
        enemyManager.Initialize(this);

        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!waveStarted)
            {
                StartCoroutine(SpawnWaves());
                waveStarted = true;
            }
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            Debug.Log($"Wave {currentWave} starting!");

            for (int i = 0; i < zombiesPerWave; i++)
            {
                int randomLane = Random.Range(0, maxLanes);
                string zombieType = GetZombieTypeForWave(currentWave);
                SpawnZombieAtLane(randomLane, zombieType);
                yield return new WaitForSeconds(spawnInterval);
            }

            Debug.Log($"Wave {currentWave} complete! Next wave in {timeBetweenWaves} seconds.");
            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;
            zombiesPerWave += 2;
        }
    }

    private string GetZombieTypeForWave(int waveNumber)
    {
        if (waveNumber < 3) return Random.value < 0.5f ? ZombieNames.ZombieBasic : ZombieNames.ZombieCone;
        if (waveNumber < 6) return Random.value < 0.8f ? ZombieNames.ZombieBasic : ZombieNames.ZombieCone;
        return Random.value < 0.5f ? ZombieNames.ZombieBasic : ZombieNames.ZombieCone;
    }

    public void SpawnZombieAtLane(int lane, string zombieType)
    {
        ZombieFactory.Instance.GetProduct(zombieType, new GridPosition(8, lane));
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        StopAllCoroutines();
        foreach (Zombie zombie in FindObjectsOfType<Zombie>())
        {
            zombie.Die();
        }

    }
}
