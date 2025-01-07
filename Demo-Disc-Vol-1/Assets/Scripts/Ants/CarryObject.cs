using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class CarryObject : MonoBehaviour
{
    [SerializeField] int requiredAnts;
    [SerializeField] float antCheckRadius;
    [SerializeField] bool goingHome;
    [SerializeField] TextMeshProUGUI requirementDisplay;

    BoxCollider antCheck;
    NavMeshAgent agent;
    HomeScript homeScript;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        homeScript = FindObjectOfType<HomeScript>();
        requirementDisplay.text = "0 / " + requiredAnts;
        antCheck = GetComponent<BoxCollider>();
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

    private void OnTriggerExit(Collider other)
    {
            Invoke("checkAnts", 1f);
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
                if(hitCollider.GetComponent<AntScript>().canChange() && !hitCollider.GetComponent<AntScript>().checkFollow()) 
                { 
                    checkedAnts.Add(hitCollider.gameObject);
                    antsInRange++;
                }
            }
        }

        if (antsInRange >= requiredAnts) 
        { 
            foreach (var ant in checkedAnts)
            {
                ant.GetComponent<AntScript>().SendHome();
            }
            antCheck.enabled = false;
            requirementDisplay.transform.parent.gameObject.SetActive(false);
            goingHome = true;
        }

        requirementDisplay.text = antsInRange + " / " + requiredAnts;
    }
}
