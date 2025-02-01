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
    TurnSystem turnSystem;

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
    }

    public bool CastMagic(int cost)
    {
        if (currentMagic >= cost)
        {
            currentMagic -= cost;
            UpdateUIValues();
            turnSystem.changeTurn();
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
        }
        else if(item == "elixir")
        {
            currentMagic = maxMagic;
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

    public void UpdateUIValues()
    {
        battleUI.currentHP = (float)currentHealth;
        battleUI.currentMP = (float)currentMagic;
        battleUI.maxHP = (float)maxHealth;
        battleUI.maxMP = (float)maxMagic;
        battleUI.UpdateUI();
    }

}
