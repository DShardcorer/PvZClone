using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{
    [SerializeField] private Transform sunSpawnPoint;

    public Sunflower(PlantManager parent, PlantProperties properties, PlantView view) : base(parent, properties, view)
    {
    }

    protected override bool CanPerformAction()
    {
        return true;
    }

    protected override void PerformAction()
    {
        ProduceSun();
    }

    private void ProduceSun()
    {
        SunFactory.Instance.GetProduct(sunSpawnPoint.position);
    }
}
