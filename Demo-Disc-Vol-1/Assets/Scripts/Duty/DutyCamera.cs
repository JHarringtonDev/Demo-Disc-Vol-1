using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DutyCamera : MonoBehaviour
{

    [SerializeField] float cameraSensitivity;
    private void Update()
    {
        TurnCamera();
    }

    void TurnCamera()
    {
        transform.Rotate(Input.GetAxis("Mouse X") * cameraSensitivity * transform.up * Time.deltaTime);
    }
}
