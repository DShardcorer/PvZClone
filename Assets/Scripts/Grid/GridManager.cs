using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour, IManager
{
    private GridSystem gridSystem;

    private GameManager _parent;
    [SerializeField] private float cellSizeH = 1.6f;
    [SerializeField] private float cellSizeV = 2;
    [SerializeField] private int horizontalLength = 9;
    [SerializeField] private int verticalLength = 5;
    [SerializeField] private Vector2 originPosition = new Vector2((float)-8.1, (float)-4.5);

    public void Initialize(GameManager manager)
    {
        _parent = manager;
        gridSystem = new GridSystem(horizontalLength, verticalLength, cellSizeH, cellSizeV, originPosition);
        gridSystem.DrawDebugGrid();
    }


    public float GetHorizontalLength()
    {
        return horizontalLength * cellSizeH;
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
    public Vector2 GetWorldPositionFromClosestGridPosition(Vector2 worldPosition)
    {
        return gridSystem.GetWorldPositionFromClosestGridPosition(worldPosition);
    }

    public Vector2 GetLaneEndWorldPosition(int lane)
    {
        return gridSystem.GetLaneEndWorldPosition(lane);
    }
}