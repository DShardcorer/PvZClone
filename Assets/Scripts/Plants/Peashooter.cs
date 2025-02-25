using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant, IShootable
{
    [SerializeField] private GameObject peaPrefab;
    [SerializeField] private Transform shootPoint;

    [SerializeField] private PeaProjectilePool peaProjectilePool;

    protected override void Awake()
    {
        health = 100;
        actionCooldownTimer = 1f;
        performingActionTimer = 1f;
        base.Awake();
    }


    protected override void PerformAction()
    {
        Shoot();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Shoot()
    {
        GameObject peaProjectile = peaProjectilePool.GetPeaProjectile();
        peaProjectile.transform.position = shootPoint.position;
        peaProjectile.SetActive(true);
    }

}
