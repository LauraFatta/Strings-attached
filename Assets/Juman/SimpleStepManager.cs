using UnityEngine;
using UnityEngine.UI;

public class SimpleStepManager : MonoBehaviour
{
    public GameObject imageClosedBag;
    public Button bagButton; // زر الشنطة داخل الصورة
    public GameObject puzzlePanel;

    void Start()
    {
        // ربط الحدث
        bagButton.onClick.AddListener(ShowPuzzlePanel);

        // إظهار صورة الشنطة فقط
        imageClosedBag.SetActive(true);
        puzzlePanel.SetActive(false);
    }

    void ShowPuzzlePanel()
    {
        imageClosedBag.SetActive(false);
        puzzlePanel.SetActive(true);
    }
}
