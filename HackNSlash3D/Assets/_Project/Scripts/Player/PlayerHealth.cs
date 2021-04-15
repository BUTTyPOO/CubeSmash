using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject GameOverUI;
    
    float currentHealth;
    bool isDead = false;

    public delegate void PlayerDiedHandler();
    public event PlayerDiedHandler playerDiedEvent;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float dmgVal)
    {
        currentHealth -= dmgVal;
        if (currentHealth <= 0) MakePlayerDie();
        UpdateSliderVal();
    }

    void UpdateSliderVal()
    {
        healthSlider.value = currentHealth / maxHealth;
    }

    void MakePlayerDie()
    {
        if (isDead) return;
        isDead = true;
        
        playerDiedEvent?.Invoke();
        Instantiate(GameOverUI);
    }
}
