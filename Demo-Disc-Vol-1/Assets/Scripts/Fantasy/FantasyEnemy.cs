using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FantasyEnemy : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int baseAttack;
    [SerializeField] string typeResist;
    [SerializeField] string typeWeakness;
    [SerializeField] float levelVariance;
    [SerializeField] float maxRoll;
    [SerializeField] Animator animator;
    [SerializeField] string enemyName;
    [SerializeField] TextMeshProUGUI nameDisplay;
    StatManager statManager;
    BattleScript battleScript;
    TurnSystem turns;
    int currentHealth;
    int level;
    bool isAlive;

    [SerializeField] Image healthBar;

    private void Start()
    {
        statManager = FindObjectOfType<StatManager>();
        battleScript = FindObjectOfType<BattleScript>();
        turns = FindObjectOfType<TurnSystem>();

        int playerLevel = statManager.GetLevel();
        level = (int)(playerLevel + (playerLevel * Random.Range(levelVariance, 1.25f)));

        if(level < 0) 
        { 
            level = playerLevel;
        }
        nameDisplay.text = $"{enemyName} lvl.{level}";
        currentHealth = maxHealth;
        showHealthValue();

        isAlive = true;
    }

    void showHealthValue()
    {
        healthBar.fillAmount = (float)currentHealth/(float)maxHealth;
        Debug.Log("Enemy Health: " + currentHealth);
    }

    public void HandleAttack()
    {
        int damageRoll = (int)(baseAttack + (Random.Range(1f, maxRoll) * level));
        statManager.DamagePlayer(damageRoll);
        animator.SetTrigger("Slash 1");
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

        checkHealth();

        showHealthValue();
    }

    void checkHealth()
    {
        if(currentHealth <= 0) 
        {
            currentHealth = 0;
            animator.SetTrigger("Death");
            isAlive = false;
            StartCoroutine(battleScript.ExitBattle());
        }
        else
        {
            turns.changeTurn();
        }
    }

    public bool GetEnemyLife()
    {
        return isAlive;
    }
}
