using System.Collections.Generic;
using UnityEngine;
public class SurfaceOP : MonoBehaviour


{
    public static SurfaceOP SharedInstance;
    public List<GameObject> poolObjects;
    public GameObject objectPool;
    public int amountToPool; 
    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        poolObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 1; i < amountToPool; i++)
        {
            tmp = Instantiate(objectPool);
            tmp.SetActive(false);
            poolObjects.Add(tmp);
        }
    }
    
    public GameObject GetPoolObject()
    {
        for (int i = 1; i < amountToPool; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
                return poolObjects[i];
        }

        return null; 
    }


    
    
}
