using System.Collections.Generic;
using UnityEngine;

public class RockMain : MonoBehaviour
{
    public int health = 40;
    private RockSounds soundHandler;
    private RockBase RockBase;

    public GameObject rockItemPrefab;
    void Start()
    {
        soundHandler = GetComponent<RockSounds>();
        if (soundHandler == null)
        {
            Debug.LogError("No SoundHandler found on the Rock prefab.");
        }

        RockBase = GetComponentInParent<RockBase>();
        if (RockBase == null)
        {
            Debug.LogError("No RockBase found on the parent.");
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            if (RockBase != null)
            {
                RockBase.OnRockDestroyed();
            }
            int numberOfRocks = Random.Range(1, 5); 
            DropItem(numberOfRocks);
            Destroy(gameObject);
        }
        else
        {   
            soundHandler.Play("rockhit1");
        }
    }
    void DropItem(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Instantiate(rockItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
