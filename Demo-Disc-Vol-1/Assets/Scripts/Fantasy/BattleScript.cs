using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScript : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyAnimator;
    [SerializeField] GameObject overWorldScene;

    // Start is called before the first frame update
    private void OnEnable()
    {
        overWorldScene.SetActive(false);
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
        overWorldScene.SetActive(true);
    }
}
