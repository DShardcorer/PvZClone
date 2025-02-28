using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties: ScriptableObject
{
    public int health;

    public int damage;

    public float speed;

    public float attackSpeed;

    public void Initialize(EnemySO enemySO)
    {
        health = enemySO.health;
        damage = enemySO.damage;
        speed = enemySO.speed;
        attackSpeed = enemySO.attackSpeed;
    }


}
