using UnityEngine;

[CreateAssetMenu(fileName = "GameItem", menuName = "ScriptableObjects/GameItem")]
public class GameItem : ScriptableObject
{
    public string itemName;
    // public GameObject itemPrefab;
    public GameObject itemButton;

    [TextArea(3, 10)] 
    public string itemDescription;
}

