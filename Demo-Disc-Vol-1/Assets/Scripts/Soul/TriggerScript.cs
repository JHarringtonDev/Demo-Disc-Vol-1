using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    SoulManager soulManager;
    [SerializeField] bool hallwayTrigger;
    [SerializeField] bool bossRoom;

    [SerializeField] GameObject fogWall;

    // Start is called before the first frame update
    void Start()
    {
        soulManager = FindAnyObjectByType<SoulManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            if (hallwayTrigger)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 0, 4), 4);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponent<PlayerController>() != null && !soulManager.hallwayActive)
                    {
                        soulManager.hallwayActive = true;
                        return;
                    }
                    else if (soulManager.hallwayActive)
                    {
                        soulManager.hallwayActive = false;
                    }
                }
            }
            else if (!bossRoom)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0, 0, -4), 4);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponent<PlayerController>() != null && !soulManager.roomActive)
                    {
                        soulManager.roomActive = true;
                        return;
                    }
                    else if (soulManager.roomActive)
                    {
                        soulManager.roomActive = false;
                    }
                }
            }
            else if (bossRoom)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(2, 0, 0), 4);
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.GetComponent<PlayerController>() != null && !soulManager.roomActive)
                    {
                        fogWall.SetActive(true);
                        soulManager.bossActive = true;
                        return;
                    }
                }
            }
        }
    }
}
