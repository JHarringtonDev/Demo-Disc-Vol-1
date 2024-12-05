using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxSpawner : MonoBehaviour
{
    
    bool canSpawn = true;

    public int spawnedBoxes = 0;
    int totalSpawned;
    int possibleIndex = 0;

    [SerializeField] int maxBoxes;
    [SerializeField] int enemyTypeThreshold;
    [SerializeField] float spawnDelay;
    [SerializeField] List<GameObject> enemyPrefabs;
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

        spawnedBoxes++;
        totalSpawned++;

        int spawnSelect = Mathf.Abs(Random.Range(0, 4));
        int enemyType = Mathf.Abs(Random.Range(0, possibleIndex));

        possibleIndex = Mathf.Abs(totalSpawned / enemyTypeThreshold);
        Instantiate(enemyPrefabs[enemyType], spawnPoints[spawnSelect]);
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

}
