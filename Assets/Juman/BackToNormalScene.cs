using UnityEngine;
using System.Collections.Generic;

public class BackToNormalScene : MonoBehaviour
{
    private List<GameObject> previouslyHidden = new List<GameObject>();
    public GameObject bookToHide; // مثل: bookA

    void Start()
    {
        // حفظ الأشياء اللي تم إخفاؤها عند تفعيل الكتاب
        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            if (obj.activeInHierarchy && obj != this.gameObject && !obj.transform.IsChildOf(bookToHide.transform) && !obj.CompareTag("MainCamera"))
            {
                previouslyHidden.Add(obj);
            }
        }
    }

    void OnMouseDown()
    {
        // إخفاء الكتاب
        if (bookToHide != null)
            bookToHide.SetActive(false);

        // إعادة تفعيل كل شيء تم إخفاؤه
        foreach (GameObject obj in previouslyHidden)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        Debug.Log("↩️ تم الرجوع للمشهد الطبيعي");
    }
}
