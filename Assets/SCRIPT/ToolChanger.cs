using System.Collections.Generic;
using UnityEngine;

public class ToolChanger : MonoBehaviour
{
    public List<GameObject> tools; // List of tool prefabs
    public Transform toolHolder;   // The parent object to hold the tools (e.g., "Held Item" in your hierarchy)
    
    private GameObject currentTool; // The currently equipped tool
    private int currentToolIndex = -1;

    void Start()
    {
        if (tools.Count > 0)
        {
            EquipTool(0); // Equip the first tool by default
        }
    }

    void Update()
    {
        // Listen for number key presses (1-9)
        for (int i = 0; i < tools.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EquipTool(i);
            }
        }
    }

    void AddTool(){
        //tools.Add();
        
    }

    void EquipTool(int index)
    {
        if (index < 0 || index >= tools.Count || index == currentToolIndex)
        {
            return; // Invalid index or same tool
        }

        // Deactivate the current tool if any
        if (currentTool != null)
        {
            Tool currentToolScript = currentTool.GetComponent<Tool>();
            if (currentToolScript != null)
            {
                currentToolScript.SetActiveState(false);
            }
            Destroy(currentTool);
        }

        // Instantiate and equip the new tool
        currentTool = Instantiate(tools[index], toolHolder);
        currentToolIndex = index;

        // Activate the new tool
        Tool newToolScript = currentTool.GetComponent<Tool>();
        if (newToolScript != null)
        {
            newToolScript.SetActiveState(true);
        }
    }
}
