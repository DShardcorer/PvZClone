using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant, IShootable
{
    [SerializeField] private Transform shootPoint;

    private ProjectileFactory projectileFactory;



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
