using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropZone2D : MonoBehaviour, IDropHandler
{
    [Tooltip("The hidden TMP text with the correct answer")]
    public TextMeshProUGUI tmp;

    private string assignedClue;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj == null) return;

        Draggable2D draggable = droppedObj.GetComponent<Draggable2D>();
        if (draggable == null) return;

        TextMeshProUGUI clueText = droppedObj.GetComponentInChildren<TextMeshProUGUI>();
        if (clueText == null) return;

        assignedClue = clueText.text;

        // Snap it in place and center it
        RectTransform droppedRect = droppedObj.GetComponent<RectTransform>();
        droppedObj.transform.SetParent(transform, false);
        droppedRect.localScale = Vector3.one;

        // Set pivot and anchors to center
        droppedRect.anchorMin = new Vector2(0.5f, 0.5f);
        droppedRect.anchorMax = new Vector2(0.5f, 0.5f);
        droppedRect.pivot = new Vector2(0.5f, 0.5f);

        // Reset position to center inside drop zone
        droppedRect.anchoredPosition = Vector2.zero;


        Debug.Log($"Dropped '{assignedClue}' into {name}");
    }

    public string GetAssignedClue()
    {
        return assignedClue;
    }

    public string GetExpectedClue()
    {
        return tmp != null ? tmp.text : "";
    }
}
