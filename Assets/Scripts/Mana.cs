using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana
{
    private float maxMana;
    private float currentMana;
    private float manaRegenAmount;

    public Mana()
    {
        maxMana = 100f;
        currentMana = 100f;
        manaRegenAmount = 1f;
    }

    public void Update()
    {

        currentMana += manaRegenAmount * Time.deltaTime;
        currentMana = Mathf.Clamp(currentMana, 0f, maxMana);  
    }

    public bool TrySpendAmount(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }

    public float GetManaNormalized()
    {
        return currentMana / maxMana;
    }

    public void UpdateMaxMana(float newMaxMana)
    {
        maxMana = newMaxMana;
        currentMana = maxMana;
    }

    public void ResetMana()
    {
        currentMana = maxMana;
    }
}
