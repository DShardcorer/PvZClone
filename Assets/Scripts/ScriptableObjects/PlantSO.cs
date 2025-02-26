using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "PlantSO", order = 1)]
public class PlantSO : ScriptableObject
{
    public string plantName;

    public int sunCost;
    public int health;
    public float actionCooldownTimer;

    public float performActionTimer;

    public int damage;
    public float projectileSpeed;

    public float projectileLifetime;

    public GameObject projectilePrefab;

    public GameObject plantPrefab;

    public GameObject plantPreviewPrefab;

}
