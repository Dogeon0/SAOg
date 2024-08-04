using UnityEngine;
using System.Collections.Generic; // Added for List<>

public class PrefabSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array of prefabs to instantiate
    public int numberOfPrefabs; // Number of prefabs to instantiate
    public float minHeight; // Minimum height to spawn prefabs
    public float maxHeight; // Maximum height to spawn prefabs
    public float minSpawnDistance = 5f; // Minimum distance between spawns

    private void Start()
    {
        // Ensure the prefabs array is not empty
        if (prefabs == null || prefabs.Length == 0)
        {
            Debug.LogError("Prefab array is empty!");
            return;
        }

        // Get the bounds of the collider
        Collider collider = GetComponent<Collider>();
        Vector3 center = collider.bounds.center;
        Vector3 size = collider.bounds.size;

        // Create an empty parent object for the spawned prefabs
        GameObject parentObject = new GameObject("SpawnedPrefabs");
        parentObject.transform.parent = transform; // Make it a child of the spawner

        // List to keep track of spawned positions
        List<Vector3> spawnedPositions = new List<Vector3>();

        // Instantiate prefabs randomly within the collider bounds
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            Vector3 spawnPosition = Vector3.zero;
            bool positionFound = false;

            // Attempt to find a valid spawn position
            while (!positionFound)
            {
                // Random position within the collider bounds
                spawnPosition = new Vector3(
                    Random.Range(center.x - size.x / 2, center.x + size.x / 2),
                    Random.Range(center.y, center.y + size.y / 2),
                    Random.Range(center.z - size.z / 2, center.z + size.z / 2)
                );

                // Check minimum distance from previously spawned positions
                positionFound = true;
                foreach (Vector3 pos in spawnedPositions)
                {
                    if (Vector3.Distance(spawnPosition, pos) < minSpawnDistance)
                    {
                        positionFound = false;
                        break;
                    }
                }
            }

            // Add the newly spawned position to the list
            spawnedPositions.Add(spawnPosition);

            // Random height within specified range
            float spawnHeight = Random.Range(minHeight, maxHeight);

            // Randomly choose a prefab from the array
            GameObject prefabToInstantiate = prefabs[Random.Range(0, prefabs.Length)];

            // Instantiate the prefab at the calculated position and height
            GameObject spawnedPrefab = Instantiate(prefabToInstantiate, new Vector3(spawnPosition.x, spawnHeight, spawnPosition.z), Quaternion.identity);

            // Parent the spawned prefab under the parentObject
            spawnedPrefab.transform.parent = parentObject.transform;
        }
    }
}
