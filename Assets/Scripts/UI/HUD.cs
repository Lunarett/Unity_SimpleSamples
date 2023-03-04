using System;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ScoreDisplay scoreDisplay;
    [SerializeField] private GameObject gameOverObject;

    private int score = 0;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void Start()
    {
        SetHealth(playerHealth.CurrentHealth, playerHealth.MaxHealth);
        scoreDisplay.SetScoreText(score.ToString());
        gameOverObject.SetActive(false);
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        float value = currentHealth / maxHealth;
        healthBar.SetSliderValue(value);
    }

    public void AddScore()
    {
        score++;
        scoreDisplay.SetScoreText(score.ToString());
    }

    public void ShowGameOver(bool isVisible)
    {
        gameOverObject.SetActive(isVisible);
    }
}
