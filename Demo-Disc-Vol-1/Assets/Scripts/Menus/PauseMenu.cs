using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject menu;
    [SerializeField] TextMeshProUGUI pauseText;
    [SerializeField] GameObject closeButton;

    bool gameIsOver;
    bool paused;
    bool canPause = true;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !gameIsOver && canPause)
        {
            if (!paused)
            {
                OpenMenu();
            }
            else if (paused)
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

    public void gameOver()
    {
        gameIsOver = true;
        OpenMenu();
        pauseText.text = "Game Over";
        closeButton.SetActive(false);
    }

    public void DisablePauseMenu(bool state)
    {
        canPause = state;
    }
}
