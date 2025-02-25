using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    private GridSystem gridSystem;


    private void Awake()
    {
        gridSystem = new GridSystem(9, 5, 1.6f, 2, new Vector2((float)-8.9, (float)-5.5));
        
        gridSystem.DrawDebugGrid();
    }

}
