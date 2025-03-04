public class PlantProperties
{
    protected long _id;
    protected string _plantName;
    protected int _health;
    protected float _actionCooldownTimer;
    protected float _performActionTimer;

    public PlantProperties(long id, PlantSO plantSO)
    {
        _id = id;
        _plantName = plantSO.plantName;
        _health = plantSO.health;
        _actionCooldownTimer = plantSO.actionCooldownTimer;
        _performActionTimer = plantSO.performActionTimer;
    }
    
    public long Id => _id;  
    public string PlantName => _plantName;
    public int Health => _health;
    public void SetHealth(int health)
    {
        _health = health;
    }
    public float ActionCooldownTimer => _actionCooldownTimer;
    public float PerformActionTimer => _performActionTimer;
}
