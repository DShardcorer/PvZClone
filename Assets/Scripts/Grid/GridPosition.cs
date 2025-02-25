using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition: IEquatable<GridPosition>
{
    public int x;
    public int y;

    public GridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public bool Equals(GridPosition other)
    {
        return x == other.x && y == other.y;
    }

    
}
