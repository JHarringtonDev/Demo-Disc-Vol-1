using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControllerDuty : MonoBehaviour
{
    DutyCamera playerCamera;

    Rigidbody rb;
    LayerMask layerMask;

    public float cameraSensitivity;


    [SerializeField] float moveSpeed;
    [SerializeField] float sprintMultiplier;
    [SerializeField] float minYLook;
    [SerializeField] float maxYLook;
    [SerializeField] bool fireHitscan;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireDelay;
    [SerializeField] float reloadTime;
    [SerializeField] GameObject GunModel;

    [SerializeField] float maxHealth;
    [SerializeField] float loadedBullets;
    [SerializeField] float heldBullets;

    [SerializeField] Image healthFill;
    [SerializeField] Image ammoFill;
    [SerializeField] TextMeshProUGUI ammoCountDisplay;


    float health;

    bool canFire = true;
    bool canReload = true;
    bool isSprinting;
    bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Target");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        health = maxHealth;

        Mathf.Clamp(transform.localRotation.z, 0, 1);

        rb = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<DutyCamera>();

        displayAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPaused)
        {
            MovePlayer();
            HandleSprint();
            TurnPlayer();

            if (Input.GetButton("Fire1") && canFire)
            {
                StartCoroutine("ShootGun");
            }

            if (Input.GetKeyDown(KeyCode.R) && canReload)
            {
                StartCoroutine(reloadGun());
            }

            if (!canReload && !isSprinting)
            {
                GunModel.transform.localRotation = Quaternion.Slerp(GunModel.transform.localRotation, new Quaternion(6, 0, 0, 1), 0.5f * Time.deltaTime);
            }
            else if (isSprinting)
            {
                GunModel.transform.localRotation = Quaternion.Slerp(GunModel.transform.localRotation, new Quaternion(-1, 0, 0, 1), 1f * Time.deltaTime);
            }
            else
            {
                GunModel.transform.localRotation = Quaternion.Slerp(GunModel.transform.localRotation, new Quaternion(0, 0, 0, 1), 5f * Time.deltaTime);

            }
        }
    }

    void MovePlayer()
    {
        float xInput;
        float yInput;

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * xInput + transform.forward * yInput;

        if (isSprinting)
        {
            dir *= (moveSpeed * sprintMultiplier);
        }
        else
        {
            dir *= moveSpeed;
        }

        rb.MovePosition(transform.position + dir * Time.deltaTime);
        
    }

    void HandleSprint()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            canFire = false;
            canReload = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && isSprinting)
        {
            isSprinting = false;
            canFire = true; 
            canReload = true;
        }
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    void TurnPlayer()
    {
        transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity,0));
    }

    void TurnCamera()
    {
        float mx = Input.GetAxis("Mouse X") * Time.deltaTime * cameraSensitivity;
        float my = Input.GetAxis("Mouse Y") * Time.deltaTime * cameraSensitivity;

        Vector3 rot = transform.rotation.eulerAngles + new Vector3(-my, mx, 0f); //use local if your char is not always oriented Vector3.up
        rot.x = ClampAngle(rot.x, -60f, 60f);

        transform.eulerAngles = rot;
    }

    IEnumerator reloadGun()
    {
        canReload = false;
        canFire = false;

        float bulletsNeeded = 6 - loadedBullets;

        if (heldBullets >= bulletsNeeded) 
        {
            heldBullets -= bulletsNeeded;
            loadedBullets += bulletsNeeded;
        }
        else if(heldBullets < bulletsNeeded)
        {
            loadedBullets += heldBullets;
            heldBullets = 0;
        }

        yield return new WaitForSeconds(reloadTime);
        displayAmmo();
        canReload = true;
        canFire = true;
    }

    IEnumerator ShootGun()
    {
        if(loadedBullets > 0)
        {
            canFire = false;
            loadedBullets--;
            displayAmmo();

            GunModel.transform.localRotation = Quaternion.Slerp(GunModel.transform.localRotation, new Quaternion(-6, 0, 0, 1), 1f * Time.deltaTime);

            if (fireHitscan)
            {
                playerCamera.CheckRaycast(layerMask);
            }
            else if(!fireHitscan)
            {
                playerCamera.FireProjectile();
            }
            yield return new WaitForSeconds(fireDelay);
            canFire = true;
        }
        else
        {
            StartCoroutine(reloadGun());
        }
    }
    
    public void HandleAmmoPickUp(int bulletAmount)
    {
        heldBullets += bulletAmount;
        displayAmmo();
    }

    public void HandleDamage(int damage)
    {
        health -= damage;
        healthFill.fillAmount = health / maxHealth;
        if(health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void displayAmmo()
    {
        ammoFill.fillAmount = loadedBullets / 6;
        ammoCountDisplay.text = "Remaining Bullets:" + heldBullets;
    }

    public void setPause()
    {
        if (!isPaused)
        {
            isPaused = true;
        } 
        else if (isPaused)
        {
            isPaused = false;
        }
    }
}
