using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    SoulManager soulManager;
    NavMeshAgent agent;
    PlayerController playerController;

    public bool attacking;

    public float maxHealth;

    public float currentHealth;

    bool preformedIntro;
    bool canMove;
    bool trackingAttack;

    [SerializeField] float introDelay;
    [SerializeField] float jumpAttackDelay;
    [SerializeField] float jumpTrackTime;
    [SerializeField] float jumpAttackActive;

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
        if (soulManager.bossActive)
        {
            if (!preformedIntro)
            {
                StartCoroutine(OpeningBehavior());
            }
            else if (preformedIntro && canMove)
            {

                agent.SetDestination(playerController.transform.position);
                animator.SetBool("isRunning", true);

                if (healthbar.activeSelf == false)
                {
                    healthbar.SetActive(true);
                }
            }
            else if (trackingAttack)
            {
                agent.SetDestination(playerController.transform.position);
            }

            Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 4), 4);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<PlayerController>() != null && canMove)
                {
                    StartCoroutine(JumpAttack());
                }

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

    IEnumerator OpeningBehavior()
    {
        animator.SetTrigger("Roar");
        preformedIntro = true;
        yield return new WaitForSeconds(introDelay);
        canMove = true;
    }

    IEnumerator JumpAttack()
    {
        trackingAttack = true;
        animator.SetBool("isRunning", false);
        animator.SetTrigger("JumpAttack");
        canMove = false;

        yield return new WaitForSeconds(jumpTrackTime);
        trackingAttack = false;
        StartCoroutine(ActivateHitBox(jumpAttackActive));
        yield return new WaitForSeconds(jumpAttackDelay);
        canMove = true;
    }

    IEnumerator ActivateHitBox(float activeTime)
    {
        attacking = true;
        yield return new WaitForSeconds(activeTime);
        attacking = false;
    }
}
