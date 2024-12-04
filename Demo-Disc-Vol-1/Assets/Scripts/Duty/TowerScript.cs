using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerScript : MonoBehaviour
{
    GameManager gameManager;

    bool canBuy;

    [SerializeField] int towerHealth;
    [SerializeField] int maxHealth;
    [SerializeField] GameObject buyMenu;

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

    public void towerDamage(int damageAmount)
    {
        towerHealth -= damageAmount;
    }
}
