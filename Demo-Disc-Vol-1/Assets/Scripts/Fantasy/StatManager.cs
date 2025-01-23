using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int maxMagic;
    [SerializeField] int attackPower;

    [Header("Level Up")]
    [SerializeField] AnimationCurve experienceCurve;
    [SerializeField] float attackMultiplier;
    [SerializeField] float healthMultiplier;
    [SerializeField] float magicMultiplier;

    int currentLevel, totalExp;
    int previousLevelExp, nextLevelExp;

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
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public float GetMagic()
    {
        return currentMagic;
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
}
