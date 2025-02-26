using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFactory : MonoBehaviour
{
    public static PlantFactory Instance { get; private set; }


    [SerializeField] private List<PlantSO> plantSOList;
    Dictionary<string, GameObject> plantPrefabDict = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Initialize();
    }

    private void Initialize()
    {
        foreach (PlantSO plantSO in plantSOList)
        {
            if (plantPrefabDict.ContainsKey(plantSO.plantName) == false)
            {
                plantPrefabDict.Add(plantSO.plantName, plantSO.plantPrefab);
            }
        }
    }

    public IProduct GetProduct(string plantName, Vector2 position)
    {
        GameObject plantGameObject = Instantiate(plantPrefabDict[plantName]);
        plantGameObject.transform.position = position;
        Plant plant = plantGameObject.GetComponent<Plant>();
        plant.Initialize();
        return plant;
    }

    public IProduct GetProduct(string plantName, GridPosition gridPosition)
    {
        Vector2 worldPosition = GridManager.Instance.GetWorldPosition(gridPosition);
        return GetProduct(plantName, worldPosition);
    }
    

}
