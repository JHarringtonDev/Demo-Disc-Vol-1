using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DutyCamera : MonoBehaviour
{
    PlayerControllerDuty player;
    Object hitTarget;
    [SerializeField] GameObject bulletPrefab;

    private void Start()
    {
        player = FindObjectOfType<PlayerControllerDuty>();
    }

    private void Update()
    {
        TurnCamera();
    }

    float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }

    void TurnCamera()
    {
        float mx = Input.GetAxis("Mouse X") * Time.deltaTime * player.cameraSensitivity;
        float my = Input.GetAxis("Mouse Y") * Time.deltaTime * player.cameraSensitivity;

        Vector3 rot = transform.rotation.eulerAngles + new Vector3(-my, 0f, 0f); //use local if your char is not always oriented Vector3.up
        rot.x = ClampAngle(rot.x, -60f, 60f);

        transform.eulerAngles = rot;
    }

    public void CheckRaycast(LayerMask layerMask)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Destroy(hit.collider.gameObject);
        }
    }

    public void FireProjectile()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
