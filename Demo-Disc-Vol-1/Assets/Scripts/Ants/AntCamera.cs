using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCamera : MonoBehaviour
{
    float cameraRotation;
    bool canRotate = true;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationCap;

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            cameraRotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        }
        else if (Mathf.Floor(cameraRotation) < 0)
        {
            increaseToZero();
        }
        else if (Mathf.Floor(cameraRotation) > 0)
        {
            reduceToZero();
        }
        else if (Mathf.Floor(cameraRotation) == 0)
        {
            canRotate = true;
            cameraRotation = 0;
        }
            float yRotation = Mathf.Clamp(cameraRotation, -rotationCap, rotationCap);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yRotation, transform.localEulerAngles.z);
    }

    public void centerRotation()
    {
        canRotate = false;
        //canRotate = false;
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        //canRotate = true;
    }

    void reduceToZero()
    {
        Debug.Log("Running reduce");
        cameraRotation -= rotationSpeed * 2 * Time.deltaTime;
    }

    void increaseToZero()
    {
        Debug.Log("Running increase");
        cameraRotation += rotationSpeed * 2 * Time.deltaTime;
    }
}
