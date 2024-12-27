using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationControl : MonoBehaviour
{
    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float returnSpeed;

    [SerializeField] GameObject playerBall;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            transform.rotation = (Quaternion.RotateTowards(transform.rotation, Quaternion.identity, returnSpeed * Time.deltaTime));
        }
        else
        {
            transform.RotateAround(playerBall.transform.position, Vector3.back, Input.GetAxis("Horizontal") * horizontalSpeed * Time.deltaTime);
            transform.RotateAround(playerBall.transform.position, Vector3.right, Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime);

        }
    }
}
