using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoxSpawner : MonoBehaviour
{
    
    bool canSpawn = true;

    public int spawnedBoxes = 0;
    float totalSpawned;
    float possibleIndex = 0;

    [SerializeField] int maxBoxes;
    [SerializeField] float enemyTypeThreshold;
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

        possibleIndex = totalSpawned / enemyTypeThreshold;
        possibleIndex = Mathf.Clamp(possibleIndex, 0, enemyPrefabs.Count - 1);

        int spawnSelect = Random.Range(0, 4);
        float enemyType =  Mathf.Round(Random.Range(0, possibleIndex));



        Debug.Log(enemyType);

        Instantiate(enemyPrefabs[(int)enemyType], spawnPoints[spawnSelect]);
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

}
