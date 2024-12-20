using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    PlayerControllerDuty duty;
    GameManager gameManager;
    TowerScript tower;
    PauseMenu pauseMenu;

    [SerializeField] int towerHeal;

    [Header("StoreCost")]
    [SerializeField] int shop1Cost;
    [SerializeField] int shop2Cost;
    [SerializeField] int shop3Cost;

    [Header("Shop Text")]
    [SerializeField] TextMeshProUGUI shopItem1;
    [SerializeField] TextMeshProUGUI shopItem2;
    [SerializeField] TextMeshProUGUI shopItem3;
    [SerializeField] TextMeshProUGUI balanceDisplay;

    private void Awake()
    {
        duty = FindObjectOfType<PlayerControllerDuty>();
        gameManager = FindObjectOfType<GameManager>();
        tower = FindObjectOfType<TowerScript>();
        pauseMenu = FindObjectOfType<PauseMenu>();

        shopItem1.text = shopItem1.text + shop1Cost;
        shopItem2.text = shopItem2.text + shop2Cost;
        shopItem3.text = shopItem3.text + shop3Cost;
    }

    void OnEnable()
    {
        gameManager.pauseGame();
        pauseMenu.DisablePauseMenu(false);
        displayBalance();
    }

    public void closeMenu()
    {
        gameManager.unpauseGame();
        pauseMenu.DisablePauseMenu(true);
        gameObject.SetActive(false);
    }

    public void HealTower()
    {
        if(gameManager.GetMoney() >= shop1Cost)
        {
            tower.healTower(towerHeal);
            gameManager.SpendMoney(shop1Cost);
            displayBalance();
        }
    }

    public void BuyAmmo()
    {
        if(gameManager.GetMoney() >= shop2Cost)
        {
            duty.HandleAmmoPickUp(6);
            gameManager.SpendMoney(shop2Cost);
            displayBalance();
        }
    }

    public void BuyGrenade()
    {
        if(gameManager.GetMoney() >= shop3Cost)
        {
            Debug.Log("bought grenade");
            gameManager.SpendMoney(shop3Cost);
            duty.GetGrenade();
            displayBalance();
        }
    }

    void displayBalance()
    {
        balanceDisplay.text = "Current Balance: " + gameManager.GetMoney();
    }
}
