
using UnityEngine;

public class Peashooter : Plant, IShootable
{
    private PeashooterView _peashooterView;
    private PeashooterProperties _peashooterProperties;


    public Peashooter(PlantManager parent, PlantProperties properties, PlantView view) : base(parent, properties, view)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        _peashooterView = (PeashooterView)_view;
        _peashooterProperties = (PeashooterProperties)_properties;
    }

    protected override void PerformAction()
    {
        Shoot();
    }

    public void Shoot()
    {
        StageManager.Instance.GetProjectileManager().GetProduct(ProjectileNameConverter(_properties.PlantName), _peashooterView.ShootPoint.position);
    }
    private string ProjectileNameConverter(string plantName)
    {
        return "Projectile" + plantName;
    }

    protected override bool CanPerformAction()
    {
        RaycastHit2D hit = Physics2D.Raycast(_peashooterView.ShootPoint.position, Vector2.right, GridManager.Instance.GetHorizontalLength(), LayerHelper.Enemies);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }




}
