using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "Zombie")]
public class ZombieSO : ScriptableObject
{
    public string zombieName;
    public int health;
    public int damage;
    public float speed;
    public float attackSpeed;
    
}
