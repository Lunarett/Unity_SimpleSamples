using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealthAmount = 100.0f;

    private float currentHealth;
    private HUD hud;
    private PlayerMovement playerMovement;
    private PlayerView playerView;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealthAmount;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerView = GetComponent<PlayerView>();
        hud = FindObjectOfType<HUD>();
        currentHealth = maxHealthAmount;
    }

    public void Damage(float damageAmount)
    {
        HealthChanged();
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0.0f, maxHealthAmount);

        if (currentHealth <= 0.0f)
        {
            Death();
        }
    }
    
    private void Death()
    {
        playerMovement.SetInputActive(false);
        playerView.SetInputActive(false);
        hud.ShowGameOver(true);
    }

    private void HealthChanged()
    {
        hud.SetHealth(currentHealth, maxHealthAmount);
    }
}
