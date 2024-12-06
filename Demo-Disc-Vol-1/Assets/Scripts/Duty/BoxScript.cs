using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    BoxSpawner spawner;
    PlayerControllerDuty duty;
    GameManager gameManager;
    TowerScript tower;

    Rigidbody rb;
    bool canDamage = true;

    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] float damageBuffer;
    [SerializeField] bool targetPlayer;
    [SerializeField] float enemyHealth;



    private void Start()
    {
        spawner = FindObjectOfType<BoxSpawner>();
        duty = FindObjectOfType<PlayerControllerDuty>();
        gameManager = FindObjectOfType<GameManager>();
        tower = FindObjectOfType<TowerScript>();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(targetPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, duty.gameObject.transform.position, speed * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(Vector3.MoveTowards(transform.position, tower.gameObject.transform.position,speed * Time.deltaTime));
        }
    }
    private void OnDestroy()
    {
        spawner.spawnedBoxes--;
        gameManager.addMoney();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canDamage)
        {
            if (collision.gameObject.tag == "Player")
            {
                duty.HandleDamage(damage);
                canDamage = false;
                StartCoroutine(ChangeDamageState());
            }
            else if(collision.gameObject.tag == "Tower")
            {
                tower.towerDamage(damage);
                canDamage = false;
                StartCoroutine(ChangeDamageState());
            }
        }
    }


    IEnumerator ChangeDamageState() 
    {
        {
            yield return new WaitForSeconds(damageBuffer);
            canDamage = true;
        } 
    }

    public void takeDamage(float damage)
    {
        enemyHealth -= damage;

        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
