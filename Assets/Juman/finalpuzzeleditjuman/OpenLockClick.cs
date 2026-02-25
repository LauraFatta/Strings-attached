using UnityEngine;

public class OpenLockClick : MonoBehaviour
{
    public GameObject openBag;
    public GameObject necklace;

    private void OnMouseDown()
    {
        if (openBag != null)
            openBag.SetActive(true);

        if (necklace != null)
            necklace.SetActive(true);

        gameObject.SetActive(false); // يخفي القفل المفتوح بعد الضغط
    }
}
