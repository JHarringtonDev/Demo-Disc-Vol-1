using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] string targetScene;
    BallScript ball;
    RotationControl rotationControl;

    MeshRenderer tapeRenederer;

    private void Start()
    {
        ball = FindObjectOfType<BallScript>();
        rotationControl = FindObjectOfType<RotationControl>();
        tapeRenederer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(PlayerWin());
        }
    }

    IEnumerator PlayerWin()
    {
        Debug.Log("player Win");
        rotationControl.canControl = false;
        tapeRenederer.enabled = false;
        ball.StopBall();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(targetScene);
    }
}
