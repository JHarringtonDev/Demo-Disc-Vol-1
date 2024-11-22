using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    BoxSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<BoxSpawner>();
    }
    private void OnDestroy()
    {
        spawner.spawnedBoxes--;
    }
}
