using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{

    public static StageManager Instance { get; private set; }


    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private SunManager sunManager;



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
        poolManager.Initialize(this);
        plantManager.Initialize(this);
        enemyManager.Initialize(this);
        projectileManager.Initialize(this);




        gameOverScreen.SetActive(false);
    }

    public PoolManager GetPoolManager()
    {
        return poolManager;
    }
    public ProjectileManager GetProjectileManager()
    {
        return projectileManager;
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
        if (waveNumber < 3) return Random.value < 0.5f ? NameHelper.ZombieBasic : NameHelper.ZombieCone;
        if (waveNumber < 6) return Random.value < 0.8f ? NameHelper.ZombieBasic : NameHelper.ZombieCone;
        return Random.value < 0.5f ? NameHelper.ZombieBasic : NameHelper.ZombieCone;
    }

    public void SpawnZombieAtLane(int lane, string zombieType)
    {
        enemyManager.SpawnEnemyAtLane(zombieType, lane);
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
