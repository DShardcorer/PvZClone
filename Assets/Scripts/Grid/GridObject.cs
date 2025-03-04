using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private Plant plant;
    private List<Enemy> enemies;

    public GridObject(GridPosition gridPosition, Plant plant)
    {
        this.gridPosition = gridPosition;
        this.plant = plant;
        enemies = new List<Enemy>();
    }

    public GridPosition GridPosition
    {
        get { return gridPosition; }
    }

    public Plant GetPlant()
    {
        return plant;
    }

    public Enemy GetZombie()
    {
        if (enemies.Count > 0)
        {
            return enemies[0];
        }
        return null;
    }

    public void AddEnemy(Enemy zombie)
    {
        enemies.Add(zombie);
    }

    public void RemoveEnemy(Enemy zombie)
    {
        enemies.Remove(zombie);
    }

    public void SetPlant(Plant plant)
    {
        this.plant = plant;
    }

    public override string ToString()
    {
        return gridPosition.ToString();
    }




}
