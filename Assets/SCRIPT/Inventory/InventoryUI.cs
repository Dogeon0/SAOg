using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel; // The panel containing the inventory UI
    public GameObject slotPrefab; // The prefab for individual inventory slots
    public Inventory inventory; // Reference to the Inventory script
    public GameObject contextMenu; // The context menu UI
    public Button throwButton; // The "Throw" button
    public Button editButton; // The "Edit" button

    private List<GameObject> slots = new List<GameObject>(); // List to keep track of slot GameObjects
    private InventorySlot selectedSlot; // The currently selected inventory slot

    void OnEnable()
    {
        if (inventory != null)
        {
            UpdateUI(); // Update the UI when the inventory UI becomes active
        }
        else
        {
            Debug.LogWarning("Inventory reference is null in InventoryUI!");
        }
    }

    public void UpdateUI()
    {
        // Clear existing slots
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject); // Destroy each child of the inventory panel
        }
        slots.Clear(); // Clear the list of slot GameObjects

        // Create new slots based on the inventory
        foreach (var slot in inventory.slots)
        {
            GameObject slotObj = Instantiate(slotPrefab, inventoryPanel.transform);
            if (slotObj == null)
            {
                Debug.LogError("slotObj is null!");
                continue;
            }

            Transform iconTransform = slotObj.transform.Find("Icon");
            Image icon = iconTransform.GetComponent<Image>();
            Transform quantityTransform = slotObj.transform.Find("Quantity");
            TextMeshProUGUI quantity = quantityTransform.GetComponent<TextMeshProUGUI>();

            icon.sprite = slot.item.icon; // Set the icon sprite
            quantity.text = slot.quantity.ToString(); // Set the quantity text
            slots.Add(slotObj); // Add the slot GameObject to the list

            // Add right-click event using EventTrigger
            EventTrigger eventTrigger = slotObj.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = slotObj.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            entry.callback.AddListener((data) => OnSlotClick((PointerEventData)data, slot));
            eventTrigger.triggers.Add(entry);
        }
    }

    void Update()
    {
        // Check if the context menu is active and the left mouse button is clicked
        if (contextMenu.activeSelf && Input.GetMouseButtonDown(0))
        {
            RectTransform contextMenuRect = contextMenu.GetComponent<RectTransform>();
            Vector2 localMousePosition = contextMenuRect.InverseTransformPoint(Input.mousePosition);
            if (!contextMenuRect.rect.Contains(localMousePosition))
            {
                contextMenu.SetActive(false); // Close the context menu
            }
        }
    }

    void OnSlotClick(PointerEventData eventData, InventorySlot slot)
    {
        // Check if the right mouse button is clicked
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnSlotRightClick(slot);
        }
    }

    void OnSlotRightClick(InventorySlot slot)
    {
        selectedSlot = slot;

        // Enable or disable buttons based on item type
        throwButton.gameObject.SetActive(true);
        editButton.gameObject.SetActive(slot.item is EditableItem);

        // Show the context menu at mouse position
        contextMenu.SetActive(true);
        contextMenu.transform.position = Input.mousePosition;
    }

    public void OnThrowButtonClicked()
    {
        if (selectedSlot != null && selectedSlot.item != null)
        {
            ThrowItem(selectedSlot.item);
            inventory.RemoveItem(selectedSlot.item, 1);
            UpdateUI();
            contextMenu.SetActive(false);
        }
    }

    private void ThrowItem(Item item)
    {
        if (item.itemPrefab != null)
        {
            GameObject thrownItem = Instantiate(item.itemPrefab, transform.position + transform.forward, Quaternion.identity);
            Rigidbody rb = thrownItem.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = transform.forward * 5f; // Adjust the velocity as needed
            }

            // Disable the collider temporarily to avoid instant re-pickup
            Collider collider = thrownItem.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
                StartCoroutine(EnableColliderAfterDelay(collider, 1f)); // 1 second delay
            }
        }
    }

    private IEnumerator EnableColliderAfterDelay(Collider collider, float delay)
    {
        yield return new WaitForSeconds(delay);
        collider.enabled = true;
    }



}
