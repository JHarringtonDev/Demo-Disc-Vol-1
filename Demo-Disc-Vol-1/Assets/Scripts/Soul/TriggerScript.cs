using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerScript : MonoBehaviour
{
    SoulManager soulManager;

    [SerializeField] BoxCollider bCollider;

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
                Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3(0, 0, 0.5f), bCollider.size);
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
                Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3(0, 0, -0.5f), bCollider.size);
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
                Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3(0.5f, 0, 0), bCollider.size);
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

    private void OnDrawGizmos()
    {
        if (hallwayTrigger)
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, 0.5f), bCollider.size);
        }
        else if (!bossRoom)
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(0, 0, -0.5f), bCollider.size);
        }
        else if (bossRoom)
        {
            Gizmos.DrawWireCube(transform.position + new Vector3(0.5f, 0, 0), bCollider.size);
        }
    }
}
