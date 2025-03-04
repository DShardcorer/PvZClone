using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    [SerializeField] private MouseWorld mouseWorld;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private PlantPlacementManager plantPlacementManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private SunManager sunManager;
    [SerializeField] private WaveManager waveManager;

    [SerializeField] private GameObject gameOverScreen;

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

    private void Start()
    {
        mouseWorld.Initialize(this);
        gridManager.Initialize(this);
        poolManager.Initialize(this);
        plantManager.Initialize(this);
        plantPlacementManager.Initialize(this);
        enemyManager.Initialize(this);
        projectileManager.Initialize(this);
        sunManager.Initialize(this);
        waveManager.Initialize(this);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            waveManager.StartWaves();
        }
    }
    public MouseWorld GetMouseWorld()
    {
        return mouseWorld;
    }
    public PoolManager GetPoolManager()
    {
        return poolManager;
    }

    public PlantManager GetPlantManager()
    {
        return plantManager;
    }
    public PlantPlacementManager GetPlantPlacementManager()
    {
        return plantPlacementManager;
    }

    public ProjectileManager GetProjectileManager()
    {
        return projectileManager;
    }

    public GridManager GetGridManager()
    {
        return gridManager;
    }
    public SunManager GetSunManager()
    {
        return sunManager;
    }

    // Called by WaveManager to spawn an enemy.
    public void SpawnEnemyAtLane(int lane, string enemyType)
    {
        enemyManager.SpawnEnemyAtLane(enemyType, lane);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        waveManager.StopAllCoroutines();

    }
}
