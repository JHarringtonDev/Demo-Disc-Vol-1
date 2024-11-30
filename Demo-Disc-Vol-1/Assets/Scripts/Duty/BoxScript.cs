using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    BoxSpawner spawner;
    PlayerControllerDuty duty;
    Rigidbody rb;

    [SerializeField] float speed;
    [SerializeField] int damage;


    private void Start()
    {
        spawner = FindObjectOfType<BoxSpawner>();
        duty = FindObjectOfType<PlayerControllerDuty>();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position,duty.gameObject.transform.position,speed * Time.deltaTime));
    }
    private void OnDestroy()
    {
        spawner.spawnedBoxes--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            duty.HandleDamage(damage);
        }
    }
}
