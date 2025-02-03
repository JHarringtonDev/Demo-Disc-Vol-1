using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] GameObject playerMenu;
    [SerializeField] float enemyTurnDuration;
    [SerializeField] GameObject actionMenu;

    bool playerTurn = true;

    StatManager statManager;
    FantasyEnemy enemy;


    public void beginBattle()
    {
        playerTurn = true;
        playerMenu.SetActive(true);
        enemy = FindObjectOfType<FantasyEnemy>();
        statManager = FindObjectOfType<StatManager>();
    }

    public void changeTurn()
    {
        if (playerTurn)
        {
            playerTurn = false;
            StartCoroutine(EnemyTurn());
            actionMenu.SetActive(false);
        }
        else if (!playerTurn)
        {
            playerTurn = true;
            actionMenu.SetActive(true);
        }
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(2);
        enemy.HandleAttack();
        yield return new WaitForSeconds(enemyTurnDuration);
        changeTurn();
    }

}
