using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float maxVelocity;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] GameObject parentContainer;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.z < maxSpeed)
        {
            rb.AddForce(parentContainer.transform.forward * acceleration, ForceMode.Acceleration);
        }
    }
}
