using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour
{
    // Prefabs for enemies (Enemy1, Enemy2, Enemy3)
    [SerializeField] private GameObject[] enemyPrefabs; // Array to hold enemy prefabs
    [SerializeField] private float makeTime; // Time interval to spawn enemies
    private float waitTime; // Timer
    [SerializeField] private float maxRadius = 10f; // Maximum radius of the circular area
    [SerializeField] private float minRadius = 3f;  // Minimum radius of the circular area

    // Start is called before the first frame update
    void Start()
    {
        waitTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (waitTime < makeTime)
        {
            waitTime += Time.deltaTime; // Increment timer
        }
        else
        {
            Vector3 spawnPosition = Vector3.zero; // Initialize spawnPosition to avoid unassigned variable error

            // Generate a random angle and distance within the allowed radius range
            float angle = Random.Range(0f, 2f * Mathf.PI);
            float distance = Random.Range(minRadius, maxRadius);

            // Calculate the random position within the circular area
            float randomX = distance * Mathf.Cos(angle);
            float randomZ = distance * Mathf.Sin(angle);

            spawnPosition = new Vector3(randomX, 1, randomZ);

            // Select a random enemy from the array
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);

            // Instantiate the selected enemy at the random position
            Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);

            // Reset timer
            waitTime = 0;
        }
    }
}
