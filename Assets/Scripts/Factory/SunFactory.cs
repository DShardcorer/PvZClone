using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFactory : Factory
{
    public static SunFactory Instance { get; private set; }

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

    [SerializeField] private ObjectPool sunPool;

    public override IProduct GetProduct(Vector2 position)
    {
        GameObject sunGameObject = sunPool.GetObject();
        sunGameObject.transform.position = position;
        Sun sun = sunGameObject.GetComponent<Sun>();
        sun.Initialize();
        return sunGameObject.GetComponent<IProduct>();
    }

}
