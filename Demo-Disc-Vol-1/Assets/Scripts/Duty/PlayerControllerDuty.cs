using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDuty : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float moveSpeed;
    [SerializeField] float cameraSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Mathf.Clamp(transform.localRotation.z, 0, 1);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        TurnCamera();
    }

    void MovePlayer()
    {
        float xInput;
        float yInput;

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(xInput, rb.velocity.y, yInput) * moveSpeed;

        rb.velocity += dir * Time.deltaTime;
    }

    void TurnCamera()
    {
        transform.Rotate(Input.GetAxis("Mouse X") * cameraSensitivity * transform.up * Time.deltaTime);
        //transform.Rotate(Input.GetAxis("Mouse Y") * -cameraSensitivity * transform.right * Time.deltaTime);
    }
}
