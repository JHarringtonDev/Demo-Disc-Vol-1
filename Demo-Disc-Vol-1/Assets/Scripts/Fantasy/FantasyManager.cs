using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FantasyManager : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float maxMagic;

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
}
