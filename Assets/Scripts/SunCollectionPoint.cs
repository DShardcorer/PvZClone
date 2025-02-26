using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCollectionPoint : MonoBehaviour
{
    public static SunCollectionPoint Instance { get; private set; }

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
}
