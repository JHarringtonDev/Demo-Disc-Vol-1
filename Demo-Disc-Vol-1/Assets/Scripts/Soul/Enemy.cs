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
    [SerializeField] float detectionDistance;
    [SerializeField] float detectionRadius;

    [Header("Kick")]
    [SerializeField] float kickRate;
    [SerializeField] float kickEndLag;

    [Header("Slash 1")]
    [SerializeField] float slash1Rate;
    [SerializeField] float slash1EndLag;

    [Header("Slash 2")]
    [SerializeField] float slash2Rate;
    [SerializeField] float slash2EndLag;

    [SerializeField] bool hallwayEnemy;
    [SerializeField] bool roomEnemy;

    NavMeshAgent agent;
    NavMeshHit hit;
    Vector3 startingLocation;
    [SerializeField] Animator animator;


    float currentHealth;
    bool blocked;
    bool isAttacking;
    bool followingPlayer = true;

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
        if(followingPlayer)
        {
            moveEnemy();
        }

        if (soulManager.enemiesAttacking < 2 && !isAttacking)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * detectionDistance, detectionRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<PlayerController>() != null)
                {
                    animator.SetBool("isRunning", false);
                    float attackRoll = Random.Range(0f, 1f);
                    Debug.Log(attackRoll);

                    if (attackRoll <= kickRate)
                    {
                        StartCoroutine(KickAttack());
                    }
                    else if (attackRoll <= slash1Rate)
                    {
                        StartCoroutine(Slash1Attack());
                    }
                    else
                    {
                        StartCoroutine(Slash2Attack());
                    }
                }
            }
        }
    }

    IEnumerator KickAttack()
    {
        followingPlayer = false;
        isAttacking = true;
        soulManager.enemiesAttacking++;
        animator.SetTrigger("Kick");
        yield return new WaitForSeconds(kickEndLag);
        soulManager.enemiesAttacking--;
        followingPlayer = true;
        isAttacking = false;
    }

    IEnumerator Slash1Attack()
    {
        followingPlayer = false;
        isAttacking = true;
        soulManager.enemiesAttacking++;
        animator.SetTrigger("Slash 1");
        yield return new WaitForSeconds(slash1EndLag);
        soulManager.enemiesAttacking--;
        followingPlayer = true;
        isAttacking = false;
    }

    IEnumerator Slash2Attack()
    {
        followingPlayer = false;
        isAttacking = true;
        soulManager.enemiesAttacking++;
        animator.SetTrigger("Slash 2");
        yield return new WaitForSeconds(slash2EndLag);
        soulManager.enemiesAttacking--;
        followingPlayer = true;
        isAttacking = false;
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
        animator.SetBool("IsRunning", true);

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
        else if (roomEnemy)
        {
            if (soulManager.roomActive)
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
