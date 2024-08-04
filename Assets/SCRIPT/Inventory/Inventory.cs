using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]public List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItem(Item item, int quantity)
    {
        foreach (var slot in slots)
        {
            if (slot.item == item)
            {
                slot.quantity += quantity;
                Debug.Log("Added" + quantity + " to: " + item.itemName + " making a total of: " + slot.quantity);
                return;
            }
        }
        slots.Add(new InventorySlot(item, quantity));
        Debug.Log("Added new InvSlot with the item: " + item.itemName + "with a quantity of: " + quantity);
    }

    public void RemoveItem(Item item, int quantity)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == item)
            {
                slots[i].quantity -= quantity;
                if (slots[i].quantity <= 0)
                {
                    slots.RemoveAt(i);
                }
                return;
            }
        }
    }
}
