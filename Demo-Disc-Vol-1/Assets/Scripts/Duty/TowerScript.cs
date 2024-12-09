using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    GameManager gameManager;
    PauseMenu pauseMenu;

    bool canBuy;

    [SerializeField] float towerHealth;
    [SerializeField] float maxHealth;
    [SerializeField] GameObject buyMenu;
    [SerializeField] GameObject storePrompt;
    [SerializeField] Image healthFill;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        towerHealth = maxHealth;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBuy)
        {
            buyMenu.SetActive(true);
        }

        if (towerHealth <= 0)
        {
            pauseMenu.gameOver();
        }

        healthFill.fillAmount = towerHealth / maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBuy = true;
            storePrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBuy = false;
            storePrompt.SetActive(false);
        }
    }

    public void towerDamage(float damageAmount)
    {
        towerHealth -= damageAmount;
    }

    public void healTower(int healthPoint)
    {
        if(towerHealth + healthPoint <= maxHealth) 
        {
            towerHealth += healthPoint;
        }
        else
        {
            towerHealth = maxHealth;
        }
    }
}
