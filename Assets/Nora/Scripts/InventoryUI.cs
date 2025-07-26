using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemCountText;
    public int totalItems;

    private Inventory inventory;
    private bool clickedOnPickup = false;


    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        UpdateItemCount();
        inventoryPanel.SetActive(false);
    }

    // private void Update()
    // {
    //     if (inventoryPanel.activeSelf && Input.GetMouseButtonDown(0))
    //     {
    //         StartCoroutine(CheckToClosePanel());
    //     }
    // }
    private void Update()
    {
        if (inventoryPanel != null && inventoryPanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CheckToClosePanel());
        }
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

        foreach (GameItem item in inventory.items)
        {
            if (item != null)
                collected++;
        }

        itemCountText.text = $"{collected} / {totalItems}";
    }

    private bool IsPointerOverUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count > 0;
    }

    public void NotifyPickupClick()
    {
        clickedOnPickup = true;
    }
    private System.Collections.IEnumerator CheckToClosePanel()
    {
        yield return null; 

        if (!IsPointerOverUI() && !clickedOnPickup)
        {
            inventoryPanel.SetActive(false);
        }

        clickedOnPickup = false; 
    }


}

