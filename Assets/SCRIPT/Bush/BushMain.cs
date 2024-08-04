using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushMain : MonoBehaviour
{
    public int health = 40;
    private BushSounds soundHandler;
    private BushBase BushBase;

    public GameObject BushItemPrefab;
    void Start()
    {
        soundHandler = GetComponent<BushSounds>();
        if (soundHandler == null)
        {
            Debug.LogError("No SoundHandler found on the Bush prefab.");
        }

        BushBase = GetComponentInParent<BushBase>();
        if (BushBase == null)
        {
            Debug.LogError("No BushBase found on the parent.");
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            if (BushBase != null)
            {
                BushBase.OnBushDestroyed();
            }
            int numberOfBushs = Random.Range(1, 5); 
            DropItem(numberOfBushs);
            Destroy(gameObject);
        }
        else
        {   
            soundHandler.Play("Bushhit1");
        }
    }
    void DropItem(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            Instantiate(BushItemPrefab, transform.position, Quaternion.identity);
        }
    }
}
