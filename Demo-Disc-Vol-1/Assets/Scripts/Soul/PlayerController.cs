using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveInput;

    float xInput;
    float yInput;
    Vector3 dir;

    [SerializeField] float cameraSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float sprintMultiplier;
    [SerializeField] Animator animator;
    private bool isRunning;
    bool isIdling;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        rotatePlayer();
        handleAnimation();
    }

   void movePlayer()
    {
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        dir = transform.right * xInput + transform.forward * yInput;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;

        rb.velocity = dir;


        if(dir != new Vector3(0,0,0))
        {
            Debug.Log(rb.velocity);
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
        else if (Mathf.Abs(xInput) >= Mathf.Abs(yInput))
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
}

