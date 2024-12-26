using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;
    [SerializeField] float returnSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, Quaternion.identity, returnSpeed * Time.deltaTime));
        }
        else
        {
            
            Quaternion deltaRotation = Quaternion.Euler(Input.GetAxis("Vertical") * verticalSpeed * Time.deltaTime, 0, -Input.GetAxis("Horizontal") *     horizontalSpeed * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}
