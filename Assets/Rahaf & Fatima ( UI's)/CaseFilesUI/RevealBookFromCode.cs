using UnityEngine;

public class RevealBookFromCode : MonoBehaviour
{
    public Sprite bookSprite; // صورة الكتاب

    void OnMouseDown()
    {
        Debug.Log("📘 تم الضغط على التارقت!");

        // 1. تغيير لون الخلفية للكاميرا إلى رمادي
        if (Camera.main != null)
        {
            Camera.main.backgroundColor = new Color(0.16f, 0.16f, 0.16f); // رمادي داكن (#2A2A2A)
        }

        // 2. إنشاء الكتاب
        GameObject bookGO = new GameObject("Book");
        bookGO.transform.position = new Vector3(0, 0, 0);

        SpriteRenderer sr = bookGO.AddComponent<SpriteRenderer>();
        sr.sprite = bookSprite;
        sr.sortingOrder = 999; // فوق الكل

        // 3. إخفاء كل شيء آخر
        HideEverythingExcept(bookGO);
    }

    void HideEverythingExcept(GameObject exception)
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj == exception) continue;
            if (obj.transform.IsChildOf(exception.transform)) continue;
            if (obj.CompareTag("MainCamera")) continue;

            obj.SetActive(false);
        }
    }
}
