using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HealthBar_MA : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        SetMaxHealth(S_Stats_MA.maxHealth);
    }

    private void Update()
    {
        SetHealth(S_Stats_MA.playerHealth);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(float health)
    {
        slider.value = health; 
    }
}
