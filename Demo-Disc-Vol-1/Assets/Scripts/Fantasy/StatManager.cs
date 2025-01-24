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

    int currentLevel = 1;
    int previousLevelExp, totalExp, nextLevelExp;

    int currentMagic;
    int currentHealth;
    int baseHealth;
    int baseMagic;
    int baseAttack;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMagic = maxMagic;

        baseHealth = maxHealth;
        baseMagic = maxMagic;
        baseAttack = attackPower;

    }

    public void DamagePlayer(int damage)
    {
        currentHealth -= damage;
        UpdateUIValues();
    }

    public void CastMagic()
    {
        if(currentMagic >= 5) 
        {
            currentMagic -= 5;
            UpdateUIValues();
        }
    }

    public void GainExp(int gained)
    {
        totalExp += gained;
        checkEXP();
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
        baseMagic = baseMagic + (int)(currentLevel * magicMultiplier);
        attackPower = baseAttack + (int)(currentLevel * attackMultiplier);

        //previousLevelExp = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelExp = (int)experienceCurve.Evaluate(currentLevel + 1);
        checkEXP();
    }

    public int GetLevel()
    {
        return currentLevel;
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
