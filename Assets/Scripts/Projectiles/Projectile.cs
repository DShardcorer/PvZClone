using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : IController
{
    private ProjectileManager _parent;

    private ProjectileProperties _properties;

    private ProjectileView _view;



    public Projectile(ProjectileManager parent, ProjectileProperties properties, ProjectileView view)
    {
        _parent = parent;
        _properties = properties;
        _view = view;
        
    }
    public void Initialize()
    {
        _view.Initialize(this);
    }

    public void Dispose()
    {
        //return to pool
        
        _parent = null;
        _properties = null;
        _view = null;
    }

    public ProjectileView GetView()
    {
        return _view;
    }

    public ProjectileProperties GetProperties()
    {
        return _properties;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & LayerHelper.Enemies) != 0)
        {
            collision.gameObject.GetComponent<EnemyView>().TakeDamage(_properties.Damage);
            _parent.ReturnObject(this);
        }
    }
    public void ReturnObject()
    {
        _parent.ReturnObject(this);
    }


}
