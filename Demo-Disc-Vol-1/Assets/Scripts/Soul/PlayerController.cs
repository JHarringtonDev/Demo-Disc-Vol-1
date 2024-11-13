using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveInput;

    [SerializeField] float moveSpeed;
    [SerializeField] float sprintMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

   void movePlayer()
    {
        rb.velocity = new Vector3(Input.GetAxis("Vertical") * moveSpeed, rb.velocity.y, -Input.GetAxis("Horizontal") * moveSpeed);
    }
}
