using System;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour, IDictFactory, IManager
{
    private GameManager _parent;

    private GridManager _gridManager;
    [SerializeField] private List<PlantSO> _plantSOList;
    private Dictionary<string, PlantSO> _plantSODict = new Dictionary<string, PlantSO>();
    private List<Plant> _plants = new List<Plant>();
    private long _currentId = 0;
    private PoolManager _poolManager;
    private Dictionary<string, (Type plantType, Type propertiesType)> _plantTypeMapping = new Dictionary<string, (Type, Type)>();

    public void Initialize(GameManager manager)
    {
        _parent = manager;
        _gridManager = GameManager.Instance.GetGridManager();
        _poolManager = GameManager.Instance.GetPoolManager();
        PopulateDictionary();
        PopulatePlantTypeMapping();
    }

    private void PopulatePlantTypeMapping()
    {
        RegisterPlantType<Peashooter, PeashooterProperties>(NameHelper.Peashooter);
        RegisterPlantType<Sunflower, SunflowerProperties>(NameHelper.Sunflower);
    }

    private void PopulateDictionary()
    {
        foreach (var plantSO in _plantSOList)
        {
            if (!_plantSODict.ContainsKey(plantSO.plantName))
            {
                _plantSODict.Add(plantSO.plantName, plantSO);
            }
        }
    }



    public void RegisterPlantType<TPlant, TProperties>(string key)
        where TPlant : Plant
        where TProperties : PlantProperties
    {
        _plantTypeMapping[key] = (typeof(TPlant), typeof(TProperties));
    }

    public void ReturnObject(IController controller)
    {
        Plant plant = (Plant)controller;
        _plants.Remove(plant);
        _poolManager.ReturnObject(plant.GetProperties().PlantName, plant.GetView().gameObject);
        plant.Dispose();
    }

    public IController GetObject(string key, Vector2 position)
    {

        GridPosition gridPosition = _gridManager.GetGridPosition(position);
        if (!_plantSODict.TryGetValue(key, out PlantSO plantSO))
        {
            Debug.LogError($"PlantManager: No PlantSO found for key '{key}'.");
            return null;
        }

        GameObject plantGameObject = _poolManager.GetObject(key);
        if (plantGameObject == null)
        {
            Debug.LogError($"PlantManager: No pooled object found for key '{key}'.");
            return null;
        }

        plantGameObject.transform.position = _gridManager.GetWorldPosition(gridPosition);
        PlantView plantView = plantGameObject.GetComponent<PlantView>();
        if (plantView == null)
        {
            Debug.LogError($"PlantManager: No PlantView component found on pooled object for key '{key}'.");
            return null;
        }

        // Look up the corresponding types for this plant key.
        (Type plantType, Type propertiesType) tuple;
        if (!_plantTypeMapping.TryGetValue(key, out tuple))
        {
            Debug.LogWarning($"PlantManager: No registered mapping for key '{key}'");
        }
        PlantProperties properties = (PlantProperties)Activator.CreateInstance(tuple.propertiesType, _currentId, plantSO);
        Plant plant = (Plant)Activator.CreateInstance(tuple.plantType, this, properties, plantView);

        plant.Initialize();
        _plants.Add(plant);
        _currentId++;

        return plant;
    }
}
