using UnityEngine;

public class ToggleSortingOrder : MonoBehaviour
{
    public int lowOrder = 0;
    public int highOrder = 31;

    private SpriteRenderer sr;
    private bool isOnTop = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void ToggleLayer()
    {
        if (sr == null) return;

        if (isOnTop)
        {
            sr.sortingOrder = lowOrder;
        }
        else
        {
            sr.sortingOrder = highOrder;
        }

        isOnTop = !isOnTop;
    }
}
