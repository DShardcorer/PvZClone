using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeashooterView : PlantView
{
    [SerializeField] private Transform _shootPoint;

    public Transform ShootPoint => _shootPoint;
}
