using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FantasyPlayer : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Animator playerAnimator;

    Vector3 playerInput;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position + new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

        playerInput = new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);

        rb.MovePosition(transform.position + playerInput);

        transform.LookAt(transform.position + playerInput);

        if(playerInput != Vector3.zero)
        {
            playerAnimator.SetBool("isRunning", true);
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
    }

    public Vector3 GetPlayerInput()
    {
        return playerInput;
    }
}
