using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    PlayerControllerDuty playerController;
    int ammoAmount;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerControllerDuty>();

        ammoAmount = Random.Range(1, 6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.HandleAmmoPickUp(ammoAmount);
            Destroy(gameObject);
        }
    }
}
