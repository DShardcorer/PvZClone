using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties
{
    public int id;
    public int health;
    public int damage;

    public float speed;

    public float attackSpeed;

    public EnemyProperties(int id, EnemySO enemySO)
    {
        this.id = id;

        health = enemySO.health;
        damage = enemySO.damage;
        speed = enemySO.speed;
        attackSpeed = enemySO.attackSpeed;
    }


}
