using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    public GameObject inventoryUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        CanvasGroup canvasGroup = inventoryUI.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = canvasGroup.alpha == 1f ? 0f : 1f;
            canvasGroup.interactable = canvasGroup.alpha == 1f;
            canvasGroup.blocksRaycasts = canvasGroup.alpha == 1f;

            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            // Update the UI only if the inventory is being shown
            if (canvasGroup.alpha == 1f)
            {
                InventoryUI inventoryUIScript = inventoryUI.GetComponent<InventoryUI>();
                if (inventoryUIScript != null)
                {
                    inventoryUIScript.UpdateUI();
                }
                else
                {
                    Debug.LogWarning("InventoryUI script not found on the inventoryUI GameObject.");
                }
            }
        }
        else
        {
            Debug.LogWarning("CanvasGroup component not found on the inventoryUI GameObject.");
        }
    }
}
