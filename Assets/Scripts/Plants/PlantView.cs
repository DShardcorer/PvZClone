using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantView : MonoBehaviour, IView
{
    protected Plant _parent;


    public void Initialize(IController controller)
    {
        _parent = (Plant)controller;
    }

    public void Dispose()
    {
        _parent = null;
    }

}
