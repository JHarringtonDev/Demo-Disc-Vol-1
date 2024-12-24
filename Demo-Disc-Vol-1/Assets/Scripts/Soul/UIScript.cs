using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("SF Images")]
    [SerializeField] Image healthBar;
    [SerializeField] Image magicBar;
    [SerializeField] Image staminaBar;
    [SerializeField] Image bossHealthBar;
    [SerializeField] Image fadeImage;

    [Header("Fade Attributes")]
    public bool fadeIn;
    public bool fadeOut;
    [SerializeField] float fadeSpeed;
    Color fadeColor;

    PlayerController player;
    BossScript boss;

    // Start is called before the first frame update
    void Start()
    {
       player = FindObjectOfType<PlayerController>(); 
       boss = FindObjectOfType<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleHud();

        if (fadeIn && fadeImage.color.a > 0)
        {
            FadeInScreen();
        }
        else if (fadeOut && fadeImage.color.a < 1)
        {
            FadeOutScreen();
        }
    }

    void HandleHud()
    {
        healthBar.fillAmount = player.health/100;
        magicBar.fillAmount = player.magic/ 100;
        staminaBar.fillAmount = player.stamina / 100;

        if(bossHealthBar.IsActive())
        {
            bossHealthBar.fillAmount = boss.currentHealth / boss.maxHealth;
        }
    }

    void FadeInScreen()
    { 
        fadeColor = fadeImage.color;
        fadeColor.a -= fadeSpeed * Time.deltaTime;
        fadeImage.color = fadeColor;
    }

    void FadeOutScreen()
    {
        fadeColor = fadeImage.color;
        fadeColor.a += fadeSpeed * Time.deltaTime;
        fadeImage.color = fadeColor;
    }
}
