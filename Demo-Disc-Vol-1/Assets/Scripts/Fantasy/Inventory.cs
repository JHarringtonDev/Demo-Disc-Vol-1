using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    int potionsHeld;
    int ethersHeld;
    int elixirsHeld;

    [SerializeField] TextMeshProUGUI potionButton;
    [SerializeField] TextMeshProUGUI etherButton;
    [SerializeField] TextMeshProUGUI elixirButton;

    private void Start()
    {
        potionsHeld = 5;
        ethersHeld = 3;
        elixirsHeld = 1;
    }

    public bool CheckPotionStock()
    {
        if ( potionsHeld == 0)
        {
            return false;
        }
        else
        {
            potionsHeld--;
            return true;
        }
    }

    public bool CheckEtherStock()
    {
        if (ethersHeld == 0)
        {
            return false;
        }
        else
        {
            ethersHeld--;
            return true;
        }
    }
    public bool CheckElixirStock()
    {
        if (elixirsHeld == 0)
        {
            return false;
        }
        else
        {
            elixirsHeld--;
            return true;
        }
    }

    public void ReflectInventory()
    {
        potionButton.text = $"Potions x{potionsHeld}";
        etherButton.text = $"Ethers x{ethersHeld}";
        elixirButton.text = $"Elixir x{elixirsHeld}";
    }
}
