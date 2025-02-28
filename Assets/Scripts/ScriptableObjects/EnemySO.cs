using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int health;
    public int damage;
    public float speed;
    public float attackSpeed;
    public GameObject enemyPrefab;
    
}
