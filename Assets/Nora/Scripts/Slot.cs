using UnityEngine;

public class Slot : MonoBehaviour
{
    public int index;
    private Inventory inventory;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventory == null)
        {
            Debug.LogError("Inventory not found!");
            enabled = false;
        }
    }

    // private void Update()
    // {
    //     if (transform.childCount <= 0)
    //     {
           
    //         inventory.items[index] = null;
    //     }
    // }
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0)
        {
            inventory.items[index] = null;
            inventoryUI.UpdateItemCount();
        }
    }

    public void RemoveItem()
    {
        foreach (Transform child in transform)
        {
            Spawn spawner = child.GetComponent<Spawn>();
            if (spawner != null)
            {
                spawner.SpawnItem();
            }

            Destroy(child.gameObject);
        }


        inventory.items[index] = null;
        inventoryUI.UpdateItemCount();
    }
}
