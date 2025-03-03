using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyManager : DictFactory
{
    private StageManager _parent;
    [SerializeField] private List<EnemySO> enemySOList;
    private Dictionary<string, EnemySO> enemySODict = new Dictionary<string, EnemySO>();
    private List<Enemy> _enemies = new List<Enemy>();
    private long _currentId = 0;
    private PoolManager _poolManager;

    public void Initialize(StageManager parent)
    {
        _parent = parent;
        _poolManager = StageManager.Instance.GetPoolManager();
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
        _enemies.Add((GetProduct(enemyName, gridPosition) as EnemyView).GetParent());
        _currentId++;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        _poolManager.ReturnObject(enemy.GetProperties().EnemyName, enemy.GetView().gameObject);
        enemy.Dispose();
    }

    public void GameOver()
    {
        _parent.GameOver();
    }

    public override IProduct GetProduct(string enemyName, GridPosition gridPosition)
    {
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

        enemyGameObject.transform.position = GridManager.Instance.GetWorldPosition(gridPosition);
        EnemyView enemyView = enemyGameObject.GetComponent<EnemyView>();
        EnemyProperties enemyProperties = new EnemyProperties(_currentId, enemySO);
        Enemy enemy = new Enemy(this, enemyProperties, enemyView);
        enemyView.Initialize();

        return enemyView;
    }
}
