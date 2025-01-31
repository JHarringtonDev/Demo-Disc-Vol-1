using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] List<GameObject> enemies;
    GameObject spawnedEnemy;

    private void OnEnable()
    {
        int spawnRoll = Random.Range(0, enemies.Count);
        Debug.Log($"spawn roll is {spawnRoll}");
        spawnedEnemy = Instantiate(enemies[spawnRoll], spawnTransform);
    }

    private void OnDisable()
    {
        Destroy(spawnedEnemy);
    }
}
