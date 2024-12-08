using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int money;
    int towerHealth;

    PlayerControllerDuty duty;


    [SerializeField] int startingMoney;
    [SerializeField] int maxTowerHealth;
    [SerializeField] int enemyReward;
    [SerializeField] int ammoCost;
    [SerializeField] GameObject ammoBox;

    // Start is called before the first frame update
    void Start()
    {
        duty = FindObjectOfType<PlayerControllerDuty>();

        money = startingMoney;
        towerHealth = maxTowerHealth;
    }

    public int GetMoney()
    {
        return money;
    }

    public void SpawnAmmo()
    {
        if (money >= ammoCost)
        {
            money -= ammoCost;
            Instantiate(ammoBox, new Vector3(0, 10, 0) + duty.gameObject.transform.position, Quaternion.identity);
        }
    }

    public void addMoney()
    {
        money += enemyReward;
    }

    public void SpendMoney(int moneySpent)
    {
        money -= moneySpent;
    }

    public void pauseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        duty.setPause();
    }

    public void unpauseGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        duty.setPause();
    }
}
