using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunflowerView : PlantView
{
    [SerializeField] private Transform sunSpawnPoint;

    public Transform SunSpawnPoint => sunSpawnPoint;
}
