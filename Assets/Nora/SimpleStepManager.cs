
using UnityEngine;
using UnityEngine.UI;

public class SimpleStepManager : MonoBehaviour
{
    public GameObject imageClosedBag;     // صورة الشنطة المغلقة
    public Button bagButton;              // الزر الموجود على الشنطة
    public GameObject puzzlePanel;        // واجهة البازل

    public Button openBagButton;          // زر البداية (Start)

    void Start()
    {
        // إخفاء الشنطة وواجهة البازل في البداية
        imageClosedBag.SetActive(false);
        puzzlePanel.SetActive(false);

        // ربط الأزرار بالدوال
        openBagButton.onClick.AddListener(ShowClosedBag);
        bagButton.onClick.AddListener(ShowPuzzlePanel);
    }

    public void ShowClosedBag()
    {
        Debug.Log("تم عرض الشنطة");
        imageClosedBag.SetActive(true);
    }

    public void ShowPuzzlePanel()
    {
        Debug.Log("تم فتح البازل");
        imageClosedBag.SetActive(false);
        puzzlePanel.SetActive(true);
    }
}
