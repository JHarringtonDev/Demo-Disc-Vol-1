using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject playerModel;
    [SerializeField] float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(playerModel.transform.position, Vector3.up, Input.GetAxis("Mouse X") * cameraSpeed * Time.deltaTime);
    }
}
