using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunflower : Plant
{
    [SerializeField] private Transform sunSpawnPoint;

    protected override void PerformAction()
    {
        ProduceSun();
    }

    private void ProduceSun()
    {
        SunFactory.Instance.GetProduct(sunSpawnPoint.position);
    }
}
