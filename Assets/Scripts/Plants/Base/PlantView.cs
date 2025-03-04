using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantView : MonoBehaviour, IView
{
    protected Plant _parent;


    public void Initialize(IController controller)
    {
        _parent = (Plant)controller;
        gameObject.SetActive(true);
    }
    protected void Update()
    {
        _parent.Update();
    }

    public void Dispose()
    {
        _parent = null;
        gameObject.SetActive(false);
    }
    public Plant GetParent()
    {
        return _parent;
    }

}
