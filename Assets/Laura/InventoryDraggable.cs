using UnityEngine;

public class DraggableItem2D : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 originalPosition;
    private Transform originalParent;
    private bool isDragging = false;

    private void Start()
    {
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Check for collision with drop slots
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("DropSlot"))
            {
                // Snap to center of drop slot
                transform.position = hit.transform.position;
                return;
            }
        }

        // Snap back if no valid slot found
        transform.position = originalPosition;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 screenPos = Input.mousePosition;
        screenPos.z = 10f; // Distance from camera
        return Camera.main.ScreenToWorldPoint(screenPos);
    }
}
