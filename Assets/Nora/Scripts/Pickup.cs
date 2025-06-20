﻿using UnityEngine;
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
    //     if (inventory == null) return;

    //     for (int i = 0; i < inventory.items.Length; i++)
    //     {
    //         if (inventory.items[i] == 0)
    //         {

    //             inventory.items[i] = 1;
    //             Instantiate(gameItem.itemButton, inventory.slots[i].transform, false);


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
                }
                // else
                // {
                //     Debug.LogWarning("لم يتم العثور على TextMeshProUGUI!");
                // }

                inventory.items[i] = 1;
                Destroy(gameObject);
                break;
            }
        }
    }
}