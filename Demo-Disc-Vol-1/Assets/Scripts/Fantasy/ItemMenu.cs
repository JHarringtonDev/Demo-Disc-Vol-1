using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] GameObject actionCover;
    StatManager statManager;
    Inventory inventory;
    TurnSystem turnSystem;
    private void OnEnable()
    {
        actionCover.SetActive(true);
        inventory.ReflectInventory();
    }

    private void OnDisable()
    {
        actionCover.SetActive(false);
    }

    private void Awake()
    {
        statManager = FindObjectOfType<StatManager>();
        inventory = FindObjectOfType<Inventory>();
        turnSystem = FindObjectOfType<TurnSystem>();
    }

    public void UsePotion()
    {
        if(inventory.CheckPotionStock())
        {
            statManager.RestoreHealth("potion");
            turnSystem.changeTurn();
            gameObject.SetActive(false);
        }
    }

    public void UseEther()
    {
        if(inventory.CheckEtherStock())
        {
            statManager.RestoreMagic("ether");
            turnSystem.changeTurn();
            gameObject.SetActive(false);
        }
    }

    public void UseElixir()
    {
        if (inventory.CheckElixirStock())
        {
            statManager.RestoreHealth("elixir");
            statManager.RestoreMagic("elixir");
            turnSystem.changeTurn();
            gameObject.SetActive(false);
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
