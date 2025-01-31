using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    [SerializeField] GameObject actionCover;
    StatManager statManager;
    Inventory inventory;
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
    }

    public void UsePotion()
    {
        if(inventory.CheckPotionStock())
        {
            statManager.RestoreHealth("potion");
        }
    }

    public void UseEther()
    {
        if(inventory.CheckEtherStock())
        {
            statManager.RestoreMagic("ether");
        }
    }

    public void UseElixir()
    {
        if (inventory.CheckElixirStock())
        {
            statManager.RestoreHealth("elixir");
            statManager.RestoreMagic("elixir");
        }
    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
