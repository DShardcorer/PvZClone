using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private LayerMask projectileLayer;
    [SerializeField] private ZombieSO zombieSO;
    private Rigidbody2D rb;

    private int health;

    private int damage;

    private float speed;

    private float attackSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = zombieSO.health;
        damage = zombieSO.damage;
        speed = zombieSO.speed;
        attackSpeed = zombieSO.attackSpeed;

    }


    private void Start()
    {
        rb.velocity = new Vector2(-speed, 0);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
