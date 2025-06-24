using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Pickup : MonoBehaviour, IPointerDownHandler
{
    public GameItem gameItem; // ScriptableObject 

    private Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene!");
            enabled = false;
        }
    }


    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     for (int i = 0; i < inventory.items.Length; i++)
    //     {
    //         if (inventory.items[i] == 0)
    //         {
    //             GameObject button = Instantiate(gameItem.itemButton, inventory.slots[i].transform, false);


    //             TextMeshProUGUI textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
    //             if (textComponent != null)
    //             {
    //                 textComponent.text = gameItem.itemName;
    //             }


    //             inventory.items[i] = 1;
    //             Destroy(gameObject);
    //             break;
    //         }
    //     }
    // }
    public void OnPointerDown(PointerEventData eventData)
{
    for (int i = 0; i < inventory.items.Length; i++)
    {
        if (inventory.items[i] == 0)
        {
            GameObject button = Instantiate(gameItem.itemButton, inventory.slots[i].transform, false);

            TextMeshProUGUI textComponent = button.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = gameItem.itemName;

            
                if (gameItem.itemType == ItemType.Word)
                {
                    textComponent.color = Color.yellow;
                }
            }

            inventory.items[i] = 1;
            Destroy(gameObject);
            break;
        }
    }
}

}