using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    GameManager gameManager;

    bool canBuy;

    [SerializeField] float towerHealth;
    [SerializeField] float maxHealth;
    [SerializeField] GameObject buyMenu;
    [SerializeField] Image healthFill;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        towerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canBuy)
        {
            Debug.Log("Purchased");
            //gameManager.SpawnAmmo();
            buyMenu.SetActive(true);
        }

        if (towerHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        healthFill.fillAmount = towerHealth / maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBuy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canBuy = false;
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
