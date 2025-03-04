
public class Sunflower : Plant
{
    private SunManager _sunManager;
    private SunflowerView _sunflowerView;
    private SunflowerProperties _sunflowerProperties;



    public Sunflower(PlantManager parent, PlantProperties properties, PlantView view) : base(parent, properties, view)
    {
    }

    public override void Initialize()
    {
        base.Initialize();
        _sunflowerView = (SunflowerView)_view;
        _sunflowerProperties = (SunflowerProperties)_properties;
        _sunManager = StageManager.Instance.GetSunManager();
    }

    protected override bool CanPerformAction()
    {
        return true;
    }

    protected override void PerformAction()
    {
        ProduceSun();
    }

    private void ProduceSun()
    {
        _sunManager.GetObject(_sunflowerView.SunSpawnPoint.position);
    }
}
