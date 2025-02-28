using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    private StageManager _parent;
    [SerializeField] private EnemyFactory _enemyFactory;
    

    public void Initialize(StageManager parent)
    {   
        _parent = parent;
    }

    public void SpawnEnemyAtLane(string enemyName, int lane){
        GridPosition gridPosition = new GridPosition(8, lane);
        SpawnEnemy(enemyName, gridPosition);
    }

    public IProduct SpawnEnemy(string enemyName, GridPosition gridPosition){
        return _enemyFactory.GetProduct(enemyName, gridPosition);
    }

    public void RemoveEnemy(Enemy enemy){
        enemy.GetView().GetComponentInParent<ObjectPool>().ReturnObject(enemy.GetView().gameObject);
    }

    public void GameOver(){
        _parent.GameOver();
    }


}
