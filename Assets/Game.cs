using System;
using System.Collections;
using System.Collections.Generic;
using GameWorld.GameObjects.PowerupPanels;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Instance;

    [Header("Boss Settings")]
    [SerializeField] private GameObject zombieBossPrefab; // Reference to the ZombieBoss prefab
    [SerializeField] private Transform bossSpawnPoint; // Optional: specific spawn point for the boss
    [SerializeField] private int bossSpawnRound = 6; // First boss appears at round 6
    [SerializeField] private int bossSpawnInterval = 6; // Then every 6 rounds
    
    [Header("Powerup Settings")]
    [SerializeField] private int powerupSpawnRound = 5; // First powerup appears at round 5
    [SerializeField] private int powerupSpawnInterval = 5; // Then every 5 rounds
    
    private Score score;
    private Player player;
    private Bullet bullet;
    private ZombieBoss currentBoss; // Reference to track the current boss instance
    
    public Button startButton;

    private bool respawnZombies; 
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        Start();
        Pause();
    }
    
    void Start()
    {
        score = GameObject.Find("Score").GetComponent<Score>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for powerup spawning
        if (score.roundCounter >= powerupSpawnRound && 
            score.roundCounter % powerupSpawnInterval == 0 && 
            score.roundCounter > 0)
        {
            // Only spawn powerups if the PowerupManager exists
            if (PowerupManager.Instance != null)
            {
                // Check if powerups are already active before spawning new ones
                bool shouldSpawnPowerups = true;
                
                // You might need to add a method in PowerupManager to check if powerups are active
                // For now, we'll just call SpawnPowerup directly
                if (shouldSpawnPowerups)
                {
                    PowerupManager.Instance.SpawnPowerup();
                }
            }
        }
            
        // Check for boss spawning
        if (score.roundCounter >= bossSpawnRound && 
            score.roundCounter % bossSpawnInterval == 0 && 
            score.roundCounter > 0 && 
            currentBoss == null)
        {
            SpawnBoss();
        }
        
    }

    // Method to spawn the ZombieBoss
    public void SpawnBoss()
    {
        if (zombieBossPrefab == null)
        {
            Debug.LogError("ZombieBoss prefab not assigned in Game class!");
            return;
        }

        Vector3 spawnPosition;
        
        // Use the designated spawn point if available, otherwise use a default position
        if (bossSpawnPoint != null)
        {
            spawnPosition = bossSpawnPoint.position;
        }
        else
        {
            // Default spawn position (you can adjust this as needed)
            spawnPosition = new Vector3(0, 0, 50); // Spawning far from player's position
        }
        
        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Cannot find player to determine boss facing direction!");
            return;
        }
        
        // Calculate direction from boss to player
        Vector3 directionToPlayer = player.transform.position - spawnPosition;
        directionToPlayer.y = 0; // Keep rotation only on the horizontal plane
        
        // Create rotation to face the player
        Quaternion rotationToFacePlayer = Quaternion.LookRotation(directionToPlayer);
        
        // Instantiate the boss with the correct rotation
        GameObject bossObject = Instantiate(zombieBossPrefab, spawnPosition, rotationToFacePlayer);
        
        // Get and store the ZombieBoss component
        currentBoss = bossObject.GetComponent<ZombieBoss>();
        
        // Announce the boss spawn (optional)
        Debug.Log("Boss spawned at round " + score.roundCounter + " facing the player");
    }

    // Method to manually spawn powerups (can be called from UI or other events)
    public void ForceSpawnPowerups()
    {
        if (PowerupManager.Instance != null)
        {
            PowerupManager.Instance.SpawnPowerup();
            Debug.Log("Powerups manually spawned");
        }
        else
        {
            Debug.LogError("PowerupManager instance not found!");
        }
    }

    // Method to manually trigger a boss spawn (can be called from UI buttons or other events)
    public void ForceBossSpawn()
    {
        if (currentBoss == null)
        {
            SpawnBoss();
        }
        else
        {
            Debug.Log("Cannot spawn boss - one already exists");
        }
    }

    //make the game more difficult by increment zombies per round
    public void NotifyRound()
    {
        Math.Max(score.roundCounter + 2, 40);
        
        // Check if it's a boss round
        if (score.roundCounter >= bossSpawnRound && score.roundCounter % bossSpawnInterval == 0)
        {
            // If the previous boss was destroyed, spawn a new one
            if (currentBoss == null)
            {
                SpawnBoss();
            }
        }
        
        // Check if it's a powerup round
        if (score.roundCounter >= powerupSpawnRound && score.roundCounter % powerupSpawnInterval == 0)
        {
            if (PowerupManager.Instance != null)
            {
                PowerupManager.Instance.SpawnPowerup();
            }
        }
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        MenuUI.Instance.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        MenuUI.Instance.gameObject.SetActive(true);
    }
    
    // Reset the boss reference when it's destroyed
    public void BossDestroyed()
    {
        currentBoss = null;
    }

    public int GetKilled()
    {
        return score.GetZombiesKilled();
    }
    
    public bool RespawnZombies()
    {
        if (score.GetZombiesKilled() >= 10)
            respawnZombies = false;
        else respawnZombies = true;

        return respawnZombies;
    }

    public void SetRespawnZombies(bool ss)
    {
        respawnZombies = ss;
    }
}