using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntCamera : MonoBehaviour
{
    float cameraRotation;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationCap;

    // Update is called once per frame
    void Update()
    {
        cameraRotation += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        float yRotation = Mathf.Clamp(cameraRotation, -rotationCap, rotationCap);

        transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, yRotation, transform.localEulerAngles.z);
    }

    public void centerRotation()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
    }
}
