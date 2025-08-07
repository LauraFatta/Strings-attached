using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public static NotebookManager Instance;
    public Transform notebookClueContainer; // Empty UI GameObject to hold word clues
    public GameObject cluePrefab; // Prefab with Draggable2D and UI setup
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddClue(GameItem item)
    {
        if (item == null)
            return;

        GameObject clue = Instantiate(cluePrefab, notebookClueContainer);
        clue.name = item.itemName;

        var text = clue.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if (text != null)
            text.text = item.itemName;

        clue.tag = item.clueTag;  // Important for DropZone tag checking
    }
}
