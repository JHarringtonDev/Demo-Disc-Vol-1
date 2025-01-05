using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarryObject : MonoBehaviour
{
    [SerializeField] int requiredAnts;
    [SerializeField] float antCheckRadius;
    NavMeshAgent agent;
    HomeScript homeScript;
    [SerializeField] bool goingHome;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        homeScript = FindObjectOfType<HomeScript>();
    }

    private void Update()
    {
        if (goingHome)
        {
            agent.SetDestination(homeScript.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AntScript>())
        {
            checkAnts();
        }
    }

    void checkAnts()
    {
        int antsInRange = 0;

        List<GameObject> checkedAnts = new List<GameObject>();

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, antCheckRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<AntScript>() != null)
            {
                checkedAnts.Add(hitCollider.gameObject);
                antsInRange++;
            }
        }

        if (antsInRange >= requiredAnts) 
        { 
            foreach (var ant in checkedAnts)
            {
                ant.GetComponent<AntScript>().SendHome();
            }
            goingHome = true;
        }
    }
}
