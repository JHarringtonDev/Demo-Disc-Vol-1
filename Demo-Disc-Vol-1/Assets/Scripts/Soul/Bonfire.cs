using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bonfire : MonoBehaviour
{
    [SerializeField] GameObject interactCanvas;
    [SerializeField] float loadDelay;
    bool playerInRange;

    UIScript ui;
    PlayerController playerController;

    private void Start()
    {
        ui = FindObjectOfType<UIScript>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyUp(KeyCode.E))
        {
            StartCoroutine(ReloadScene());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            interactCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            interactCanvas.SetActive(false);
        }
    }

    IEnumerator ReloadScene()
    {
        interactCanvas.SetActive(false);
        ui.fadeIn = false;
        ui.fadeOut = true;
        playerController.CrouchAnimation();
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
