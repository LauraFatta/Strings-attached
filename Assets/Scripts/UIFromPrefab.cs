using UnityEngine;
using UnityEngine.UI;

public class BookSpawner : MonoBehaviour
{
    [SerializeField] Canvas targetCanvas;                 // حطي Canvas البازل هنا
    [Header("Prefabs")]
    [SerializeField] RectTransform bookFramePrefab;       // إطار الكتاب (وفيه Child اسمها ContentSlot)
    [SerializeField] RectTransform defaultContentPrefab;  // المحتوى الافتراضي

    RectTransform bookInst;
    RectTransform contentInst;

    // زر الميسّنق ثريد يستدعي هذي
    public void ShowDefault() => Show(defaultContentPrefab);

    // لو تبغي تختاري محتوى مختلف من الإنسبكتور في OnClick
    public void ShowWithPrefab(RectTransform contentPrefab) => Show(contentPrefab);

    public void Show(RectTransform contentPrefab)
    {
        if (!targetCanvas || !bookFramePrefab || !contentPrefab) return;

        // 1) طلّع إطار الكتاب داخل Canvas البازل
        if (!bookInst)
        {
            bookInst = Instantiate(bookFramePrefab, targetCanvas.transform, false);

            // نلقط زر الإغلاق داخل الإطار لو اسمه CloseButton
            var closeT = bookInst.Find("CloseButton");
            if (closeT)
            {
                var btn = closeT.GetComponent<Button>();
                if (btn)
                {
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(Close);
                }
            }
        }

        // 2) حطّ المحتوى جوّا مكانه
        var slot = bookInst.Find("ContentSlot") as RectTransform;
        if (!slot) slot = bookInst;

        if (contentInst) Destroy(contentInst.gameObject);
        contentInst = Instantiate(contentPrefab, slot, false);

        bookInst.gameObject.SetActive(true);
        bookInst.SetAsLastSibling(); // فوق عناصر البازل داخل نفس الكانفس
    }

    public void Close()
    {
        if (contentInst) { Destroy(contentInst.gameObject); contentInst = null; }
        if (bookInst)    { Destroy(bookInst.gameObject);    bookInst    = null; }
    }
}
