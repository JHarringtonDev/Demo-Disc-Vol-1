using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyPlayer : MonoBehaviour
{
    [SerializeField] GameObject ballObject;
    [SerializeField] float cameraSpeed;
    Rigidbody playerRB;

    private void Start()
    {
        playerRB = ballObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ballObject.transform.position;
        transform.rotation = Quaternion.LookRotation(playerRB.velocity, Vector3.up);
    }
}
