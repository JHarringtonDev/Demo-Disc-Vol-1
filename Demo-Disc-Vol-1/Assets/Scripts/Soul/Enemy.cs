using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    PlayerController player;
    GameManager gameManager;
    SoulManager soulManager;


    [SerializeField] float damageAmount;
    [SerializeField] float maxHealth;

    [SerializeField] bool hallwayEnemy;
    [SerializeField] bool roomEnemy;

    NavMeshAgent agent;
    NavMeshHit hit;
    Vector3 startingLocation;


    float currentHealth;
    bool blocked;
    bool followingPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        soulManager = FindObjectOfType<SoulManager>();
        gameManager = FindObjectOfType<GameManager>();

        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        startingLocation = transform.position;
    }

    private void Update()
    {
        moveEnemy();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.StartCoroutine("TakeDamage", damageAmount);
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

    void moveEnemy()
    {
        if (hallwayEnemy)
        {
            if (soulManager.hallwayActive)
            {
                blocked = NavMesh.Raycast(transform.position, player.transform.position, out hit, NavMesh.AllAreas);
                //Debug.DrawLine(transform.position, player.transform.position, blocked ? Color.red : Color.green);

                if (!blocked)
                {
                    agent.SetDestination(player.transform.position);
                }
            }
            else
            {
                agent.SetDestination(startingLocation);
            }
            
        }
    }
}
