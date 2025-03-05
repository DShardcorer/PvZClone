using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private MouseWorld mouseWorld;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private PlantManager plantManager;
    [SerializeField] private PlantPlacementManager plantPlacementManager;
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private ProjectileManager projectileManager;
    [SerializeField] private SunManager sunManager;
    [SerializeField] private StageManager stageManager;

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
        stageManager.Initialize(this);
        gameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stageManager.StartStage();
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
    public EnemyManager GetEnemyManager()
    {
        return enemyManager;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        StopAllCoroutines();

    }
}
