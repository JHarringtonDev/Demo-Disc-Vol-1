using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerDuty : MonoBehaviour
{
    DutyCamera playerCamera;

    Rigidbody rb;
    LayerMask layerMask;

    [SerializeField] float moveSpeed;
    public float cameraSensitivity;
    [SerializeField] float minYLook;
    [SerializeField] float maxYLook;
    [SerializeField] bool fireHitscan;
    [SerializeField] GameObject bullet;


    bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Target");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Mathf.Clamp(transform.localRotation.z, 0, 1);

        rb = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<DutyCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        //TurnCameraHorizontal();
        TurnPlayer();

        if (Input.GetButton("Fire1") && canFire)
        {
            StartCoroutine("ShootGun");
        }
    }

    void MovePlayer()
    {
        float xInput;
        float yInput;

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.right * xInput + transform.forward * yInput;

        dir *= moveSpeed;

        transform.position += dir * Time.deltaTime;
        
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

    IEnumerator ShootGun()
    {
        canFire = false;
        if (fireHitscan)
        {
            playerCamera.CheckRaycast(layerMask);
        }
        else if(!fireHitscan)
        {
            playerCamera.FireProjectile();
        }
        yield return new WaitForSeconds(1);
        canFire = true;
    }
     
}
