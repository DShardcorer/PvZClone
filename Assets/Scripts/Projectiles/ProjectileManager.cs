
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager :MonoBehaviour, IDictFactory, IManager
{
    private StageManager _parent;
    [SerializeField] private List<PlantSO> plantSOList;
    private Dictionary<string, PlantSO> projectileSOList = new Dictionary<string, PlantSO>();
    private List<Projectile> _projectiles = new List<Projectile>();
    private long _currentId = 0;
    private PoolManager _poolManager;

    public void Initialize(StageManager parent)
    {
        _parent = parent;
        _poolManager = StageManager.Instance.GetPoolManager();
        PopulateDictionary();
    }

    private void PopulateDictionary()
    {
        foreach (var plantSO in plantSOList)
        {
            if (!projectileSOList.ContainsKey("Projectile" + plantSO.plantName))
            {
                projectileSOList.Add("Projectile" + plantSO.plantName, plantSO);
            }
        }
    }



    public void ReturnObject(IController controller)
    {
        Projectile projectile = (Projectile)controller;
        _projectiles.Remove(projectile);
        _poolManager.ReturnObject(projectile.GetProperties().ProjectileName, projectile.GetView().gameObject);
        projectile.Dispose();
    }


    public IController GetObject(string projectileName, Vector2 shootPoint)
    {
        if (!projectileSOList.TryGetValue(projectileName, out PlantSO plantSO))
        {
            Debug.LogError($"ProjectileManager: No PlantSO found for {projectileName}");
            return null;
        }



        GameObject projectileGameObject = _poolManager.GetObject(projectileName);
        if (projectileGameObject == null)
        {
            Debug.LogError($"ProjectileManager: No pooled object found for {projectileName}");
            return null;
        }

        projectileGameObject.transform.position = shootPoint;
        ProjectileView projectileView = projectileGameObject.GetComponent<ProjectileView>();
        ProjectileProperties projectileProperties = new ProjectileProperties(_currentId, plantSO);
        Projectile projectile = new Projectile(this, projectileProperties, projectileView);
        projectile.Initialize();
        _projectiles.Add(projectile);
        _currentId++;

        return projectile;
    }

}
