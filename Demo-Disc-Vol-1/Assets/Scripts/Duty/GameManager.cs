using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int money;

    PlayerControllerDuty duty;

    [SerializeField] int startingMoney;
    [SerializeField] int ammoCost;
    [SerializeField] GameObject ammoBox;

    // Start is called before the first frame update
    void Start()
    {
        duty = FindObjectOfType<PlayerControllerDuty>();
        money = startingMoney;
    }

    public int getMoney()
    {
        return money;
    }

    public void SpawnAmmo()
    {
        if(money >= ammoCost) 
        {
            money -= ammoCost;
            Instantiate(ammoBox, new Vector3(0, 10, 0) + duty.gameObject.transform.position, Quaternion.identity);
        } 
    }
}
