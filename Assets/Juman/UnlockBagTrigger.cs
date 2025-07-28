using UnityEngine;

public class UnlockBagTrigger : MonoBehaviour
{
    public GameObject openBagImage; // صورة الشنطة المفتوحة

    public void ShowOpenBag()
    {
        if (openBagImage != null)
            openBagImage.SetActive(true); // نشغل الصورة
    }
}
