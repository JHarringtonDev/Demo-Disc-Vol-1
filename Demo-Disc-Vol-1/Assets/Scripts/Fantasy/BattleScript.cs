using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour
{
    [SerializeField] float exitTime;

    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject overWorldScene;

    [SerializeField] TextMeshProUGUI playerNameLvl;

    StatManager stats;

    // Start is called before the first frame update
    private void Awake()
    {
        stats = FindObjectOfType<StatManager>();
    }

    private void OnEnable()
    {
        overWorldScene.SetActive(false);
        playerNameLvl.text = $"Player Lvl. {stats.GetLevel()}";
    }

    public void Attack()
    {
        Debug.Log("player melee");
        playerAnimator.SetTrigger("Attack");
    }

    public void Magic()
    {
        Debug.Log("magic cast");
    }

    public void Item()
    {
        Debug.Log("open items");
    }

    public void Run() 
    {
        StartCoroutine(ExitBattle());
    }

    IEnumerator ExitBattle()
    {
        yield return new WaitForSeconds(exitTime);
        overWorldScene.SetActive(true);
    }
}
