using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int poolSize;
    

    protected Stack<GameObject> objectStack = new Stack<GameObject>();

    protected virtual void Awake()
    {
        FillPool();
    }

    protected virtual void FillPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, transform);
            obj.SetActive(false);
            objectStack.Push(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectStack.Count == 0)
        {
            GameObject obj = Instantiate(objectPrefab, transform);
            objectStack.Push(obj);
        }
        GameObject objectToReturn = objectStack.Pop();
        return objectToReturn;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        objectStack.Push(obj);
    }


}
