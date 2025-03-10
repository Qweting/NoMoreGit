using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void OnClick()
    {
        Game.Instance.UnPause();
        gameObject.SetActive(false);
    }
    
    void Start()
    {
        startButton.onClick.AddListener(OnClick);
    }
    

}
