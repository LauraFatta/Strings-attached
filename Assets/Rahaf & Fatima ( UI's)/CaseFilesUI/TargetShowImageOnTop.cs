using UnityEngine;

public class TargetShowImageOnTop : MonoBehaviour
{
    public Sprite imageToShow; // الصورة اللي نعرضها
    public Vector3 imagePosition = Vector3.zero; // مكان ظهور الصورة

    void OnMouseDown()
    {
        if (imageToShow == null)
        {
            Debug.LogWarning("❗ لم يتم تعيين الصورة.");
            return;
        }

        // إنشاء كائن جديد لعرض الصورة
        GameObject newImageGO = new GameObject("TopImage");
        newImageGO.transform.position = imagePosition;

        SpriteRenderer sr = newImageGO.AddComponent<SpriteRenderer>();
        sr.sprite = imageToShow;
        sr.sortingOrder = 999; // فوق كل شيء

        Debug.Log("✅ تم عرض الصورة فوق الجميع");
    }
}
