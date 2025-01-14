using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterScript : MonoBehaviour
{
    [SerializeField] bool canEncounter;


    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<FantasyPlayer>() != null)
        {
            canEncounter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FantasyPlayer>() != null)
        {
            canEncounter = false;
        }
    }
}
