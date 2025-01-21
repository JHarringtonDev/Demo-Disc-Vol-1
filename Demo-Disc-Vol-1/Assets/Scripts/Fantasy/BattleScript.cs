using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScript : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("attempt escape");
    }
}
