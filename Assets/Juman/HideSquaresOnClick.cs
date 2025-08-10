using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HideSquaresOnClick : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("الأشياء التي ستختفي عند الضغط (أضف هذا المربع + المربع/المربعات الثانية)")]
    public List<GameObject> targetsToHide = new List<GameObject>();

    // للسبرايتات ثنائية الأبعاد (لو ما تستخدم UI)
    private void OnMouseDown()
    {
        // يعمل إذا كان فيه Collider2D
        HideAll();
    }

    // لعناصر الـ UI (Image / Button ...الخ)
    public void OnPointerClick(PointerEventData eventData)
    {
        HideAll();
    }

    private void HideAll()
    {
        if (targetsToHide == null || targetsToHide.Count == 0)
        {
            // لو ما حددت شيء، اختفِ هذا العنصر فقط
            gameObject.SetActive(false);
            return;
        }

        foreach (var go in targetsToHide)
        {
            if (go != null) go.SetActive(false);
        }
    }

    // عشان يضيف نفسه تلقائياً في القائمة أول ما تضيف السكربت
    private void Reset()
    {
        if (!targetsToHide.Contains(gameObject))
            targetsToHide.Add(gameObject);
    }
}
