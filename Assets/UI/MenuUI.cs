using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Button startButton;
    public Button restartButton;
    public static MenuUI Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void StartGame()
    {
        Game.Instance.UnPause();
        gameObject.SetActive(false);
    }
    
    void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
    }
}
