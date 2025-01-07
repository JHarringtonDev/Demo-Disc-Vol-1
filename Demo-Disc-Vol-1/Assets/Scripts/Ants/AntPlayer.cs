using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AntScript>() != null)
        {
            Debug.Log("Ant Detected");
            if(other.GetComponent<AntScript>().checkReturning()) 
            {
                Debug.Log("returnChecked");
                AntScript triggeredAnt = other.GetComponent<AntScript>();
                triggeredAnt.FollowPlayer();
            }
        }
    }
}
