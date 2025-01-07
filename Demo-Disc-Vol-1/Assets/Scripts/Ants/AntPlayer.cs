using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AntScript>() != null)
        {
            AntScript triggeredAnt = other.GetComponent<AntScript>();
            triggeredAnt.FollowPlayer();
        }
    }
}
