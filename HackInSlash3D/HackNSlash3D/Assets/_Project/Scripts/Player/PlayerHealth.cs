using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject bloodParticles;
    
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
        if (isDead) return;

        currentHealth -= dmgVal;
        if (currentHealth <= 0) MakePlayerDie();
        UpdateSliderVal();
        SpawnParticles();
        Recoil();
    }

    void Recoil()
    {
        transform.DOComplete();
        transform.DOPunchScale(Vector3.one * -0.2f, 0.4f, 0, 0);
    }

    void SpawnParticles()
    {
        Destroy(Instantiate(bloodParticles, transform.position, Quaternion.identity), 2.0f);
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
        transform.DOScale(Vector3.zero, 0.3f);
        Instantiate(GameOverUI);
    }
}
