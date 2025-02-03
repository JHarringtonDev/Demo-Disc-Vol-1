using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{
    [SerializeField] float exitTime;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject overWorldScene;
    [SerializeField] GameObject magicMenu;
    [SerializeField] GameObject itemMenu;
    [SerializeField] GameObject actionMenu;

    [Header("UI Features")]
    [SerializeField] TextMeshProUGUI playerNameLvl;
    [SerializeField] TextMeshProUGUI HPNumber;
    [SerializeField] TextMeshProUGUI MPNumber;
    [SerializeField] Image HPBar;
    [SerializeField] Image MPBar;

    [HideInInspector]
    public float maxHP;
    public float maxMP;
    public float currentHP;
    public float currentMP;

    StatManager stats;
    FantasyEnemy enemy;
    TurnSystem turnSystem;

    // Start is called before the first frame update
    private void Awake()
    {
        stats = FindObjectOfType<StatManager>();
        turnSystem = FindObjectOfType<TurnSystem>();
    }

    private void OnEnable()
    {
        overWorldScene.SetActive(false);
        stats.UpdateUIValues();

        Invoke("FindCurrentEnemy", 0.5f);
    }

    public void Attack()
    {
        playerAnimator.SetTrigger("Attack");
        enemy.TakeDamage(3,"phys");
        turnSystem.changeTurn();
        actionMenu.SetActive(false);
    }

    public void Magic()
    {
        magicMenu.SetActive(true);
    }

    public void Item()
    {
        itemMenu.SetActive(true);
    }

    public void Run() 
    {
        StartCoroutine(ExitBattle());
    }

    public IEnumerator ExitBattle()
    {
        yield return new WaitForSeconds(exitTime);
        overWorldScene.SetActive(true);
    }

    void FindCurrentEnemy()
    {
        enemy = FindObjectOfType<FantasyEnemy>();
    }

    public void UpdateUI()
    {
        playerNameLvl.text = $"Player Lvl. {stats.GetLevel()}";
        HPNumber.text = $"{currentHP}";
        MPNumber.text = $"{currentMP}";
        HPBar.fillAmount =  currentHP / maxHP ; 
        MPBar.fillAmount = currentMP / maxMP; 

        if(currentHP <= maxHP / 3)
        {
            HPBar.color = Color.red;
        }
        else if(currentHP <= maxHP / 2)
        {
            HPBar.color = Color.yellow;
        }
        else 
        { 
            HPBar.color = Color.green;
        }
    }
}
