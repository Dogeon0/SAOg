using UnityEngine;

public class Tool : MonoBehaviour
{
    // Example property
    private bool isActive;


    // Method to set active state
    public void SetActiveState(bool state)
    {
        isActive = state;
    }

    public bool IsActive()
    {
        return isActive;
    }

}

