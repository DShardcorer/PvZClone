using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int horizontalLength;
    private int verticalLength;
    private float cellSizeH;
    private float cellSizeV;
    private Vector2 originPosition;

    private GridObject[,] gridArray;

    public GridSystem(int horizontalLength, int verticalLength, float cellSizeH, float cellSizeV, Vector2 originPosition)
    {
        this.horizontalLength = horizontalLength;
        this.verticalLength = verticalLength;
        this.cellSizeH = cellSizeH;
        this.cellSizeV = cellSizeV;
        this.originPosition = originPosition;

        gridArray = new GridObject[horizontalLength, verticalLength];

        for (int x = 0; x < horizontalLength; x++)
        {
            for (int y = 0; y < verticalLength; y++)
            {
                gridArray[x, y] = new GridObject(new GridPosition(x, y), null);
            }
        }
    }

    public Vector2 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector2(gridPosition.x * cellSizeH + originPosition.x, gridPosition.y * cellSizeV + originPosition.y);
    }

    public GridPosition GetGridPosition(Vector2 worldPosition)
    {
        return new GridPosition(Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSizeH), Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSizeV));
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridArray[gridPosition.x, gridPosition.y];
    }


    public void DrawDebugGrid(){
        for (int x = 0; x < horizontalLength; x++)
        {
            for (int y = 0; y < verticalLength; y++)
            {
                Debug.DrawLine(new Vector2(x * cellSizeH + originPosition.x, y * cellSizeV + originPosition.y), new Vector2((x + 1) * cellSizeH + originPosition.x, y * cellSizeV + originPosition.y), Color.white, 100f, false);
                Debug.DrawLine(new Vector2(x * cellSizeH + originPosition.x, y * cellSizeV + originPosition.y), new Vector2(x * cellSizeH + originPosition.x, (y + 1) * cellSizeV + originPosition.y), Color.white, 100f, false);
            }
        }
        Debug.DrawLine(new Vector2(0, verticalLength * cellSizeV + originPosition.y), new Vector2(horizontalLength * cellSizeH + originPosition.x, verticalLength * cellSizeV + originPosition.y), Color.white, 100f, false);
        Debug.DrawLine(new Vector2(horizontalLength * cellSizeH + originPosition.x, 0), new Vector2(horizontalLength * cellSizeH + originPosition.x, verticalLength * cellSizeV + originPosition.y), Color.white, 100f, false);
    }


}
