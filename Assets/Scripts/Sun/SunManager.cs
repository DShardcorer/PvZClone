using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour, IFactory, IManager
{
    private StageManager _parent;

    private PoolManager _poolManager;

    private int sunCount = 100;
    [SerializeField] private RectTransform sunCollectionPoint;

    public void Initialize(StageManager parent)
    {
        _parent = parent;
        _poolManager = _parent.GetPoolManager();
    }

    public event EventHandler OnSunChanged;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    public IController GetObject(Vector2 position)
    {
        Sun sun = _poolManager.GetObject(NameHelper.Sun).GetComponent<Sun>();
        sun.transform.position = position;
        sun.Initialize();
        return sun;
    }

}
