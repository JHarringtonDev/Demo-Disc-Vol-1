using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    PlayerController player;
    public bool hitBoxActive;
    [SerializeField] float weaponDamage;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null && hitBoxActive)
        {
            StartCoroutine(player.TakeDamage(weaponDamage));
        }
    }
}
