using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntScript : MonoBehaviour
{
    public bool followingPlayer;

    [SerializeField] bool returningHome;

    Vector3 followTarget;

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
            followTarget = antPlayer. transform.position;
        }
        else if(returningHome)
        {
            followTarget = homeScript.transform.position;
            Debug.Log(homeScript.transform.position);
        }

        antAgent.SetDestination(followTarget);
    }

    public void FollowPlayer()
    {
        returningHome = false;
        followingPlayer = true;
    }

    public void SendAnt(Vector3 antDestination)
    {
        returningHome = false;
        followingPlayer = false;
        
        followTarget = antDestination;

    }
}
