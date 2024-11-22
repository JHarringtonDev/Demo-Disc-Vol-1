using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxSpawner : MonoBehaviour
{
    
    bool canSpawn = true;

    public int spawnedBoxes = 0;

    [SerializeField] int maxBoxes;
    [SerializeField] float spawnDelay;
    [SerializeField] GameObject boxPrefab;
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();



    // Update is called once per frame
    void Update()
    {
        if(canSpawn && spawnedBoxes < maxBoxes)
        {
            StartCoroutine("SpawnBoxes");
        }
    }

    IEnumerator SpawnBoxes()
    {
        canSpawn = false;
        int spawnSelect = Mathf.Abs(Random.Range(0, 4));
        spawnedBoxes++;
        Instantiate(boxPrefab, spawnPoints[spawnSelect]);
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }
}
