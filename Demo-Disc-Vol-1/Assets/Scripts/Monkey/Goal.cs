using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] string targetScene;
    BallScript ball;

    MeshRenderer tapeRenederer;

    private void Start()
    {
        ball = FindObjectOfType<BallScript>();
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
        tapeRenederer.enabled = false;
        ball.StopBall();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(targetScene);
    }
}
