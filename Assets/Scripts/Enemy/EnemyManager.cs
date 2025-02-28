using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    private StageManager _parent;
    [SerializeField] private EnemyFactory _enemyFactory;
    private List<Enemy> _enemies = new List<Enemy>();
    private long _currentid = 0;

    public void Initialize(StageManager parent)
    {   
        _parent = parent;
    }

    public void SpawnEnemyAtLane(string enemyName, int lane){
        GridPosition gridPosition = new GridPosition(8, lane);
        _currentid ++;
        _enemies.Add(new Enemy(this, _currentid, enemyName, gridPosition));
    }
    public Enemy GetEnemyById(long id){
        foreach (Enemy enemy in _enemies){
            if (enemy.GetId() == id){
                return enemy;
            }
        }
        return null;
    }

    public void UpdateHpEnemyById(long id, int hp){
        GetEnemyById(id).UpdateHp(hp);
    }

    public IProduct SpawnEnemy(string enemyName, GridPosition gridPosition){
        return _enemyFactory.GetProduct(enemyName, gridPosition);
    }

    public void RemoveEnemy(Enemy enemy){
        enemy.Dispese();
        _enemies.Remove(enemy);
        
        enemy.GetView().GetComponentInParent<ObjectPool>().ReturnObject(enemy.GetView().gameObject);
    }

    public void GameOver(){
        _parent.GameOver();
    }


}
