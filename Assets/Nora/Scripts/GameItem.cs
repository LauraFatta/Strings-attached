using UnityEngine;

public enum ItemType
{
    Object,
    names,
    actions ,
    ComponentOnly,
    locations 
}
[CreateAssetMenu(fileName = "GameItem", menuName = "ScriptableObjects/GameItem")]
public class GameItem : ScriptableObject
{
    public string itemName;
    public GameObject itemButton;
    public ItemType itemType;

    [TextArea(3, 2)]
    public string itemDescription;

    public GameItem requiredItemA;
    public GameItem requiredItemB;
    public GameItem requiredItemC;
    [Tooltip("Used for notebook clues — e.g. 'BlueClue', 'RedClue'.")]
    public string clueTag;
}

