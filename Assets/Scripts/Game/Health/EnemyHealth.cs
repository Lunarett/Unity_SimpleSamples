using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealthAmount = 100.0f;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject healthObject;

    private float currentHealth;
    private HUD hud;
    private bool died;
    private Transform playerTransform;
    private Camera camera;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        camera = FindObjectOfType<PlayerView>().PlayerCamera;
        hud = FindObjectOfType<HUD>();
        currentHealth = maxHealthAmount;
    }

    private void Start()
    {
        healthBar.SetSliderValue(currentHealth / maxHealthAmount);
    }

    private void Update()
    {
        healthObject.transform.LookAt(camera.transform);
    }

    public void Damage(float damageAmount)
    {
        HealthChanged();
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0.0f, maxHealthAmount);
        Debug.Log($"Daamge! Enemy Health: {currentHealth} Damage value {damageAmount}");
        if (currentHealth <= 0.0f)
        {
            Death();
        }
    }
    
    private void Death()
    {
        if (died) return;
        died = true;
        Debug.Log("Enemy Died");
        hud.AddScore();
        Destroy(gameObject);
    }

    private void HealthChanged()
    {
        healthBar.SetSliderValue(currentHealth / maxHealthAmount);
        Debug.Log($"enemy HP Changed: {currentHealth}");
    }
}
