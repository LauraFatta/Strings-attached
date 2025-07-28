using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public GameItem[] items;
    public Transform inventoryContentParent;
    private InventoryUI inventoryUI;
    private NotebookManager notebook;

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

    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        
        // Add save system integration
        if (SaveSystem.instance != null)
        {
            SaveSystem.instance.RemoveCollectedPickupsInScene();
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

        // Add save system integration for scene loading
        StartCoroutine(DelayedRemovePickups());
    }

    private System.Collections.IEnumerator DelayedRemovePickups()
    {
        // Wait a frame to ensure SaveSystem is initialized
        yield return null;
        if (SaveSystem.instance != null)
        {
            SaveSystem.instance.RemoveCollectedPickupsInScene();
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
                inventoryUI.UpdateNotebookClues();
                if (notebook != null)
                {
                    notebook.AddClue(item);
                }
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
                // Animate each component before building composite
                AnimateComponentsBeforeComposite(item.requiredItemA, item.requiredItemB, item.requiredItemC);
                
                AddItem(item);
                DisableSceneObject(item.requiredItemA);
                DisableSceneObject(item.requiredItemB);
                DisableSceneObject(item.requiredItemC);
                break;
            }
        }
    }

    private void AnimateComponentsBeforeComposite(GameItem itemA, GameItem itemB, GameItem itemC)
    {
        AnimateSceneObject(itemA);
        AnimateSceneObject(itemB);
        AnimateSceneObject(itemC);
    }

    private void AnimateSceneObject(GameItem item)
    {
        foreach (var pickup in Pickup.activePickups)
        {
            if (pickup != null && pickup.gameItem == item)
            {
                // Use pickup animation for components when building composite
                var pref = Resources.Load<GameObject>("Prefabs/sns/PickupAnimator");
                if (pref != null)
                {
                    var animatorObj = Instantiate(pref, pickup.transform.position, Quaternion.identity);
                    pickup.transform.SetParent(animatorObj.transform);
                    var pickupComponent = pickup.GetComponent<Pickup>();
                    if (pickupComponent != null)
                    {
                        Destroy(pickupComponent);
                    }
                }
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

    // Add save system methods
    public List<string> GetCollectedItemIDs()
    {
        var ids = new List<string>();
        foreach (var itm in items)
            if (itm != null)
                ids.Add(itm.itemName);
        return ids;
    }

    public void LoadFromIDs(List<string> ids)
    {
        foreach (var id in ids)
        {
            GameItem item = Resources.Load<GameItem>("Items/" + id);
            if (item != null)
                AddItem(item);
        }
    }
}