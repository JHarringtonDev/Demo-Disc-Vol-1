using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] LayerMask clickMask;
    [SerializeField] GameObject player;
    [SerializeField] NavMeshAgent playerAgent;
    [SerializeField] NavMeshAgent antAgent;
    [SerializeField] float returnClickRange;
    AntScript[] loadedAnts;

    private void Start()
    {
        loadedAnts = FindObjectsOfType<AntScript>(); 

        for (int i = 0; i < loadedAnts.Length; i++)
        {
            loadedAnts[i].FollowPlayer();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            antAgent.SetDestination(transform.position);
            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, clickMask))
            {
                clickPosition = hit.point;
                playerAgent.SetDestination(clickPosition);
                Debug.Log("move player to " + clickPosition);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            int antIndex = -1;
            for (int i = 0; i < loadedAnts.Length; i++)
            {
                if (loadedAnts[i].followingPlayer)
                {
                    antIndex = i; break;
                }
            }
            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, clickMask) && antIndex != -1)
            {
                clickPosition = hit.point;
                loadedAnts[antIndex].SendAnt(clickPosition);
                Debug.Log("send ant to " + clickPosition);
            }
        }
        else if (Input.GetMouseButtonDown(2))
        {
            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, clickMask))
            {
                clickPosition = hit.point;
                //Debug.Log("ants in range of " + clickPosition + " return to player");
            }
            Collider[] hitColliders = Physics.OverlapSphere(clickPosition, returnClickRange);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<AntScript>() != null)
                {
                    AntScript ant = hitCollider.GetComponent<AntScript>();
                    ant.FollowPlayer();
                }
            }
        }
    }
}
