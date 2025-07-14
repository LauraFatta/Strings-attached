using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        if (transform.childCount == 0)
        {
            dropped.transform.SetParent(transform);
            dropped.transform.position = transform.position;


        }
    }
}




