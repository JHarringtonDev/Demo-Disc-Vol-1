using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    SoulManager soulManager;
    NavMeshAgent agent;
    PlayerController playerController;

    public bool attacking;

    public float maxHealth;

    public float currentHealth;

    [SerializeField] Animator animator;
    [SerializeField] GameObject healthbar;
    

    // Start is called before the first frame update
    void Start()
    {
        soulManager = FindObjectOfType<SoulManager>();
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
       if(soulManager.bossActive)
        {
            agent.SetDestination(playerController.transform.position);
            animator.SetBool("isRunning", true);

            if(healthbar.activeSelf == false)
            {
                healthbar.SetActive(true);
            }
        } 
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthbar.SetActive(false);
            Destroy(gameObject);
        }
    }
}
