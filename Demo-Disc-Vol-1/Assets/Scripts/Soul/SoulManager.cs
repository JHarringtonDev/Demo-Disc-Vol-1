using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulManager : MonoBehaviour
{
    public bool hallwayActive;
    public bool roomActive;
    public bool bossActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeHallway()
    {
        if (hallwayActive)
        {
            hallwayActive = false;
        }
        else
        {
            hallwayActive = true;
        }
    }
}
