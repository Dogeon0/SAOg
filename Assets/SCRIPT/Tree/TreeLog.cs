using System.Collections.Generic;
using UnityEngine;

public class TreeLog : MonoBehaviour
{
    public int health = 100;
    private TreeSounds soundHandler;
    private TreeBase treeBase;

    // List of sound keys for different hit sounds
    public List<string> hitSoundKeys = new List<string>()
    {
        "treehit1",
        "treehit2",
        "treehit3"
    };

    public GameObject treeItemPrefab; // Prefab for the log item to be dropped
    public float dropHeight = 1.0f; // Height above the log to spawn the item
    public float dropForce = 2.0f; // Force to apply to the dropped items

    void Start()
    {
        soundHandler = GetComponent<TreeSounds>();
        if (soundHandler == null)
        {
            Debug.LogError("No SoundHandler found on the tree prefab.");
        }

        treeBase = GetComponentInParent<TreeBase>();
        if (treeBase == null)
        {
            Debug.LogError("No TreeBase found on the parent.");
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            if (treeBase != null)
            {
                treeBase.OnTreeDestroyed();
            }
            int numberOfTree = Random.Range(1, 5); // Random number between 1 and 4
            DropItem(numberOfTree); // Drop log items
            Destroy(gameObject);
        }
        else
        {
            soundHandler.PlayRandom(hitSoundKeys);
        }
    }

    void DropItem(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            // Calculate a random position slightly above the tree log
            Vector3 dropPosition = transform.position + new Vector3(0, dropHeight, 0);

            // Instantiate the log item at the calculated position
            GameObject droppedItem = Instantiate(treeItemPrefab, dropPosition, Quaternion.identity);

            // Add a random force to the item's Rigidbody
            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    1f, // Ensure some upward force
                    Random.Range(-1f, 1f)
                ).normalized;
                rb.AddForce(randomDirection * dropForce, ForceMode.Impulse);
            }
        }
    }

    public void SetHealth(int newH)
    {
        health = newH;
    }
}
