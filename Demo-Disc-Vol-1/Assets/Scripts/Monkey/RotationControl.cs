using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotationControl : MonoBehaviour
{
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float returnSpeed;

    [SerializeField] GameObject playerCamera;
    [SerializeField] Transform cameraDirection;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            transform.rotation = (Quaternion.RotateTowards(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime));
        }
        else
        {
            transform.RotateAround(Vector3.zero, playerCamera.transform.right, Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime);
            transform.RotateAround(Vector3.zero, playerCamera.transform.forward, -Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime);
        }
    }
}
