using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterScript : MonoBehaviour
{
    [SerializeField] bool canEncounter;
    [SerializeField] float encounterDelay;
    [SerializeField] float encounterRate;


    FantasyPlayer playerController;

    bool rollingEncounter;

    private void Start()
    {
        playerController = FindObjectOfType<FantasyPlayer>();
    }

    private void Update()
    {
        if (canEncounter && !rollingEncounter && playerController.GetPlayerInput() != Vector3.zero)
        {
            StartCoroutine(checkEncounter());
        }
    }

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

    IEnumerator checkEncounter()
    {
        rollingEncounter = true;
        float encounterRoll = Random.Range(0f, 1f);
        Debug.Log(encounterRoll);
        if(encounterRoll <= encounterRate)
        {
            activateEncounter();
        }

        yield return new WaitForSeconds(encounterDelay);
        rollingEncounter = false;
    }

    void activateEncounter()
    {
        Debug.Log("encounter begun");
    }
}
