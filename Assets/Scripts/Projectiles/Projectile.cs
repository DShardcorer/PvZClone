using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProduct
{
    [SerializeField] protected LayerMask zombiesLayer;
    private Rigidbody2D rb;
    [SerializeField] protected PlantSO plantSO;
    protected ObjectPool pool;
    private float projectileSpeed;
    private int damage;

    private float lifeTimeTimer;

    protected virtual void Awake()
    {
        projectileSpeed = plantSO.projectileSpeed;
        damage = plantSO.damage;
        lifeTimeTimer = plantSO.projectileLifetime;
        rb = GetComponent<Rigidbody2D>();

        SetPool();
    }


    public void SetPool()
    {
        pool = GetComponentInParent<ObjectPool>();
    }


    protected void Update()
    {
        lifeTimeTimer -= Time.deltaTime;
        if (lifeTimeTimer <= 0)
        {
            pool.ReturnObject(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & zombiesLayer.value) != 0)
        {
            collision.gameObject.GetComponent<Zombie>().TakeDamage(damage);
            pool.ReturnObject(gameObject);
        }

    }
    private void ResetLifeTimeTimer()
    {
        lifeTimeTimer = plantSO.projectileLifetime;
    }

    public ObjectPool GetPool()
    {
        return pool;
    }

    public int GetDamage()
    {
        return damage;
    }

    public float GetSpeed()
    {
        return projectileSpeed;
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        rb.velocity = new Vector2(projectileSpeed, 0);
        ResetLifeTimeTimer();
    }
}
