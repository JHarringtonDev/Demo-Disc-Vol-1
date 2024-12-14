using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    bool hitboxActive;
    float attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(hitboxActive)
        {
            if(other.GetComponent<PlayerController>() != null)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.TakeDamage(attackDamage);
            }
        }
    }
}
