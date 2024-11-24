using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float disappearDelay;
    [SerializeField] float bulletSpeed;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(Despawn());
    }

    
    void HandleCollision(Collision contact)
    {
        if (contact.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            Destroy(contact.gameObject);
        }
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}
