using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float damageAmount;
    [SerializeField] float maxHealth;

    NavMeshAgent agent;

    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.StartCoroutine("TakeDamage",damageAmount);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
