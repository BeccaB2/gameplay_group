using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    characterControls stats;
    Image healthBar;
    float maxHealth = 100f;
    private static float health;
    // Use this for initialization
    void Start()
    {
        stats = GameObject.Find("Mage").GetComponent<characterControls>();
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //health = stats.health;
        healthBar.fillAmount = health / maxHealth;
    }
}
