using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : Plant, IShootable
{
    [SerializeField] private LayerMask zombieLayer;
    [SerializeField] private Transform shootPoint;







    protected override void PerformAction()
    {
        Shoot();
    }

    public void Shoot()
    {
        projectileFactory.GetProduct(shootPoint.position);
    }

    protected override bool CanPerformAction()
    {
        RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, Vector2.right, GridManager.Instance.GetHorizontalLength(), zombieLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }




}
