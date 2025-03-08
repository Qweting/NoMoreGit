using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is responsible for spawning zombies at random intervals
 * It uses the ZombieOP script to get zombies from the object pool
 * It spawns zombies at random intervals between 0.5 seconds
 * It spawns zombies at random spawn points
 */
public class NormalZombieSpawns : MonoBehaviour
{
    public Transform[] _spawnPoints; //array of spawn points
    private readonly float _spawnDelay = 0.5f; //delay between spawns
    private bool _respawn = true; //flag to control spawning
    private int _zombieCount = 10; //number of zombies to spawn
    
    
    
    
    void Start()
    { 
        StartCoroutine(SpawnZombies());
    }
    
    IEnumerator SpawnZombies()
    {
        for (int i = 0; i < _zombieCount; i++)
        {
            GameObject zombie = ZombieOP.SharedInstance.GetPoolObject();
            if (zombie != null)
            {
                zombie.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
                zombie.gameObject.SetActive(true);
                Debug.Log("Successfully spawned zombie at " + zombie.transform.position);
            }
            yield return new WaitForSeconds(_spawnDelay); //delay the spawn between zombies so they don't spawn into each other
        }
        yield return new WaitForSeconds(5f); //delay of 5 seconds
        _respawn = false; //allow spawning again
    }

    // Update is called once per frame
    void Update()
    {
        if (!_respawn) //if we are done spawning, spawn again
        {
            _respawn = true; //don't allow anymore spawning
            StartCoroutine(SpawnZombies());
        }
    }

    public int ZombieCount
    {
        get => _zombieCount;
        set => _zombieCount = value;
    }

    public bool Respawn
    {
        get => _respawn;
        set => _respawn = value;
    }
}
