using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Health
{
    public float maxHealth;
    public float currentHealth;

    public Health(float max, float current)
    {
        maxHealth = max;
        currentHealth = current;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Math.Max(0, currentHealth - damage);
    }
}
