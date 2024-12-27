using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField] float rotateSpeed;
    [SerializeField] float destroyDelay;
    [SerializeField] GameObject coinMesh;
    [SerializeField] Transform stage;

    MonkeyManager monkeyManager;

    private void Start()
    {
        monkeyManager = FindObjectOfType<MonkeyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        coinMesh.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(HandleCollection());
        }
    }

    IEnumerator HandleCollection()
    {
        monkeyManager.CollectCoin();
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
