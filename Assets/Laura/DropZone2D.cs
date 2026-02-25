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
    [Range(0.5f, 0.8f)]
    public float scaleFactor = 0.9f; // How much to scale down items (0.8 = 80% of slot size)

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObj = eventData.pointerDrag;
        if (droppedObj == null) return;

        Draggable2D draggable = droppedObj.GetComponent<Draggable2D>();
        if (draggable == null) return;

        // REJECT if there's already something here
        if (currentClue != null)
        {
            Debug.Log($"Slot {name} already occupied, snapping clue back");
            draggable.SnapBack(); // Keep this fix!
            return;
        }

        // Check overlap leniency
        RectTransform dropRect = GetComponent<RectTransform>();
        RectTransform draggedRect = droppedObj.GetComponent<RectTransform>();

        if (!IsOverlappingEnough(dropRect, draggedRect))
        {
            draggable.SnapBack(); // Keep this fix!
            return;
        }

        // SUCCESS - Accept the drop
        TextMeshProUGUI clueText = droppedObj.GetComponentInChildren<TextMeshProUGUI>();
        if (clueText == null) return;

        assignedClue = clueText.text;
        currentClue = droppedObj;

        // Snap it in place
        droppedObj.transform.SetParent(transform, false);

        // IMPROVED SCALING: Use current actual size, not base size
        Vector2 slotSize = dropRect.rect.size;
        Vector2 draggedCurrentSize = draggedRect.rect.size;

        // Scale to fit the slot size properly
        float scaleX = slotSize.x / draggedCurrentSize.x;
        float scaleY = slotSize.y / draggedCurrentSize.y;

        // Use the smaller scale to ensure it fits both dimensions, with some padding
        float finalScale = Mathf.Min(scaleX, scaleY) * scaleFactor;

        Vector3 droppedScale = new Vector3(finalScale, finalScale, 1f);
        draggedRect.localScale = droppedScale;
        draggable.SetDroppedScale(droppedScale);

        // Center inside slot
        draggedRect.anchorMin = draggedRect.anchorMax = new Vector2(0.5f, 0.5f);
        draggedRect.pivot = new Vector2(0.5f, 0.5f);
        draggedRect.anchoredPosition = Vector2.zero;

        draggable.SetDropZone(this);

        Debug.Log($"Successfully dropped '{assignedClue}' into {name} with scale {finalScale:F2}");
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

    // Clear slot without resetting the draggable (used when picking up)
    public void ClearSlotWithoutReset()
    {
        currentClue = null;
        assignedClue = null;
    }

    public string GetAssignedClue() => assignedClue;
    public string GetExpectedClue() => tmp != null ? tmp.text : "";
}