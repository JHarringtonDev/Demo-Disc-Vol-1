using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FantasyEnemy : MonoBehaviour
{
    [SerializeField] int maxHealth;
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        showHealthValue();
    }
}
