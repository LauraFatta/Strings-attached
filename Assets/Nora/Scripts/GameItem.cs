using UnityEngine;

public enum ItemType
{
    Object,
    Word 
}
[CreateAssetMenu(fileName = "GameItem", menuName = "ScriptableObjects/GameItem")]
public class GameItem : ScriptableObject
{
    public string itemName;
    // public GameObject itemPrefab;
    public GameObject itemButton;
    public ItemType itemType;

    [TextArea(3, 10)] 
    public string itemDescription;
}

