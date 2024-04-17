using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{

    public Action OnOutOfHealth;

    private float maxHealth;
    private float currentHealth;

    public Health()
    {
        maxHealth = 100f;
        currentHealth = maxHealth;
    }

    public float GetHealthNormalized()
    {
        return currentHealth / maxHealth;
    }

    public void UpdateMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnOutOfHealth?.Invoke();
        }
    }
}
