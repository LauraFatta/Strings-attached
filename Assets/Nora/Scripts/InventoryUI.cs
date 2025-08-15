// using UnityEngine;
// using TMPro;
// using UnityEngine.EventSystems;
// using System.Collections.Generic;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class InventoryUI : MonoBehaviour
// {
//     public GameObject inventoryPanel;
//     public TextMeshProUGUI itemCountText;
//     public int totalItems;

//     private Inventory inventory;
//     private bool clickedOnPickup = false;
//     //Laura's Edit:
//     public Transform notebookClueContainer;
//     public GameObject notebookCluePrefabRed;
//     public GameObject notebookCluePrefabBlue;


//     private void Start()
//     {
//         inventory = FindObjectOfType<Inventory>();

//         UpdateItemCount();
//         inventoryPanel.SetActive(false);
//     }

//     private void Update()
//     {
//         if (inventoryPanel != null && inventoryPanel.activeSelf && Input.GetMouseButtonDown(0))
//         {
//             StartCoroutine(CheckToClosePanel());
//         }
//     }





//     public void ToggleInventory()
//     {
//         inventoryPanel.SetActive(!inventoryPanel.activeSelf);
//     }

//     public void UpdateItemCount()
//     {
//         if (inventory == null || inventory.items == null)
//             return;

//         int collected = 0;

//         foreach (GameItem item in inventory.items)
//         {
//             if (item != null)
//                 collected++;
//         }

//         itemCountText.text = $"{collected} / {totalItems}";
//     }

//     private bool IsPointerOverUI()
//     {
//         PointerEventData pointerData = new PointerEventData(EventSystem.current)
//         {
//             position = Input.mousePosition
//         };

//         List<RaycastResult> results = new List<RaycastResult>();
//         EventSystem.current.RaycastAll(pointerData, results);

//         return results.Count > 0;
//     }

//     public void NotifyPickupClick()
//     {
//         clickedOnPickup = true;
//     }
//     private System.Collections.IEnumerator CheckToClosePanel()
//     {
//         yield return null; 

//         if (!IsPointerOverUI() && !clickedOnPickup)
//         {
//             inventoryPanel.SetActive(false);
//         }

//         clickedOnPickup = false; 
//     }


   


//        public void UpdateNotebookClues()
//     {
//         foreach (Transform child in notebookClueContainer)
//             Destroy(child.gameObject);

//         foreach (GameItem item in inventory.items)
//         {
//             if (item == null) continue;

//             // Declare it once before if/else
//             GameObject clue;

//             if (item.itemType == ItemType.Object)
//             {
//                 clue = Instantiate(notebookCluePrefabRed, notebookClueContainer, false);
//             }
//             else
//             {
//                 clue = Instantiate(notebookCluePrefabBlue, notebookClueContainer, false);
//             }

//             // Make it draggable only in the notebook
//             if (clue.GetComponent<Draggable2D>() == null)
//                 clue.AddComponent<Draggable2D>();

//             // Make it properly sized for the notebook
//             LayoutElement layout = clue.GetComponent<LayoutElement>();
//             if (layout == null) layout = clue.AddComponent<LayoutElement>();
//             layout.preferredWidth = 160;
//             layout.preferredHeight = 40;

//             // Update text + color
//             TextMeshProUGUI text = clue.GetComponentInChildren<TextMeshProUGUI>();
//             if (text != null)
//             {
//                 text.text = item.itemName;
//             }

//             // Force center anchor/position
//             RectTransform textRT = text.GetComponent<RectTransform>();
//             if (textRT != null)
//             {
//                 textRT.anchorMin = new Vector2(0.5f, 0.5f);
//                 textRT.anchorMax = new Vector2(0.5f, 0.5f);
//                 textRT.pivot = new Vector2(0.5f, 0.5f);
//                 textRT.anchoredPosition = Vector2.zero;
//             }
//         }
//     }


// }




///////------------------- تعديل رهف -------------------------
/// using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public TextMeshProUGUI itemCountText;
    public int totalItems;

    private Inventory inventory;
    private bool clickedOnPickup = false;
    //Laura's Edit:
    public Transform notebookClueContainer;
    public GameObject notebookCluePrefabRed;
    public GameObject notebookCluePrefabBlue;


    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();

        UpdateItemCount();
        inventoryPanel.SetActive(false);
    }

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
    private bool IsPointerOverThisPanel(GameObject panel)
    {
        if (!panel) return false;

        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);

        foreach (var r in results)
        {
            if (r.gameObject && r.gameObject.transform.IsChildOf(panel.transform))
                return true;
        }
        return false;
    }

    public void NotifyPickupClick()
    {
        clickedOnPickup = true;
    }

    private System.Collections.IEnumerator CheckToClosePanel()
    {
        yield return null;

        if (!IsPointerOverThisPanel(inventoryPanel))
        {
            inventoryPanel.SetActive(false);
        }

        clickedOnPickup = false;
    }

    public void UpdateNotebookClues()
    {
        foreach (Transform child in notebookClueContainer)
            Destroy(child.gameObject);

        foreach (GameItem item in inventory.items)
        {
            if (item == null) continue;

            // Declare it once before if/else
            GameObject clue;

            if (item.itemType == ItemType.Object)
            {
                clue = Instantiate(notebookCluePrefabRed, notebookClueContainer, false);
            }
            else
            {
                clue = Instantiate(notebookCluePrefabBlue, notebookClueContainer, false);
            }

            // Make it draggable only in the notebook
            if (clue.GetComponent<Draggable2D>() == null)
                clue.AddComponent<Draggable2D>();

            // Make it properly sized for the notebook
            LayoutElement layout = clue.GetComponent<LayoutElement>();
            if (layout == null) layout = clue.AddComponent<LayoutElement>();
            layout.preferredWidth = 160;
            layout.preferredHeight = 40;

            // Update text + color
            TextMeshProUGUI text = clue.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = item.itemName;
            }

            // Force center anchor/position
            RectTransform textRT = text.GetComponent<RectTransform>();
            if (textRT != null)
            {
                textRT.anchorMin = new Vector2(0.5f, 0.5f);
                textRT.anchorMax = new Vector2(0.5f, 0.5f);
                textRT.pivot = new Vector2(0.5f, 0.5f);
                textRT.anchoredPosition = Vector2.zero;
            }
        }
    }
}


