using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthControlling : MonoBehaviour
{
    public GameObject healthBar;
    public Health health;

    private Transform healthLevel;
    protected CreatureSpawner spawner;
    private LoadingSceneScript loadingScript;

    public void Initialize(Health health)
    {
        this.health = health;
        InitialHealth();
    }

    void Start()
    {
        loadingScript = FindObjectOfType<LoadingSceneScript>();
        spawner = FindObjectOfType<CreatureSpawner>();
        loadingScript.AddObjectScene(this.gameObject);
        InitialHealth();
    }

    private void InitialHealth()
    {
        if (healthBar.transform.childCount > 0)
            healthLevel = healthBar.transform.GetChild(0);
        TakeDamage(0f);
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);

        if (health.currentHealth <= 0)
            Die();

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        var currentScale = healthLevel.localScale;
        currentScale.x = health.currentHealth / health.maxHealth;

        healthLevel.localScale = currentScale;
    }

    public virtual void Die()
    {
        loadingScript.RemoveObjectScene(gameObject);
        Destroy(gameObject);
    }
}
