using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant, IShootable
{
    [SerializeField] private GameObject peaPrefab;
    [SerializeField] private Transform shootPoint;

    private ProjectileFactory projectileFactory;
    [SerializeField] private PlantSO shootablePlantSO;


    protected override void Awake()
    {
        health = shootablePlantSO.health;
        actionCooldownTimer = shootablePlantSO.actionCooldownTimer;
        performingActionTimer = shootablePlantSO.performActionTimer;
        
        base.Awake();
    }
    void Start()
    {
        projectileFactory = ProjectileFactory.Instance;
    }


    protected override void PerformAction()
    {
        Shoot();
    }

    public void Shoot()
    {
        projectileFactory.GetProduct(shootPoint.position);
    }

}
