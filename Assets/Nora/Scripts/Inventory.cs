using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public GameItem[] items;
    public Transform inventoryContentParent;
    private InventoryUI inventoryUI;

    private List<GameItem> tempComponents = new List<GameItem>();
    public GameItem[] allCompositeItems;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        if (inventoryUI != null)
        {
            inventoryUI.UpdateItemCount();
        }

        GameObject contentGO = GameObject.FindGameObjectWithTag("InventoryContent"); 
        if (contentGO != null)
        {
            inventoryContentParent = contentGO.transform;
            RebuildInventoryButtons(); 
        }
    }

    
    private void RebuildInventoryButtons()
    {
        foreach (GameItem item in items)
        {
            if (item != null && item.itemButton != null)
            {
                GameObject button = Instantiate(item.itemButton, inventoryContentParent);
                var textComponent = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = item.itemName;
                }
            }
        }
    }

    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public bool AddItem(GameItem item)
    {
        foreach (GameItem existingItem in items)
        {
            if (existingItem == item)
            {
                return false;
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;

                GameObject button = Instantiate(item.itemButton, inventoryContentParent);
                var textComponent = button.GetComponentInChildren<TMPro.TextMeshProUGUI>();
                if (textComponent != null)
                {
                    textComponent.text = item.itemName;
                }

                if (item.itemType == ItemType.ComponentOnly)
                {
                    tempComponents.Add(item);
                    TryBuildComposite();
                }

                inventoryUI.UpdateItemCount();
                return true;
            }
        }

        return false;
    }

    private void TryBuildComposite()
    {
        foreach (var item in allCompositeItems)
        {
            if (item.itemType != ItemType.actions) continue;
            if (item.requiredItemA == null || item.requiredItemB == null) continue;

            if (tempComponents.Contains(item.requiredItemA) &&
                tempComponents.Contains(item.requiredItemB) &&
                tempComponents.Contains(item.requiredItemC))
            {
                AddItem(item);
                DisableSceneObject(item.requiredItemA);
                DisableSceneObject(item.requiredItemB);
                DisableSceneObject(item.requiredItemC);
                break;
            }
        }
    }

    private void DisableSceneObject(GameItem item)
    {
        foreach (var pickup in Pickup.activePickups)
        {
            if (pickup != null && pickup.gameItem == item)
            {
                pickup.ShowUncollectedVisual(false);
                Destroy(pickup.gameObject);
                break;
            }
        }
    }
}
