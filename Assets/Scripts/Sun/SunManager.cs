using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public static SunManager Instance;
    private int sunCount = 100;
    [SerializeField] private RectTransform sunCollectionPoint;

    public event EventHandler OnSunChanged;
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
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddSun(25);
        }
    }

    public void AddSun(int amount)
    {
        sunCount += amount;
        OnSunChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SpendSun(int amount)
    {
        sunCount -= amount;
        OnSunChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanAfford(int amount)
    {
        return sunCount >= amount;
    }
    public int GetSunCount()
    {
        return sunCount;
    }
    public Vector2 GetSunCollectionPointOnWorldSpace()
    {
        //Converts the sun collection point in UI position to world space
        return sunCollectionPoint.position;
    }
}
