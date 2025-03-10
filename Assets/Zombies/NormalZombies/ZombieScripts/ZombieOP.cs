using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieOP : MonoBehaviour
{
    public static ZombieOP SharedInstance;
    public List<GameObject> poolObjects;
    public List<GameObject> dogPoolObjects;
    public GameObject objectToPool;
    public GameObject zombieDogToPool;
    public int initialPoolSize = 20;
    public int expandAmount = 10;
    public int dogPoolSize = 5;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        poolObjects = new List<GameObject>();
        dogPoolObjects = new List<GameObject>();
        ExpandPool(initialPoolSize);
        InitializeDogPool(dogPoolSize);
    }

    public void ExpandPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            poolObjects.Add(tmp);
        }

        Debug.Log($"Pool expanded by {amount}. New size: {poolObjects.Count}");
    }

    public void InitializeDogPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tmp = Instantiate(zombieDogToPool);
            tmp.SetActive(false);
            dogPoolObjects.Add(tmp);
        }

        Debug.Log($"Dog pool initialized with {amount} instances.");
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < poolObjects.Count; i++)
            if (!poolObjects[i].activeInHierarchy)
                return poolObjects[i];

        ExpandPool(expandAmount);
        return poolObjects[poolObjects.Count - expandAmount];
    }

    public GameObject GetDogPoolObject()
    {
        for (int i = 0; i < dogPoolObjects.Count; i++)
            if (!dogPoolObjects[i].activeInHierarchy)
                return dogPoolObjects[i];

        // If no inactive dog objects are found, reuse the first one
        GameObject reusedDog = dogPoolObjects[0];
        reusedDog.SetActive(false);
        return reusedDog;
    }
}