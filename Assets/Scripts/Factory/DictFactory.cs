using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DictFactory : MonoBehaviour
{
    public abstract IController GetEnemy(string key, GridPosition gridPosition);
}
