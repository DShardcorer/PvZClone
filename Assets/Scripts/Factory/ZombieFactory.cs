using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFactory : DictFactory
{
    public static ZombieFactory Instance { get; private set; }
    [SerializeField] private List<ZombieSO> zombieSOList;
    [SerializeField] private List<ObjectPool> zombiePoolList;
    Dictionary<string, ObjectPool> zombiePoolDict = new Dictionary<string, ObjectPool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        Initialize();
        //print the dictionary
        foreach (KeyValuePair<string, ObjectPool> item in zombiePoolDict)
        {
            Debug.Log(item.Key + " " + item.Value);
        }

    }




    private void Initialize()
    {
        for (int i = 0; i < zombieSOList.Count; i++)
        {
            if (zombiePoolDict.ContainsKey(zombieSOList[i].zombieName) == false)
            {
                //Compare prefabs from the zombieSOList with the objectPool prefab
                for (int j = 0; j < zombiePoolList.Count; j++)
                {
                    if (zombieSOList[i].zombiePrefab == zombiePoolList[j].GetPrefab())
                    {
                        zombiePoolDict.Add(zombieSOList[i].zombieName, zombiePoolList[j]);
                    }
                }
            }
        }
    }   

    public override IProduct GetProduct(string zombieName, GridPosition gridPosition)
    {
        ObjectPool zombiePool = zombiePoolDict[zombieName];
        GameObject zombieGameObject = zombiePool.GetObject();
        zombieGameObject.transform.position = GridManager.Instance.GetWorldPosition(gridPosition);
        IProduct zombie = zombieGameObject.GetComponent<IProduct>();
        zombie.Initialize();
        return zombie;
    }
}
