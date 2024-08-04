using System.Collections;
using UnityEngine;
using DG.Tweening;
public class RockBase : MonoBehaviour
{
    public GameObject RockPrefab; // Prefab of the Rock to spawn
    private GameObject spawnedRock;
    private RockSounds soundHandler;

    void Start()
    {
        soundHandler = GetComponent<RockSounds>();
        if (soundHandler == null)
        {
            Debug.LogError("No SoundHandler found on the RockBase.");
        }

        CheckAndSpawnRock();
    }

    void CheckAndSpawnRock()
    {
        if (spawnedRock == null)
        {
            SpawnRock();
        }
    }

    
    void SpawnRock()
    {
        if (RockPrefab != null)
        {
            Vector3 spawnPosition = transform.position; 
            spawnedRock = Instantiate(RockPrefab, spawnPosition, Quaternion.identity);
            spawnedRock.transform.SetParent(transform);

            spawnPosition = spawnPosition + new Vector3(0, 0.7f, 0);
            spawnedRock.transform.DOMove(spawnPosition,0.4f);
        }
        else
        {
            Debug.LogError("Rock prefab is not assigned.");
        }
    }


    public void OnRockDestroyed()
    {
        if (soundHandler != null)
        {
            soundHandler.Play("RockDown");
        }
        StartCoroutine(SpawnRockAfterDelay(30f));
    }

    IEnumerator SpawnRockAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckAndSpawnRock();
    }
}
