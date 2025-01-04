using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntScript : MonoBehaviour
{
    bool followingPlayer;

    bool returningHome;

    HomeScript homeScript;

    NavMeshAgent antAgent;

    AntPlayer antPlayer;

    private void Start()
    {
        antAgent = GetComponent<NavMeshAgent>();
        antPlayer = FindObjectOfType<AntPlayer>();
        homeScript = FindObjectOfType<HomeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (followingPlayer)
        {
            antAgent.SetDestination(antPlayer.transform.position);
        }
        else if(returningHome)
        {
            antAgent.SetDestination(homeScript.transform.position);
        }
    }

    public void FollowPlayer()
    {
        returningHome = false;
        followingPlayer = true;
    }

    public void ReturnHome()
    {
        returningHome = true;
        followingPlayer = false;
        Debug.Log("destination set home");
    }
}
