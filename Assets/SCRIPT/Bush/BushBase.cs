using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BushBase : MonoBehaviour
{
    public GameObject BushPrefab; // Prefab of the Bush to spawn
    private GameObject spawnedBush;
    private BushSounds soundHandler;

    void Start()
    {
        soundHandler = GetComponent<BushSounds>();
        if (soundHandler == null)
        {
            Debug.LogError("No SoundHandler found on the BushBase.");
        }

        CheckAndSpawnBush();
    }

    void CheckAndSpawnBush()
    {
        if (spawnedBush == null)
        {
            SpawnBush();
        }
    }

    
    void SpawnBush()
    {
        if (BushPrefab != null)
        {
            Vector3 spawnPosition = transform.position; 
            spawnedBush = Instantiate(BushPrefab, spawnPosition, Quaternion.identity);
            spawnedBush.transform.SetParent(transform);

            spawnPosition = spawnPosition + new Vector3(0, 0.7f, 0);
            spawnedBush.transform.DOMove(spawnPosition,0.4f);
        }
        else
        {
            Debug.LogError("Bush prefab is not assigned.");
        }
    }


    public void OnBushDestroyed()
    {
        if (soundHandler != null)
        {
            soundHandler.Play("BushDown");
        }
        StartCoroutine(SpawnBushAfterDelay(30f));
    }

    IEnumerator SpawnBushAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckAndSpawnBush();
    }
}
