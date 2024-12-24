using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    bool hasDied;

    Color fadeColor;

    [Header("Event Timings")]
    [SerializeField] float introDelay;
    [SerializeField] float deathTime;
    [SerializeField] float fadeTime;

    [Header("Player Detection")]
    [SerializeField] float detectionDistance;
    [SerializeField] float detectionRadius;
    
    [Header("Punch Attack")]
    [SerializeField] float punchStartup;
    [SerializeField] float punchEndDelay;
    [SerializeField] float punchActive;
    [SerializeField] float punchRandomRate;

    [Header("Sweep Attack")]
    [SerializeField] float sweepStartup;
    [SerializeField] float sweepTrackTime;
    [SerializeField] float sweepEndDelay;
    [SerializeField] float sweepActive;
    [SerializeField] float sweepRandomRate;

    [Header("Jump Attack")]
    [SerializeField] float jumpAttackDelay;
    [SerializeField] float jumpTrackTime;
    [SerializeField] float jumpAttackActive;

    [Header("Serialized Components")]
    [SerializeField] Animator animator;
    [SerializeField] GameObject healthbar;
    [SerializeField] GameObject deathBlast;
    [SerializeField] Material deathMaterial;
    [SerializeField] SkinnedMeshRenderer modelRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
        soulManager = FindObjectOfType<SoulManager>();
        playerController = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        fadeColor = deathMaterial.color;
        fadeColor.a = 1;
        deathMaterial.color = fadeColor;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (soulManager.bossActive && !hasDied)
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

                Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward * detectionDistance, detectionRadius);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponent<PlayerController>() != null && canMove)
                    {
                        animator.SetBool("isRunning", false);
                        float attackRoll = Random.Range(0f, 1f);
                        Debug.Log(attackRoll);

                        if(attackRoll <= punchRandomRate)
                        {
                            StartCoroutine(PunchAttack());
                        }
                        else if(attackRoll <= sweepRandomRate)
                        {
                            StartCoroutine (SweepAttack());
                        }
                        else
                        {
                            StartCoroutine(JumpAttack());
                        }
                    }

                }
            }
            else if (trackingAttack)
            {
                agent.SetDestination(playerController.transform.position);
            }

        }

        if (hasDied && deathMaterial.color.a > 0)
        {
            
            fadeColor.a -= fadeTime * Time.deltaTime;
            deathMaterial.color = fadeColor;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            StartCoroutine(HandleDeath());
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
        animator.SetTrigger("JumpAttack");
        canMove = false;

        yield return new WaitForSeconds(jumpTrackTime);
        trackingAttack = false;
        StartCoroutine(ActivateHitBox(jumpAttackActive));
        yield return new WaitForSeconds(jumpAttackDelay);
        canMove = true;
    }

    IEnumerator SweepAttack() 
    {
        animator.SetTrigger("Sweep");
        canMove = false;
        StartCoroutine(ActivateHitBox(sweepActive));
        yield return new WaitForSeconds(sweepEndDelay);
        canMove = true;
    }

    IEnumerator PunchAttack()
    {
        animator.SetTrigger("Punch");
        canMove = false;
        StartCoroutine(ActivateHitBox(punchActive));
        yield return new WaitForSeconds(punchEndDelay);
        canMove = true;
    }

    IEnumerator ActivateHitBox(float activeTime)
    {
        attacking = true;
        yield return new WaitForSeconds(activeTime);
        attacking = false;
    }

    IEnumerator HandleDeath()
    {
        animator.SetBool("isRunning", false);
        modelRenderer.material = deathMaterial;
        hasDied = true;
        canMove = false;
        deathBlast.SetActive(true);
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(deathTime);
        healthbar.SetActive(false);
        soulManager.bossDefeated = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward * detectionDistance, detectionRadius);
    }
}
