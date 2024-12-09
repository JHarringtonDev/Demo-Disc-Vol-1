using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int money;
    

    PlayerController soulController;
    PlayerControllerDuty dutyController;

    [SerializeField] int startingMoney;
    [SerializeField] int maxTowerHealth;
    [SerializeField] int enemyReward;
    [SerializeField] int ammoCost;
    [SerializeField] GameObject ammoBox;

    // Start is called before the first frame update
    void Start()
    {
        soulController = FindObjectOfType<PlayerController>();
        dutyController = FindObjectOfType<PlayerControllerDuty>();

        money = startingMoney;
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
            Instantiate(ammoBox, new Vector3(0, 10, 0) + dutyController.gameObject.transform.position, Quaternion.identity);
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
            if(soulController != null)
            {
                soulController.setPause();
            }
            else if(dutyController != null)
            {
                dutyController.setPause();
            }
        
    }

    public void unpauseGame()
    {
       
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            if (soulController != null)
            {
                soulController.setPause();
            }
            else if (dutyController != null)
            {
                dutyController.setPause();
            }
        
    }
}
