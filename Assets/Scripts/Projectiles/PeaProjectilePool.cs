using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaProjectilePool : ProjectilePool
{
    public static PeaProjectilePool instance;
    [SerializeField] private PlantSO peaPlantSO;

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
