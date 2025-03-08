using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieOP : MonoBehaviour
{
    public static ZombieOP SharedInstance;
    public List<GameObject> poolObjects;
    public GameObject objectToPool;
    public int initialPoolSize = 20;
    public int expandAmount = 10;
    
    void Awake()
    {
        SharedInstance = this;
    }
    
    void Start()
    {
        poolObjects = new List<GameObject>();
        ExpandPool(initialPoolSize);
    }
    
    public void ExpandPool(int amount)
    {
        GameObject tmp;
        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            poolObjects.Add(tmp);
            
        }
        
        Debug.Log($"Pool expanded by {amount}. New size: {poolObjects.Count}");
    }
    
    public GameObject GetPoolObject()
    {
        for(int i = 0; i < poolObjects.Count; i++)
            if (!poolObjects[i].activeInHierarchy)
                return poolObjects[i];
        
        ExpandPool(expandAmount);
        return poolObjects[poolObjects.Count - expandAmount];
    }
    
    

}
