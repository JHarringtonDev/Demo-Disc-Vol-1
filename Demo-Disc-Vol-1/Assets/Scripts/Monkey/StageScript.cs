using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageScript : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Input.GetAxis("Vertical") * rotateSpeed * Time.deltaTime, 0, -Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime);
    }
}
