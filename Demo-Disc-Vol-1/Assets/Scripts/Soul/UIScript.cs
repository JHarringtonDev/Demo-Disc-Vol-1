using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    [SerializeField] Image healthBar;
    [SerializeField] Image magicBar;
    [SerializeField] Image staminaBar;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
       player = FindObjectOfType<PlayerController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        HandleHud();
    }

    void HandleHud()
    {
        healthBar.fillAmount = player.health/100;
        magicBar.fillAmount = player.magic/100;
        staminaBar.fillAmount= player.stamina/100;
    }
}
