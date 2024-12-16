using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
    BossScript boss;

    [SerializeField] float attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(boss.attacking)
        {
            if(other.GetComponent<PlayerController>() != null)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                player.StartCoroutine("TakeDamage", attackDamage);
                boss.attacking = false;
            }
        }
    }
}
