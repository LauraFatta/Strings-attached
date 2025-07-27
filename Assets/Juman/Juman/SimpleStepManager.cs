using UnityEngine;
using UnityEngine.UI;

public class SimpleStepManager : MonoBehaviour
{
    public Button buttonStart;
    public GameObject imageClosedBag;
    public Button bagButton; // زر الشنطة داخل الصورة
    public GameObject puzzlePanel;

    void Start()
    {
        // ربط الأحداث
        buttonStart.onClick.AddListener(ShowBagImage);
        bagButton.onClick.AddListener(ShowPuzzlePanel);

        // إخفاء العناصر بعد الربط
        imageClosedBag.SetActive(false);
        puzzlePanel.SetActive(false);
    }

    void ShowBagImage()
    {
        buttonStart.gameObject.SetActive(false);
        imageClosedBag.SetActive(true);
    }

    void ShowPuzzlePanel()
    {
        imageClosedBag.SetActive(false);
        puzzlePanel.SetActive(true);
    }
}
