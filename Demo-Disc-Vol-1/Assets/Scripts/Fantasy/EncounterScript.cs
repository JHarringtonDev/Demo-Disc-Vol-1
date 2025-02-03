using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterScript : MonoBehaviour
{
    [SerializeField] bool canEncounter;
    [SerializeField] float encounterDelay;
    [SerializeField] float encounterRate;

    [SerializeField] float encounterTime;
    [SerializeField] GameObject battleScene;

    [SerializeField] FantasyPlayer playerController;

    TurnSystem turnSystem;

    bool rollingEncounter;

    private void OnEnable()
    {
        battleScene.SetActive(false);
        playerController.ExitBattle();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        playerController = FindObjectOfType<FantasyPlayer>();
        turnSystem = FindObjectOfType<TurnSystem>();
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
            StartCoroutine(activateEncounter());
        }

        yield return new WaitForSeconds(encounterDelay);
        rollingEncounter = false;
    }

    IEnumerator activateEncounter()
    {
        canEncounter = false;
        Debug.Log("encounter begun");
        playerController.EnterBattle();
        yield return new WaitForSeconds(encounterTime);
        battleScene.SetActive(true);
        turnSystem.beginBattle();
    }
}
