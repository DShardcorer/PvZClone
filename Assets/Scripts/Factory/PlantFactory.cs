using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFactory : DictFactory
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



    public override IProduct GetProduct(string plantName, GridPosition gridPosition)
    {
        Vector2 worldPosition = GridManager.Instance.GetWorldPosition(gridPosition);
        GameObject plantGameObject = Instantiate(plantPrefabDict[plantName], worldPosition, Quaternion.identity);
        Plant plant = plantGameObject.GetComponent<Plant>();
        plant.Initialize();
        return plantGameObject.GetComponent<IProduct>();

    }


    

}
