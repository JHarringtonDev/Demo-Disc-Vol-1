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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Target"))
        {
            if(collision.gameObject.GetComponent<BoxScript>() != null)
            {
                BoxScript box = collision.gameObject.GetComponent<BoxScript>();
                box.takeDamage();
            }
        }
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(disappearDelay);
        Destroy(gameObject);
    }
}
