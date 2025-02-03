using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] int maxHealth;
    [SerializeField] int maxMagic;
    [SerializeField] int attackPower;

    [Header("Level Up")]
    [SerializeField] AnimationCurve experienceCurve;
    [SerializeField] float attackMultiplier;
    [SerializeField] float healthMultiplier;
    [SerializeField] float magicMultiplier;

    [Header("Scripts")]
    [SerializeField] BattleScript battleUI;

    [SerializeField] Animator playerAnimator;

    TurnSystem turnSystem;

    bool playerAlive = true;

    int currentLevel;
    int previousLevelExp, totalExp, nextLevelExp;

    int currentMagic;
    int currentHealth;
    int baseHealth;
    int baseMagic;
    int baseAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 1;
        currentHealth = maxHealth;
        currentMagic = maxMagic;

        baseHealth = maxHealth;
        baseMagic = maxMagic;
        baseAttack = attackPower;

        turnSystem = FindObjectOfType<TurnSystem>();

        nextLevelExp = (int)experienceCurve.Evaluate(currentLevel + 1);
    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        UpdateUIValues();
        CheckHealth();
    }

    public bool CastMagic(int cost)
    {
        if (currentMagic >= cost)
        {
            currentMagic -= cost;
            UpdateUIValues();
            playerAnimator.SetTrigger("Cast");
            return true;  
        }
        else
        {
            Debug.Log("not enough magic");
            return false;
        }
    }

    public void RestoreHealth(string item)
    {
        int amount = maxHealth/3;

        if (item == "potion" && currentHealth + amount < maxHealth)
        {
            currentHealth += amount;
            playerAnimator.SetTrigger("Item");
        }
        else if(item == "elixir")
        {
            currentHealth = maxHealth;
        }
        UpdateUIValues();
    }

    public void RestoreMagic(string item)
    {
        int amount = maxMagic/3;
        if(item == "ether" && currentMagic + amount < maxMagic)
        {
            currentMagic += amount;
            playerAnimator.SetTrigger("Item");
        }
        else if(item == "elixir")
        {
            currentMagic = maxMagic;
            playerAnimator.SetTrigger("Item");
        }
        UpdateUIValues();
    }

    public void GainExp(int gained)
    {
        totalExp += gained;
        checkEXP();
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public bool GetPlayerLife()
    {
        return playerAlive;
    }

    void checkEXP()
    {
        if(totalExp >= nextLevelExp)
        {
            levelUp();
        }
    }

    void levelUp()
    {
        currentLevel++;
        maxHealth = baseHealth + (int)(currentLevel * healthMultiplier);
        maxMagic = baseMagic + (int)(currentLevel * magicMultiplier);
        attackPower = baseAttack + (int)(currentLevel * attackMultiplier);

        //previousLevelExp = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExp = (int)experienceCurve.Evaluate(currentLevel + 1);
        checkEXP();
    }

    void decreaseLevel()
    {
        if(currentLevel > 1)
        {
            currentLevel--;
        }
        maxHealth = baseHealth + (int)(currentLevel * healthMultiplier);
        maxMagic = baseMagic + (int)(currentLevel * magicMultiplier);
        attackPower = baseAttack + (int)(currentLevel * attackMultiplier);

        totalExp = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExp = (int)experienceCurve.Evaluate(currentLevel + 1);

        currentHealth = maxHealth;
        currentMagic = maxMagic;
    }

    void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            playerAnimator.SetTrigger("Death");
            playerAlive = false;
            UpdateUIValues();
            decreaseLevel();
            StartCoroutine(battleUI.ExitBattle());
        }
    }

    public void SetPlayerLife()
    {
        playerAlive = true;
    }

    public void UpdateUIValues()
    {
        battleUI.currentHP = (float)currentHealth;
        battleUI.currentMP = (float)currentMagic;
        battleUI.maxHP = (float)maxHealth;
        battleUI.maxMP = (float)maxMagic;
        battleUI.UpdateUI();
    }

}
