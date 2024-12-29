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

    [SerializeField] Transform cameraDirection;

    public bool canControl = true;


    private void Update()
    {
        Debug.Log(cameraDirection.transform.forward);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.zero;

        if((Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) || !canControl)
        {
            transform.rotation = (Quaternion.RotateTowards(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime));
        }
        else if(cameraDirection.forward.z > cameraDirection.forward.x)
        {
            transform.localEulerAngles =
                new Vector3(Input.GetAxis("Vertical") * 20, transform.localEulerAngles.y, -Input.GetAxis("Horizontal") * 20)
              ;
        }
        else
        {
            transform.localEulerAngles =
            new Vector3(-Input.GetAxis("Vertical") * 20, transform.localEulerAngles.y, Input.GetAxis("Horizontal") * 20)
            ;
        }
        

  



        //else
        //{
        //    float vertRotation = Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime;
        //    float horiRotation = -Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime;
        //    
        //    if(vertRotation > 0)
        //    {
        //        if(Mathf.Floor(transform.localEulerAngles.x) <= 20 || Mathf.Floor(transform.localEulerAngles.x) >= 330)
        //        {
        //            transform.RotateAround(Vector3.zero, playerCamera.transform.right, vertRotation);
        //        }
        //    }
        //    else if(vertRotation < 0)
        //    {
        //        if(Mathf.Floor(transform.localEulerAngles.x) >= 340 || Mathf.Floor(transform.localEulerAngles.x) <= 30)
        //        {
        //            transform.RotateAround(Vector3.zero, playerCamera.transform.right, vertRotation);
        //        }
        //    }
        //    
        //    if (horiRotation > 0)
        //    {
        //        if(Mathf.Floor(transform.localEulerAngles.z) <= 20 || Mathf.Floor(transform.localEulerAngles.z) >= 330)
        //        {
        //            transform.RotateAround(Vector3.zero, playerCamera.transform.forward, horiRotation);
        //        }
        //    }
        //    else if (horiRotation < 0)
        //    {
        //        if (Mathf.Floor(transform.localEulerAngles.z) >= 340 || Mathf.Floor(transform.localEulerAngles.z) <= 30)
        //        {
        //            transform.RotateAround(Vector3.zero, playerCamera.transform.forward, horiRotation);
        //        }
        //    }
        //}
        }
    }

