using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount; 
    

    
    
    public void SetHealth(float health)
    {
        healthBar.fillAmount = health / healthAmount;
    }
}