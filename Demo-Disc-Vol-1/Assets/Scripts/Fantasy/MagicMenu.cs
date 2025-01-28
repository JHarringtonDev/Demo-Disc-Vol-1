using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMenu : MonoBehaviour
{
    [SerializeField] GameObject actionCover;
    FantasyEnemy enemy;

    private void OnEnable()
    {
        actionCover.SetActive(true);
        enemy = FindObjectOfType<FantasyEnemy>();
    }

    private void OnDisable()
    {
        actionCover.SetActive(false);
    }

    public void CastFire()
    {
        enemy.TakeDamage(5, "fire");
    }

    public void CastIce()
    {
        enemy.TakeDamage(5, "ice");

    }

    public void CastLightning()
    {
        enemy.TakeDamage(5, "lightning");

    }

    public void Back()
    {
        gameObject.SetActive(false);
    }
}
