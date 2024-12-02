using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int money;
    [SerializeField] int startingMoney;

    // Start is called before the first frame update
    void Start()
    {
        money = startingMoney;
    }


}
