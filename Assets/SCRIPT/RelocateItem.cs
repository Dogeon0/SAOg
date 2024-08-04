using UnityEngine;

public class RelocateItem : MonoBehaviour
{
    public GameObject itemToRelocate; 
    public Vector3 worldPosition;
    public Vector3 worldRotation;
    public Vector3 worldScale;


    void Start()
    {
        if (itemToRelocate != null)
        {
            itemToRelocate.transform.SetParent(transform);
            itemToRelocate.transform.position = worldPosition;
            itemToRelocate.transform.rotation = Quaternion.Euler(worldRotation);
            itemToRelocate.transform.localScale = worldScale;
        }
        else
        {
            Debug.LogError("itemToRelocate not assigned!");
        }
    }
}
