using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : DictFactory
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private List<EnemySO> enemySOList;
    [SerializeField] private List<ObjectPool> enemyPoolList;
    Dictionary<string, ObjectPool> enemyPoolDict = new Dictionary<string, ObjectPool>();
    Dictionary<string, EnemySO> enemySODict = new Dictionary<string, EnemySO>();

    private void Start()
    {
        Initialize();
        foreach (KeyValuePair<string, ObjectPool> item in enemyPoolDict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }

    }
    private void Initialize()
    {
        for (int i = 0; i < enemySOList.Count; i++)
        {
            if (enemyPoolDict.ContainsKey(enemySOList[i].enemyName) == false)
            {
                //Compare prefabs from the zombieSOList with the objectPool prefab
                for (int j = 0; j < enemyPoolList.Count; j++)
                {
                    if (enemySOList[i].enemyPrefab == enemyPoolList[j].GetPrefab())
                    {
                        enemyPoolDict.Add(enemySOList[i].enemyName, enemyPoolList[j]);
                    }
                }
            }
        }

        for (int i = 0; i < enemySOList.Count; i++)
        {
            if (enemySODict.ContainsKey(enemySOList[i].enemyName) == false)
            {
                enemySODict.Add(enemySOList[i].enemyName, enemySOList[i]);
            }
        }

    }

public override IProduct GetProduct(string enemyName, GridPosition gridPosition)
{
    if (!enemyPoolDict.TryGetValue(enemyName, out ObjectPool enemyPool))
    {
        Debug.LogError($"EnemyFactory: No enemy pool found for enemy: {enemyName}");
        return null;
    }
    

    GameObject enemyGameObject = enemyPool.GetObject();
    enemyGameObject.transform.position = GridManager.Instance.GetWorldPosition(gridPosition);
    EnemyView enemyView = enemyGameObject.GetComponent<EnemyView>();
    Enemy enemyController = new Enemy();

    EnemySO enemySO = enemySODict[enemyName];
    if (enemySO == null)
    {
        Debug.LogError($"EnemyFactory: EnemySO not found for enemy: {enemyName}");
        return null;
    }
    EnemyProperties enemyProperties = new EnemyProperties();

    enemyProperties.Initialize(enemySO);
    enemyController.Initialize(enemyManager, enemyProperties, enemyView);
    enemyView.Initialize();
    
    return enemyView;
}


}
