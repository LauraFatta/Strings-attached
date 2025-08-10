using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DropZone2D : MonoBehaviour, IDropHandler
{
    [Tooltip("The hidden TMP text with the correct answer")]
    public TextMeshProUGUI tmp;

    private string assignedClue;
    private GameObject currentClue; // Track the clue currently in this slot

    [Header("Settings")]
    public float overlapThreshold = 0.3f; // How much of the object must be over the slot to accept it

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj == null) return;

        // Reject if there's already something here
        if (currentClue != null) return;

        // Check overlap leniency
        RectTransform dropRect = GetComponent<RectTransform>();
        RectTransform draggedRect = droppedObj.GetComponent<RectTransform>();

        if (!IsOverlappingEnough(dropRect, draggedRect)) return;

        Draggable2D draggable = droppedObj.GetComponent<Draggable2D>();
        if (draggable == null) return;

        TextMeshProUGUI clueText = droppedObj.GetComponentInChildren<TextMeshProUGUI>();
        if (clueText == null) return;

        assignedClue = clueText.text;
        currentClue = droppedObj;

        // Snap it in place
        droppedObj.transform.SetParent(transform, false);
        // Match scale to drop zone size
        Vector2 slotSize = dropRect.rect.size;
        Vector2 draggedSize = draggedRect.rect.size;

        float scaleFactor = Mathf.Min(
            slotSize.x / draggedSize.x,
            slotSize.y / draggedSize.y
        );
        Vector3 droppedScale = new Vector3(scaleFactor, scaleFactor, 1f);
        draggedRect.localScale = droppedScale;

        // NEW: Tell the draggable what its "dropped size" should be
        draggable.SetDroppedScale(droppedScale);

        // Center inside slot
        draggedRect.anchorMin = draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
        draggedRect.pivot = new Vector2(0.5f, 0.5f);
        draggedRect.anchoredPosition = Vector2.zero;

        draggable.SetDropZone(this);

        Debug.Log($"Dropped '{assignedClue}' into {name}");
    }

    private bool IsOverlappingEnough(RectTransform slot, RectTransform obj)
    {
        Rect slotWorld = GetWorldRect(slot);
        Rect objWorld = GetWorldRect(obj);

        Rect overlap = Rect.MinMaxRect(
            Mathf.Max(slotWorld.xMin, objWorld.xMin),
            Mathf.Max(slotWorld.yMin, objWorld.yMin),
            Mathf.Min(slotWorld.xMax, objWorld.xMax),
            Mathf.Min(slotWorld.yMax, objWorld.yMax)
        );

        float overlapArea = Mathf.Max(0, overlap.width) * Mathf.Max(0, overlap.height);
        float objArea = objWorld.width * objWorld.height;

        return (overlapArea / objArea) >= overlapThreshold;
    }

    private Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return new Rect(corners[0].x, corners[0].y,
                        corners[2].x - corners[0].x,
                        corners[2].y - corners[0].y);
    }

    public void ClearSlot()
    {
        if (currentClue != null)
        {
            Draggable2D draggable = currentClue.GetComponent<Draggable2D>();
            if (draggable != null)
                draggable.ResetToOriginalParent();

            currentClue = null;
            assignedClue = null;
        }
    }

    // NEW: Clear slot without resetting the draggable (used when picking up)
    public void ClearSlotWithoutReset()
    {
        currentClue = null;
        assignedClue = null;
    }

    public string GetAssignedClue() => assignedClue;
    public string GetExpectedClue() => tmp != null ? tmp.text : "";
}