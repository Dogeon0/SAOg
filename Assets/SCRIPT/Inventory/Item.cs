using UnityEngine;

[System.Serializable]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
}
