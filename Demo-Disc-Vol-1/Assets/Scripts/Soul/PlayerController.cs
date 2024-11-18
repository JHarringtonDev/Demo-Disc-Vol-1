using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveInput;
    WeaponScript weaponScript;

    float xInput;
    float yInput;

    int remainingFlasks;

    bool canMove = true;
    bool isRolling;
    bool isDamageable = true;
    bool isAlive = true;

     public float health;
    [HideInInspector] public float magic;
    [HideInInspector] public float stamina;

    [Header("Attribute Settings")]
    public float maxHealth;
    public float maxMagic;
    public float maxStamina;

    [SerializeField] float staminaDrain;
    [SerializeField] float staminaRegen;
    [SerializeField] float damageFrames;

    float currentSpeed;
    Vector3 dir;

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

    [Header("Serialized Objects")]
    [SerializeField] Animator animator;
    [SerializeField] Transform playerModel;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();

        weaponScript = FindObjectOfType<WeaponScript>();
        

        health = maxHealth;
        magic = maxMagic;
        stamina = maxStamina;
        currentSpeed = moveSpeed;
        remainingFlasks = 3;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        magic = Mathf.Clamp(magic, 0, maxMagic);
        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        if(isAlive)
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

            if (Input.GetButtonDown("Jump") && !isRolling)
            {
                StartCoroutine("HandleHeal");
            }

            if (isDamageable)
            {
                health -= 5 * Time.deltaTime;
            }

        }

        if(health <= 0 && isAlive)
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
         transform.Rotate(Input.GetAxis("Mouse X") * cameraSpeed * transform.up * Time.deltaTime);
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
        if(stamina >= attackCost)
        {
            canMove = false;
            animator.SetTrigger("basicSlash");
            stamina -= attackCost;
            weaponScript.CheckHitbox(attackDelay);
            yield return new WaitForSeconds(attackDelay);
            canMove = true;
        }
     }

    IEnumerator HandleHeal()
    {
        if(remainingFlasks >= 1)
        {
            isRolling = true;
            health += 20;
            animator.SetTrigger("Drink");
            remainingFlasks--;
            yield return new WaitForSeconds(1f);
            isRolling = false;
        }
    }

    public IEnumerator TakeDamage(float damage)
    {
        if(isDamageable)
        {
            isDamageable = false;
            health-= damage;
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

    }


