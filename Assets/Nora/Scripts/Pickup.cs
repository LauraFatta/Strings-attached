using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class Pickup : MonoBehaviour, IPointerDownHandler
{
    public GameItem gameItem;
    public static List<Pickup> activePickups = new List<Pickup>();

    private Inventory inventory;
    private Renderer objectRenderer;
    

    private void OnEnable()
    {
        activePickups.Add(this);
    }

    private void OnDisable()
    {
        activePickups.Remove(this);
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        objectRenderer = GetComponent<Renderer>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryUI ui = FindObjectOfType<InventoryUI>();
        if (ui != null)
        {
            ui.NotifyPickupClick();
        }

        if (inventory.AddItem(gameItem))
        {
            if (gameItem.itemType != ItemType.ComponentOnly)
            {
                Destroy(gameObject);
            }

            if (gameItem.itemType == ItemType.ComponentOnly)
            {
                ShowUncollectedVisual(true);
            }
        }
    }

    public void ShowUncollectedVisual(bool show)
    {
        if (objectRenderer != null && gameItem.itemType == ItemType.ComponentOnly)
        {
            objectRenderer.material.color = show ? Color.red : Color.white;
        }
    
    }
}
