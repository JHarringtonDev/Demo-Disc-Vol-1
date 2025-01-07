using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public class AntScript : MonoBehaviour
{

    [SerializeField] bool returningHome;

    [SerializeField] float moveSpeed;

    [SerializeField] bool followingPlayer;

    bool canChangePath = true;

    [SerializeField] bool returningToPlayer;

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
        if (followingPlayer || returningToPlayer)
        {
            followTarget = antPlayer.transform.position;
        }
        else if(returningHome)
        {
            followTarget = new Vector3(44,0,0);
        }

        antAgent.SetDestination(followTarget);
    }

    public void FollowPlayer()
    {
        returningHome = false;
        followingPlayer = true;
        returningToPlayer = false;
        canChangePath = true;
        followTarget = antPlayer.transform.position;
    }

    public void SendAnt(Vector3 antDestination)
    {
        returningHome = false;
        followingPlayer = false;
        
        followTarget = antDestination;
    }

    public void ReturnToPlayer()
    {
        Debug.Log(gameObject.name + " returning to player");
        returningHome = false;
        followingPlayer = false;
        returningToPlayer = true;
        canChangePath = false;
    }
    public void SendHome()
    {
        returningHome = true;
        followingPlayer = false;

        canChangePath = false;
    }

    public bool canChange()
    {
        return canChangePath;
    }

    public bool checkReturning()
    {
        return returningToPlayer;
    }

    public bool checkFollow()
    {
        return followingPlayer;
    }
}
