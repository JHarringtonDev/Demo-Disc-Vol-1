using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FantasyEnemy : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] string typeResist;
    [SerializeField] string typeWeakness;

    int currentHealth;

    [SerializeField] Image healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        showHealthValue();
    }

    void showHealthValue()
    {
        healthBar.fillAmount = (float)currentHealth/(float)maxHealth;
    }

    public void TakeDamage(int damage, string type)
    {
        if(typeResist.Contains(type))
        {
            currentHealth -= damage/2;
        }
        else if(typeWeakness.Contains(type))
        {
            currentHealth -= damage * 2;
        }
        else
        {
            currentHealth -= damage;
        }

        showHealthValue();
    }
}
