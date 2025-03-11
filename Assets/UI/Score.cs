using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text.RegularExpressions;
using GameWorld.GameObjects.PowerupPanels;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/*
 * Score and Health is handled by the "Zombie" class. 
 */
public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI playerHealthText;

    private int score = 0;
    private int round = 1;
    private int zombiesKilled = 0; 
    

    public GameObject player;

    
    //a counter that keeps track of the rounds, it is used for spawning the power up panels
    public int roundCounter = 0; 
        
        
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score;
        roundText.text = "Round: " + round;
        playerHealthText.text = "HP: " + player.GetComponent<Player>().Health;
    }
    void Update()
    {
    }

    public void UpdateRound()
    {
        roundText.text = "Round: " + round;
        if (zombiesKilled == 20)
            zombiesKilled = 0; 
    }
    

    //increment the score
    public void IncrementScore(int value)
    {
        score += value;
        zombiesKilled++;
        if (zombiesKilled >= 20) //if 20 zombies are killed, increment the round and reset the zombies killed
        {
            UpdateRound();
            round++;
            roundCounter++;
            zombiesKilled = 0;
            
            Game.Instance.NotifyRound();
        }
        scoreText.text = "Score: " + score;
    }
    
    //update the player health
    public void UpdatePlayerHealth(float health)
    {
        playerHealthText.text = "Health: " + health;
    }

    public int GetZombiesKilled()
    {
        return zombiesKilled;
    }

    
}
