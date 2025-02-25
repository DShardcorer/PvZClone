using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaProjectilePool : MonoBehaviour
{
    
    [SerializeField] private GameObject peaProjectilePrefab;
    [SerializeField] private int poolSize = 5;

    private List<GameObject> peaProjectiles = new List<GameObject>();

    private void Awake()
    {
        FillPool();
    }

    private void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject peaProjectile = Instantiate(peaProjectilePrefab, transform);
            peaProjectile.SetActive(false);
            peaProjectiles.Add(peaProjectile);
        }
    }

    public GameObject GetPeaProjectile()
    {
        foreach (GameObject peaProjectile in peaProjectiles)
        {
            if (!peaProjectile.activeInHierarchy)
            {
                return peaProjectile;
            }
        }

        GameObject newPeaProjectile = Instantiate(peaProjectilePrefab, transform);
        peaProjectiles.Add(newPeaProjectile);
        return newPeaProjectile;
    }

    public void ReturnPeaProjectile(GameObject peaProjectile)
    {
        peaProjectile.SetActive(false);
    }

    public void ReturnAllPeaProjectiles()
    {
        foreach (GameObject peaProjectile in peaProjectiles)
        {
            peaProjectile.SetActive(false);
        }
    }


}
