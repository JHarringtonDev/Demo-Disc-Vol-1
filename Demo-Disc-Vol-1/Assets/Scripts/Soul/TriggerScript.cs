using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    SoulManager soulManager;

    // Start is called before the first frame update
    void Start()
    {
        soulManager = FindAnyObjectByType<SoulManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + new Vector3(0,0,4), 4);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.GetComponent<PlayerController>() != null && !soulManager.hallwayActive)
                {
                    Debug.Log("player");
                    soulManager.hallwayActive = true;
                    return;
                }
                else if(soulManager.hallwayActive)
                {
                    Debug.Log("why");
                    soulManager.hallwayActive = false;
                }
            }
        }
    }
}
