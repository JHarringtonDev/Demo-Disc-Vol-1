using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyPlayer : MonoBehaviour
{
    [SerializeField] GameObject ballObject;
    [SerializeField] float cameraSpeed;
    [SerializeField] float gameTime;
    public bool levelOver;
    Rigidbody playerRB;

    private void Start()
    {
        playerRB = ballObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!levelOver)
        {
            transform.position = ballObject.transform.position;
        }

        if(gameTime <= 3)
        {
            gameTime += Time.deltaTime;
            if(playerRB.velocity == Vector3.zero)
            {
                transform.rotation = Quaternion.identity;
            }
        } 


        if (playerRB.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(playerRB.velocity, Vector3.up);
        }
       
    }
}
