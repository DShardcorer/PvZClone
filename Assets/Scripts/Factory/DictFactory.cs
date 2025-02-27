using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DictFactory : MonoBehaviour
{
    public abstract IProduct GetProduct(string key, GridPosition gridPosition);
}
