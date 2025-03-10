using System;
using System.Collections;
using System.Collections.Generic;
using GameWorld.GameObjects.PowerupPanels;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Instance;

    
    public GameObject scoreObject;
    
    
    private Score score;
    private Player player;
    private Bullet bullet;
    private ZombieBoss zombieBoss;


    public bool spawnPowerUp = false;
    public bool spawnZombieBoss = false;
    public bool resetPowerUp= false;
    
    
    public Button startButton;
    
    
    
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
        if(spawnPowerUp)
            PowerupManager.Instance.SpawnPowerup();
    }


    //make the game more diffuclt by incrmenet zombies per round
    public void NotifyRound()
    {
        Math.Max(score.roundCounter + 2, 40);
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
    
    
}
