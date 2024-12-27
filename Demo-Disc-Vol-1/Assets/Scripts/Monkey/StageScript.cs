using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    [SerializeField] Transform stageRotation;

    float rotX;
    float rotY;
    float rotZ;
    [SerializeField] float clampedRotation;

    private void Update()
    {
        rotX = Mathf.Clamp(stageRotation.rotation.x, -clampedRotation, clampedRotation);
        rotY = Mathf.Clamp(stageRotation.rotation.y, -clampedRotation, clampedRotation);
        rotZ = Mathf.Clamp(stageRotation.rotation.z, -clampedRotation, clampedRotation);

        transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);
    }


}
