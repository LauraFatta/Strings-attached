using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemCountText;
    public int totalItems = 2;

    private Inventory inventory; 

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        UpdateItemCount(); 
        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    public void UpdateItemCount()
    {
        if (inventory == null || inventory.items == null)
            return;

        int collected = 0;
        foreach (int i in inventory.items)
        {
            if (i == 1) collected++;
        }

        itemCountText.text = $"{collected} / {totalItems}";
    }
}
