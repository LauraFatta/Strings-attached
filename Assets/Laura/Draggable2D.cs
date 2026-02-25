using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable2D : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Vector2 originalPosition;
    private Transform originalParent;
    private Vector3 originalScale; // The initial scale when game starts
    private Vector3 currentScale; // The scale it should return to (either original or dropped size)
    private DropZone2D currentZone;
    private Vector2 baseSize; // The original width/height before any scaling


    // Store the TRUE original parent and position from game start
    private Vector2 trueOriginalPosition;
    private Transform trueOriginalParent;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        currentScale = originalScale; // Initially, both are the same

        // Store the TRUE starting position and parent
        trueOriginalPosition = rectTransform.anchoredPosition;
        trueOriginalParent = transform.parent;
        baseSize = rectTransform.rect.size;
    }

    public Vector2 GetBaseSize() => baseSize;


    public void OnBeginDrag(PointerEventData eventData)
    {
        // Key fix: Use currentScale instead of originalScale
        rectTransform.localScale = currentScale;

        // Set drag-specific original position/parent (for immediate return if needed)
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;

        // Clear current zone since we're picking it up
        if (currentZone != null)
        {
            currentZone.ClearSlotWithoutReset();
            currentZone = null;
        }

        transform.SetParent(canvas.transform, true);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null) return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        GameObject slot = GetSlotUnderPointer(eventData);

        if (slot != null)
        {
            DropZone2D zone = slot.GetComponent<DropZone2D>();
            if (zone != null)
            {
                zone.OnDrop(eventData);
                return; // Let the DropZone handle acceptance/rejection
            }
        }

        // If not dropped on a slot → return to TRUE original position and size
        ReturnToOriginalPosition();
    }

    // Helper method to return to original position
    private void ReturnToOriginalPosition()
    {
        transform.SetParent(trueOriginalParent, false);
        rectTransform.localScale = originalScale; // Back to original size when returning home
        currentScale = originalScale; // Update currentScale to original
        rectTransform.anchoredPosition = trueOriginalPosition; // Use true original position
    }

    private GameObject GetSlotUnderPointer(PointerEventData eventData)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("DropSlot"))
                return result.gameObject;
        }

        return null;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    public void SetDropZone(DropZone2D zone)
    {
        currentZone = zone;
    }

    // Call this when successfully dropped to update the "current" scale
    public void SetDroppedScale(Vector3 droppedScale)
    {
        currentScale = droppedScale;
    }

    public void ResetToOriginalParent()
    {
        transform.SetParent(trueOriginalParent, false);
        rectTransform.localScale = originalScale;
        currentScale = originalScale; // Reset currentScale when going back to original
        rectTransform.anchoredPosition = trueOriginalPosition; // Use true original position
    }

    // Called when drop is invalid (slot occupied or overlap too small)
    public void SnapBack()
    {
        if (currentZone != null)
        {
            // Return to the slot we were in
            transform.SetParent(currentZone.transform, false);
            rectTransform.localScale = currentScale; // stay at dropped size
            rectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            // Return home
            transform.SetParent(trueOriginalParent, false);
            rectTransform.localScale = originalScale;
            rectTransform.anchoredPosition = trueOriginalPosition;
            currentScale = originalScale;
        }
    }

}