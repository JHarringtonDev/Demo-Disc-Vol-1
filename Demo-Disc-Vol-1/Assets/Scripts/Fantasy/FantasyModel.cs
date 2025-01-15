using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasyModel : MonoBehaviour
{
    [SerializeField] Rigidbody playerRB;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerRB.velocity);
    }
}
