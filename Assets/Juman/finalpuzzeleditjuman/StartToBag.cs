using UnityEngine;

public class StartToBag : MonoBehaviour
{
    public GameObject closedBag;

    private void OnMouseDown()
    {
        closedBag.SetActive(true);      // تظهر الشنطة
        gameObject.SetActive(false);    // تخفي الزر نفسه
    }
}
