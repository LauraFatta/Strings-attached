using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Pickup : MonoBehaviour, IPointerDownHandler
{
    public GameItem gameItem; // ScriptableObject 

    private Inventory inventory;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventory == null)
        {
            Debug.LogError("Inventory not found in the scene!");
            enabled = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        if (gameItem.itemType == ItemType.ComponentOnly)
        {
            inventory.AddItem(gameItem); 
            Destroy(gameObject);
            return;
        }

        
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i] == null)
            {
                inventory.items[i] = gameItem;

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

                inventoryUI.UpdateItemCount();
                Destroy(gameObject);
                break;
            }
        }
    }
}
