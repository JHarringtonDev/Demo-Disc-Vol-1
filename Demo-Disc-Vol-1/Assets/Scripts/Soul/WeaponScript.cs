using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] CapsuleCollider weaponHitbox;

    bool hitboxActive;
    float attackTime;

    [SerializeField] float weaponDamage;

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
            if (other.GetComponent<Enemy>() != null)
            {
                Enemy hitEnemy = other.GetComponent<Enemy>();
                hitEnemy.TakeDamage(weaponDamage);
                hitboxActive = false;
            }
            else if(other.GetComponent<BossScript>() != null)
            {
                BossScript hitEnemy = other.GetComponent<BossScript>();
                hitEnemy.TakeDamage(weaponDamage);
                hitboxActive = false;
            }
        }
    }

    IEnumerator ActivateHitbox()
    {
        hitboxActive = true;

        yield return new WaitForSeconds(attackTime);

        hitboxActive = false;
    }

}
