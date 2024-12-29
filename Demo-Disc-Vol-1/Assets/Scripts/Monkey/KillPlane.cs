using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlane : MonoBehaviour
{
    MonkeyPlayer monkeyPlayer;
    RotationControl rotationControl;
    // Start is called before the first frame update
    void Start()
    {
        monkeyPlayer = FindObjectOfType<MonkeyPlayer>();
        rotationControl = FindObjectOfType<RotationControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            monkeyPlayer.levelOver = true;
            rotationControl.canControl = false;
            StartCoroutine(EndLevel());
        }
    }

    IEnumerator EndLevel()
    {
        Debug.Log("co");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Monkey");
    }
}
