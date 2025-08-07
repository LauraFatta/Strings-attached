using UnityEngine;

public class ClosedBagClick : MonoBehaviour
{
    public GameObject puzzleGroup;

    private void OnMouseDown()
    {
        puzzleGroup.SetActive(true);      // يظهر البازل
        gameObject.SetActive(false);      // يخفي الشنطة المغلقة
    }
}
