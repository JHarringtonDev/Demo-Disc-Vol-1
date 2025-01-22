using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float maxMagic;
    [SerializeField] float attackPower;

    [Header("Level thresholds")]
    [SerializeField] int level1Experience;
    [SerializeField] int level2Experience;
    [SerializeField] int level3Experience;
    [SerializeField] int level4Experience;
    [SerializeField] int level5Experience;
    [SerializeField] int level6Experience;
    [SerializeField] int level7Experience;
    [SerializeField] int level8Experience;
    [SerializeField] int level9Experience;
    [SerializeField] int level10Experience;


    int expPoints = 0;
    int playerLevel;
    float currentMagic;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentMagic = maxMagic;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DamagePlayer(float damage)
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

    void checkEXP()
    {
        if (expPoints > level1Experience && playerLevel < 2)
        {
            levelUp();
        }
    }

    void levelUp()
    {

    }
}
