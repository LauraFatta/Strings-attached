using UnityEngine;
using System.Collections.Generic;


public class Inventory : MonoBehaviour
{
    public GameItem[] items;
    public GameObject[] slots;
    private InventoryUI inventoryUI;

    private List<GameItem> tempComponents = new List<GameItem>();
    public GameItem[] allCompositeItems;

    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    // public void AddItem(GameItem item)
    // {
    //     if (item.itemType == ItemType.ComponentOnly)
    //     {

    //         tempComponents.Add(item);
    //         Debug.Log($"gathering done : {item.itemName}");

    //         TryBuildComposite();
    //         return;
    //     }


    //     for (int i = 0; i < items.Length; i++)
    //     {
    //         if (items[i] == null)
    //         {
    //             items[i] = item;

    //             GameObject button = Instantiate(item.itemButton, slots[i].transform, false);

    //             var textComponent = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    //             if (textComponent != null)
    //             {
    //                 textComponent.text = item.itemName;

    //                 if (item.itemType == ItemType.Word)
    //                 {
    //                     textComponent.color = Color.yellow;
    //                 }
    //             }

    //             inventoryUI.UpdateItemCount();
    //             break;
    //         }
    //     }
    // }

    public bool AddItem(GameItem item)
    {
        if (item.itemType == ItemType.ComponentOnly)
        {
            tempComponents.Add(item);
            Debug.Log($"gathering done : {item.itemName}");
            TryBuildComposite();
            return true;
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                GameObject button = Instantiate(item.itemButton, slots[i].transform, false);

                var textComponent = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = item.itemName;
                    if (item.itemType == ItemType.Word) textComponent.color = Color.yellow;
                }

                inventoryUI.UpdateItemCount();
                return true;
            }
        }
        return false;
    }

    // private void TryBuildComposite()
    // {
    //     // GameItem[] allItems = Resources.LoadAll<GameItem>(""); 

    //     foreach (var item in allCompositeItems)
    //     {
    //         if (item.itemType != ItemType.Composite)
    //             continue;

    //         if (item.requiredItemA == null || item.requiredItemB == null)
    //             continue;

    //         if (tempComponents.Contains(item.requiredItemA) && tempComponents.Contains(item.requiredItemB))
    //         {
    //             Debug.Log($"composition : {item.itemName}");


    //             tempComponents.Remove(item.requiredItemA);
    //             tempComponents.Remove(item.requiredItemB);


    //             AddItem(item);
    //             break;
    //         }
    //     }
    // }
    private void TryBuildComposite()
    {
        foreach (var item in allCompositeItems)
        {
            if (item.itemType != ItemType.Composite) continue;
            if (item.requiredItemA == null || item.requiredItemB == null) continue;

            if (tempComponents.Contains(item.requiredItemA) && 
                tempComponents.Contains(item.requiredItemB))
            {
                tempComponents.Remove(item.requiredItemA);
                tempComponents.Remove(item.requiredItemB);
                AddItem(item); 
                break;
            }
        }
    }


}
