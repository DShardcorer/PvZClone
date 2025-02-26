using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition;
    private Plant plant;
    private List<Zombie> zombies;

    public GridObject(GridPosition gridPosition, Plant plant)
    {
        this.gridPosition = gridPosition;
        this.plant = plant;
        zombies = new List<Zombie>();
    }

    public GridPosition GridPosition
    {
        get { return gridPosition; }
    }

    public Plant GetPlant()
    {
        return plant;
    }

    public Zombie GetZombie()
    {
        if (zombies.Count > 0)
        {
            return zombies[0];
        }
        return null;
    }

    public void AddZombie(Zombie zombie)
    {
        zombies.Add(zombie);
    }

    public void RemoveZombie(Zombie zombie)
    {
        zombies.Remove(zombie);
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
