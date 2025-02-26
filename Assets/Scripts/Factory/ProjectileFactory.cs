using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    public static ProjectileFactory Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private ProjectilePool projectilePool;
    public IProduct GetProduct(Vector2 position)
    {
        GameObject projectileGameObject = projectilePool.GetObject();
        projectileGameObject.transform.position = position;
        Projectile projectile = projectileGameObject.GetComponent<Projectile>();
        projectile.Initialize();
        return projectileGameObject.GetComponent<IProduct>();

    }

    // Start is called before the first frame update
}
