using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    PlayerControllerDuty player;
    GameManager gameManager;

    bool paused;

    private void Start()
    {
        player = FindObjectOfType<PlayerControllerDuty>();
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!paused)
            {
                OpenMenu();
            }
            else if(paused)
            {
                closeMenu();
            }
        }
    }

    void OpenMenu()
    {
        paused = true;
        menu.SetActive(true);
        gameManager.pauseGame();
    }

    public void ReloadScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void closeMenu()
    {
        paused = false;
        menu.SetActive(false);
        gameManager.unpauseGame();
    }
}
