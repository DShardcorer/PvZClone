using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IDictFactory, IManager
{
    private GameManager _parent;
    private GridManager _gridManager;
    [SerializeField] private List<EnemySO> enemySOList;
    private Dictionary<string, EnemySO> enemySODict = new Dictionary<string, EnemySO>();
    private List<Enemy> _enemies = new List<Enemy>();
    private long _currentId = 0;
    private PoolManager _poolManager;

    public void Initialize(GameManager parent)
    {
        _parent = parent;
        _poolManager = GameManager.Instance.GetPoolManager();
        _gridManager = GameManager.Instance.GetGridManager();
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        foreach (var enemySO in enemySOList)
        {
            if (!enemySODict.ContainsKey(enemySO.enemyName))
            {
                enemySODict.Add(enemySO.enemyName, enemySO);
            }
        }
    }

    public Enemy GetEnemyById(long id)
    {
        return _enemies.Find(enemy => enemy.GetId() == id);
    }

    public void UpdateHpEnemyById(long id, int hp)
    {
        var enemy = GetEnemyById(id);
        if (enemy != null)
        {
            enemy.UpdateHp(hp);
        }
    }

    public void SpawnEnemyAtLane(string enemyName, int lane)
    {
        GridPosition gridPosition = new GridPosition(8, lane);
        Vector2 worldPosition = _gridManager.GetWorldPosition(gridPosition);
        Enemy enemy = (Enemy)GetObject(enemyName, worldPosition);
        //print enemy info
        Debug.Log($"Enemy spawned: {enemy.GetProperties().EnemyName} at lane {lane} with id {enemy.GetId()}");
    }

    public void ReturnObject(IController controller)
    {
        Enemy enemy = (Enemy)controller;
        _enemies.Remove(enemy);
        _poolManager.ReturnObject(enemy.GetProperties().EnemyName, enemy.GetView().gameObject);
        enemy.Dispose();
    }

    public void GameOver()
    {
        _parent.GameOver();
    }

    public IController GetObject(string enemyName, Vector2 position)
    {
        GridPosition gridPosition = _gridManager.GetGridPosition(position);
        if (!enemySODict.TryGetValue(enemyName, out EnemySO enemySO))
        {
            Debug.LogError($"EnemyManager: No EnemySO found for {enemyName}");
            return null;
        }

        // Get enemy object from PoolManager


        GameObject enemyGameObject = _poolManager.GetObject(enemyName);
        if (enemyGameObject == null)
        {
            Debug.LogError($"EnemyManager: No pooled object found for {enemyName}");
            return null;
        }

        enemyGameObject.transform.position = _gridManager.GetWorldPosition(gridPosition);
        EnemyView enemyView = enemyGameObject.GetComponent<EnemyView>();
        EnemyProperties enemyProperties = new EnemyProperties(_currentId, enemySO);
        Enemy enemy = new Enemy(this, enemyProperties, enemyView);
        enemy.Initialize();
        _enemies.Add(enemy);
        _currentId++;
        return enemy;
    }
}
