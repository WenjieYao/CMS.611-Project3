﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : Singleton<HealthBar>
{
    public Slider slider;
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
