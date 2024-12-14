using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    SoulManager soulManager;
    NavMeshAgent agent;
    PlayerController playerController;

    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        soulManager = FindObjectOfType<SoulManager>();
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       if(soulManager.bossActive)
        {
            agent.SetDestination(playerController.transform.position);
        } 
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
