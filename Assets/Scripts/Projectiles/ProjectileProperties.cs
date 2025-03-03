
public class ProjectileProperties
{
    private long _id;
    private string _projectileName;
    private float _projectileSpeed;
    private int _damage;
    private float _projectileLifetime;


    public ProjectileProperties(long id, PlantSO plantSO)
    {
        _id = id;
        _projectileName = "Projectile" + plantSO.plantName;
        _projectileSpeed = plantSO.projectileSpeed;
        _damage = plantSO.damage;
        _projectileLifetime = plantSO.projectileLifetime;
    }
    public string ProjectileName => _projectileName;
    public long Id => _id;
    public float ProjectileSpeed => _projectileSpeed;
    public int Damage => _damage;
    public float ProjectileLifetime => _projectileLifetime;




}
