using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GridSystem gridSystem;

    public static GridManager Instance { get; private set; }




    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gridSystem = new GridSystem(9, 5, 1.6f, 2, new Vector2((float)-8.1, (float)-4.5));
        
        gridSystem.DrawDebugGrid();
    }

    public Vector2 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }
    public GridPosition GetGridPosition(Vector2 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public bool IsWithinBounds(GridPosition gridPosition)
    {
        return gridSystem.IsWithinBounds(gridPosition);
    }

    public bool IsCellOccupiedByPlant(GridPosition gridPosition)
    {
        return gridSystem.IsCellOccupiedByPlant(gridPosition);
    }

    public void SetPlantAtGridPosition(GridPosition gridPosition, Plant plant)
    {
        gridSystem.GetGridObject(gridPosition).SetPlant(plant);
    }
    public void RemovePlantAtGridPosition(GridPosition gridPosition)
    {
        gridSystem.GetGridObject(gridPosition).SetPlant(null);
    }
    


}
