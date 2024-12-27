using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyCamera : MonoBehaviour
{
    [SerializeField] Rigidbody ballRb;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ballRb.velocity);
    }
}
