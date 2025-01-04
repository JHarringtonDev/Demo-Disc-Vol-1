using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ClickDetection : MonoBehaviour
{
    [SerializeField] LayerMask clickMask;
    [SerializeField] NavMeshAgent playerAgent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
            Vector3 clickPosition = -Vector3.one;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, clickMask))
            {
                clickPosition = hit.point;
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
                Debug.Log("ants in range of " + clickPosition + " return to player");
            }
        }
    }
}
