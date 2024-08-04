using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public int quantity = 1;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inventory = other.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddItem(item, quantity);
                Destroy(gameObject);
            }
        }
    }
}

