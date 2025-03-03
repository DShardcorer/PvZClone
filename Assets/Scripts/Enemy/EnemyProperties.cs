

public class EnemyProperties
{
    // Private fields
    private long _id;
    private string _enemyName;
    private int _health;
    private int _damage;
    private float _speed;
    private float _attackSpeed;

  
    public long Id => _id; 
    public string EnemyName => _enemyName;
    public int Health { get => _health; set => _health = value; } // Allows modification
    public int Damage => _damage;
    public float Speed => _speed;
    public float AttackSpeed => _attackSpeed;

    public EnemyProperties(long id, EnemySO enemySO)
    {
        _id = id;
        _enemyName = enemySO.enemyName;
        _health = enemySO.health;
        _damage = enemySO.damage;
        _speed = enemySO.speed;
        _attackSpeed = enemySO.attackSpeed;
    }
}
