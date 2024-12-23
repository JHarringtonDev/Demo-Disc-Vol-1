using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveInput;
    WeaponScript weaponScript;
    FlaskUI flaskUI;

    float xInput;
    float yInput;

    [HideInInspector] public int remainingRedFlasks;
    [HideInInspector] public int remainingBlueFlasks;

    bool canMove = true;
    bool isRolling;
    bool isDamageable = true;
    bool isAlive = true;
    bool redFlask = true;
    bool canSwitchFlask = true;
    bool lockedOn;
    bool isPaused;

    Transform lockTarget;

    [HideInInspector] public float health;
    [HideInInspector] public float magic;
    [HideInInspector] public float stamina;

    [Header("Attribute Settings")]
    public float maxHealth;
    public float maxMagic;
    public float maxStamina;
    [SerializeField] int maxHealthFlask;
    [SerializeField] int maxMagicFlask;

    [SerializeField] float staminaDrain;
    [SerializeField] float staminaRegen;
    [SerializeField] float damageFrames;

    float currentSpeed;
    Vector3 dir;
    Quaternion baseRotation;

    [Header("Movement Settings")]
    [SerializeField] float cameraSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float rollSpeed;
    [SerializeField] float sprintDelay;
    [SerializeField] float sprintMultiplier;
    [SerializeField] float timeHeld;
    [SerializeField] float rollCost;
    [SerializeField] float rollDelay;
    [SerializeField] float attackCost;
    [SerializeField] float attackDelay;
    [SerializeField] float magicDamage;
    [SerializeField] float magicDelay;
    [SerializeField] float magicDistance;
    [SerializeField] float magicAOE;
    [SerializeField] float magicCost;

    [Header("Serialized Objects")]
    [SerializeField] Animator animator;
    [SerializeField] Transform playerModel;
    [SerializeField] Transform[] enemysInScene;
    [SerializeField] GameObject magicEffect;


    private void Awake()
    {
        animator.SetTrigger("Stand");
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();

        weaponScript = FindObjectOfType<WeaponScript>();
        flaskUI = FindObjectOfType<FlaskUI>();

        health = maxHealth;
        stamina = maxStamina;
        magic = maxMagic;
        remainingRedFlasks = maxHealthFlask;
        remainingBlueFlasks = maxMagicFlask;

        currentSpeed = moveSpeed;

        
        baseRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        magic = Mathf.Clamp(magic, 0, maxMagic);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);



        if (isAlive && !isPaused)
        {
            movePlayer();
            handleSprint();
            rotatePlayer();
            handleAnimation();

            if (!isRolling)
            {
                playerModel.localRotation = Quaternion.Slerp(playerModel.localRotation, Quaternion.identity, 3f * Time.deltaTime);
            }

            if (Input.GetButtonDown("Fire1") && canMove)
            {
                StartCoroutine("HandleAttack");
            }

            if (Input.GetButtonDown("Fire2") && canSwitchFlask)
            {
                StartCoroutine("SwitchFlask");
            }

            if (Input.GetKeyDown(KeyCode.T) && magic >= magicCost && canMove)
            {
                StartCoroutine("HandleMagicAttack");
            }

            if (Input.GetButtonDown("Jump") && !isRolling)
            {
                StartCoroutine("HandleFlask");
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                HandleLockOn();
            }
        }

        if (health <= 0 && isAlive)
        {
            HandleDeath();
        }

    }

    void movePlayer()
    {
        if (canMove)
        {

            xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");

            dir = transform.right * xInput + transform.forward * yInput;
            dir *= currentSpeed;
            dir.y = rb.velocity.y;

            rb.velocity = dir;

        }

    }

    void rotatePlayer()
    {
        if (!lockedOn)
        {
            transform.Rotate(Input.GetAxis("Mouse X") * cameraSpeed * transform.up * Time.deltaTime);

        }
        else if (lockedOn)
        {
            transform.LookAt(lockTarget);
        }
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform tMin = null;
        float minDist = 20;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }

    void HandleLockOn()
    {


        if (!lockedOn && GetClosestEnemy(enemysInScene) != null)
        {
            lockedOn = true;
            lockTarget = GetClosestEnemy(enemysInScene);
        }
        else if (lockedOn)
        {
            lockedOn = false;
            transform.rotation = baseRotation;
        }


    }

    void handleAnimation()
    {
        if (Mathf.Abs(yInput) > Mathf.Abs(xInput))
        {

            if (yInput > 0)
            {
                uncheckAnimations();
                animator.SetBool("isRunning", true);
            }
            else if (yInput < 0)
            {
                uncheckAnimations();
                animator.SetBool("isBackwards", true);
            }

        }
        else if (Mathf.Abs(xInput) >= Mathf.Abs(yInput) && xInput != 0)
        {

            if (xInput > 0)
            {
                uncheckAnimations();
                animator.SetBool("isStrafingR", true);
            }
            else if (xInput < 0)
            {
                uncheckAnimations();
                animator.SetBool("isStrafingL", true);
            }

        }

        else
        {
            uncheckAnimations();
        }
    }

    void uncheckAnimations()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isBackwards", false);
        animator.SetBool("isStrafingL", false);
        animator.SetBool("isStrafingR", false);
    }

    void handleSprint()
    {

        if (Input.GetButton("Fire3") && stamina > 0)
        {
            timeHeld += sprintDelay * Time.deltaTime;

            if (timeHeld > sprintDelay)
            {
                currentSpeed = moveSpeed * sprintMultiplier;
                stamina -= staminaDrain * Time.deltaTime;
            }

        }
        else if (Input.GetButtonUp("Fire3") && timeHeld < 1)
        {
            StartCoroutine("HandleRoll");
        }
        else if (stamina < maxStamina && canMove)
        {
            timeHeld = 0;
            currentSpeed = moveSpeed;
            stamina += staminaRegen * Time.deltaTime;
        }
    }

    IEnumerator HandleRoll()
    {
        if (stamina >= rollCost && canMove)
        {
            Vector3 rollDirection = rb.velocity;
            animator.SetTrigger("Roll");
            rb.velocity = new Vector3(0, 0, 0);
            isRolling = true;
            isDamageable = false;
            canMove = false;
            stamina -= rollCost;
            rb.AddForce(rollDirection * rollSpeed, ForceMode.Impulse);
            playerModel.LookAt(rollDirection + transform.position);

            yield return new WaitForSeconds(rollDelay);

            isRolling = false;
            canMove = true;
            isDamageable = true;

        }

    }

    IEnumerator HandleAttack()
    {
        if (stamina >= attackCost)
        {
            canMove = false;
            animator.SetTrigger("basicSlash");
            stamina -= attackCost;
            weaponScript.CheckHitbox(attackDelay);
            yield return new WaitForSeconds(attackDelay);
            canMove = true;
        }
    }

    IEnumerator HandleMagicAttack()
    {
        canMove = false;
        Vector3 center = transform.position + (transform.forward * magicDistance);

        magic -= magicCost;
        animator.SetTrigger("MagicCast");

        yield return new WaitForSeconds(magicDelay);

        Instantiate(magicEffect, center, Quaternion.identity);

        Collider[] hitColliders = Physics.OverlapSphere(center, magicAOE);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "Enemy")
            {
                if (hitCollider.GetComponent<Enemy>() != null)
                {
                    Enemy enemy = hitCollider.GetComponent<Enemy>();
                    enemy.TakeDamage(magicDamage);
                }
                else if (hitCollider.transform.parent.transform.parent.GetComponent<BossScript>() != null)
                {
                    BossScript hitEnemy = hitCollider.transform.parent.transform.parent.GetComponent<BossScript>();
                    hitEnemy.TakeDamage(magicDamage);
                }
            }
        }
        yield return new WaitForSeconds(attackDelay);
        canMove = true;
    }

    IEnumerator HandleFlask()
    {
        isRolling = true;

        if (remainingRedFlasks >= 1 && redFlask)
        {
            health += 20;
            animator.SetTrigger("Drink");
            remainingRedFlasks--;
            flaskUI.FlaskNumber(redFlask);
        }
        else if (remainingBlueFlasks >= 1 && !redFlask)
        {
            magic += 20;
            animator.SetTrigger("Drink");
            remainingBlueFlasks--;
            flaskUI.FlaskNumber(redFlask);
        }

        yield return new WaitForSeconds(1f);
        isRolling = false;
    }

    IEnumerator SwitchFlask()
    {
        canSwitchFlask = false;

        if (redFlask)
        {
            redFlask = false;
            flaskUI.switchFlask(redFlask);

        }
        else if (!redFlask)
        {
            redFlask = true;
            flaskUI.switchFlask(redFlask);
        }

        yield return new WaitForSeconds(1);
        canSwitchFlask = true;
    }

    public IEnumerator TakeDamage(float damage)
    {
        if (isDamageable)
        {
            isDamageable = false;
            health -= damage;
            yield return new WaitForSeconds(damageFrames);
            isDamageable = true;
        }
    }

    void HandleDeath()
    {
        isAlive = false;
        canMove = false;
        animator.SetTrigger("Death");
    }

    public void setPause()
    {
        if (!isPaused)
        {
            isPaused = true;
        }
        else if (isPaused)
        {
            isPaused = false;
        }
    }
}



