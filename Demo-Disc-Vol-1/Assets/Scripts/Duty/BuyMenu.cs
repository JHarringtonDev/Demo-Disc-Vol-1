using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    PlayerControllerDuty duty;

    private void Awake()
    {
        duty = FindObjectOfType<PlayerControllerDuty>();
    }

    void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        duty.setPause();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closeMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        duty.setPause();
    }
}
