using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject playerModel;
    [SerializeField] float cameraSpeed;
    [SerializeField] float clampAmount;

    private void Update()
    {
        TurnCamera();
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
    
    void TurnCamera()
    {
        float my = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSpeed;

        Vector3 rot = transform.rotation.eulerAngles + new Vector3(-my, 0f, 0f); //use local if your char is not always oriented Vector3.up
        rot.x = ClampAngle(rot.x, -clampAmount, clampAmount);

        transform.eulerAngles = rot;
    }
}
