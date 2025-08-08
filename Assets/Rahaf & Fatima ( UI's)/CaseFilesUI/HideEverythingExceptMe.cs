using UnityEngine;

public class HideEverythingExceptMe : MonoBehaviour
{
    void OnEnable()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj == this.gameObject) continue; // لا تخفي الكتاب نفسه
            if (obj.transform.IsChildOf(this.transform)) continue; // لا تخفي أطفاله
            if (obj.CompareTag("MainCamera")) continue; // لا تخفي الكاميرا

            obj.SetActive(false);
        }

        Debug.Log("📘 الكتاب ظهر، كل شيء اختفى!");
    }
}
