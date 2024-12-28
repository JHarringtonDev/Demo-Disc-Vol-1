using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RotationControl : MonoBehaviour
{
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float returnSpeed;
    [SerializeField] float clampedRotation;

    [SerializeField] GameObject playerCamera;
    [SerializeField] Transform cameraDirection;


    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            transform.position = Vector3.zero;
            transform.rotation = (Quaternion.RotateTowards(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime));
        }
        else
        {

            float vertRotation = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;
            float horiRotation = -Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
            Debug.Log(vertRotation);
            

            if(vertRotation > 0)
            {
                if(Mathf.Floor(transform.localEulerAngles.x) <= 20 || Mathf.Floor(transform.localEulerAngles.x) >= 330)
                {
                    transform.RotateAround(Vector3.zero, playerCamera.transform.right, vertRotation);
                }
            }
            else if(vertRotation < 0)
            {
                if(Mathf.Floor(transform.localEulerAngles.x) >= 340 || Mathf.Floor(transform.localEulerAngles.x) <= 30)
                {
                    transform.RotateAround(Vector3.zero, playerCamera.transform.right, vertRotation);
                }
            }

            if (horiRotation > 0)
            {
                if(Mathf.Floor(transform.localEulerAngles.z) <= 20 || Mathf.Floor(transform.localEulerAngles.z) >= 330)
                {
                    transform.RotateAround(Vector3.zero, playerCamera.transform.forward, horiRotation);
                }
            }
            else if (horiRotation < 0)
            {
                if (Mathf.Floor(transform.localEulerAngles.z) >= 340 || Mathf.Floor(transform.localEulerAngles.z) <= 30)
                {
                    transform.RotateAround(Vector3.zero, playerCamera.transform.forward, horiRotation);
                }
            }
        }
    }
}
