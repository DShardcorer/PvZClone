
using UnityEngine;

public class Peashooter : Plant, IShootable
{
    private PeashooterView _peashooterView;
    private PeashooterProperties _peashooterProperties;
    private ProjectileManager _projectileManager;
    private GridManager _gridManager;


    public Peashooter(PlantManager parent, PlantProperties properties, PlantView view) : base(parent, properties, view)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        _peashooterView = (PeashooterView)_view;
        _peashooterProperties = (PeashooterProperties)_properties;
        _projectileManager = GameManager.Instance.GetProjectileManager();
        _gridManager = GameManager.Instance.GetGridManager();
    }

    protected override void PerformAction()
    {
        Shoot();
    }

    public void Shoot()
    {
        Debug.Log("Peashooter is shooting");
        _projectileManager.GetObject(ProjectileNameConverter(_properties.PlantName), _peashooterView.ShootPoint.position);
    }
    private string ProjectileNameConverter(string plantName)
    {
        return "Projectile" + plantName;
    }

    protected override bool CanPerformAction()
    {
        RaycastHit2D hit = Physics2D.Raycast(_peashooterView.ShootPoint.position, Vector2.right, _gridManager.GetHorizontalLength(), LayerHelper.Enemies);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }




}
