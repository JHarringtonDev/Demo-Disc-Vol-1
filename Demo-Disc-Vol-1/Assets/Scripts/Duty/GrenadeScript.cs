using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    [SerializeField] float rangeRadius;
    [SerializeField] float explosionDelay;
    [SerializeField] float grenadeDamage;
    [SerializeField] float throwStrength;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckBlastRange());
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * throwStrength,ForceMode.Impulse);
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<BoxScript>() != null)
            {
                BoxScript box = hitCollider.GetComponent<BoxScript>();
                box.takeDamage(grenadeDamage);
            }
            else if(hitCollider.GetComponent<PlayerControllerDuty>() != null)
            {
                PlayerControllerDuty player = hitCollider.GetComponent<PlayerControllerDuty>();
                player.HandleDamage(grenadeDamage);
            }
        }
    }

    IEnumerator CheckBlastRange()
    {
        yield return new WaitForSeconds(explosionDelay);
        ExplosionDamage(transform.position, rangeRadius);
        Destroy(gameObject);
    }

}
