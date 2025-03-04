using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileView : MonoBehaviour, IView
{
    private Rigidbody2D _rb;


    private Projectile _parent;

    private float lifeTimeTimer;


    public void Initialize(IController controller)
    {
        _parent = (Projectile)controller;
        _rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(true);
        _rb.velocity = new Vector2(_parent.GetProperties().ProjectileSpeed, 0);
        ResetLifeTimeTimer();
        
    }

    protected void Update()
    {
        lifeTimeTimer -= Time.deltaTime;
        if (lifeTimeTimer <= 0)
        {
            _parent.ReturnObject();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _parent.OnCollisionEnter2D(collision);

    }
    private void ResetLifeTimeTimer()
    {
        lifeTimeTimer = _parent.GetProperties().ProjectileLifetime;
    }



    public void Dispose()
    {
        _parent = null;
    }
}
