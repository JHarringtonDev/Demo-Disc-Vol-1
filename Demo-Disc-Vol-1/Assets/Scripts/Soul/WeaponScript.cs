using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] CapsuleCollider weaponHitbox;

    bool hitboxActive;
    float attackTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckHitbox(float timeActive)
    {
        attackTime = timeActive;
        StartCoroutine("ActivateHitbox");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitboxActive && other.tag == "Enemy")
        {
            Debug.Log("target hit");
            Destroy(other.gameObject);
        }
    }

    IEnumerator ActivateHitbox()
    {
        hitboxActive = true;

        yield return new WaitForSeconds(attackTime);

        hitboxActive = false;
    }

}
