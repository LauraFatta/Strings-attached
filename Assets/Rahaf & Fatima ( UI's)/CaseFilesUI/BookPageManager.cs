using UnityEngine;

public class BookPageManager : MonoBehaviour
{
    public Sprite page1Sprite; // CabnietUI_1
    public Sprite page2Sprite; // CabnietUI_2
    private GameObject page1;
    private GameObject page2;

    void Start()
    {
        // إنشاء الصفحة الأولى
        page1 = new GameObject("BookPage1");
        var sr1 = page1.AddComponent<SpriteRenderer>();
        sr1.sprite = page1Sprite;
        sr1.sortingOrder = 10;

        // إنشاء زر السهم كمجسم فرعي للصفحة 1
        GameObject nextButton = new GameObject("NextButton");
        nextButton.transform.parent = page1.transform;
        nextButton.transform.localPosition = new Vector3(2.7f, -1.7f, 0); // عدل حسب مكان السهم
        var collider = nextButton.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.7f, 0.7f);
        nextButton.AddComponent<NextButtonLogic>().manager = this;

        // إنشاء الصفحة الثانية لكنها مخفية بالبداية
        page2 = new GameObject("BookPage2");
        var sr2 = page2.AddComponent<SpriteRenderer>();
        sr2.sprite = page2Sprite;
        sr2.sortingOrder = 10;
        page2.SetActive(false);
    }

    // يتم استدعاؤها عند الضغط على زر السهم
    public void ShowPage2()
    {
        page1.SetActive(false);
        page2.SetActive(true);
        Debug.Log("📖 تم الانتقال للصفحة الثانية");
    }
}
