﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    Image healthBar;
    float maxHealth = 100f;
    private static float health;
    // Use this for initialization
    void Start()
    {
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        health = characterControls.health;
        healthBar.fillAmount = health / maxHealth;
    }
}
