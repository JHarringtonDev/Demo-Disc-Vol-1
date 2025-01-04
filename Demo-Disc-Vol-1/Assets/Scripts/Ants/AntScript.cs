using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntScript : MonoBehaviour
{
    public bool followingPlayer;
    
    NavMeshAgent antAgent;

    AntPlayer antPlayer;

    private void Start()
    {
        antAgent = GetComponent<NavMeshAgent>();
        antPlayer = FindObjectOfType<AntPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer)
        {
            antAgent.SetDestination(antPlayer.transform.position);
        }
    }
}
