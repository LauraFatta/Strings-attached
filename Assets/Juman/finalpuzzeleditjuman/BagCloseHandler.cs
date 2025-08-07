using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BagCloseHandler : MonoBehaviour
{
    public GameObject openBagImage;         // صورة الشنطة المفتوحة
    public TextMeshProUGUI wordText;        // النص اللي يظهر
    public GameObject startButton;          // زر البداية
    public GameObject unlockedLockImage;    // صورة القفل المفتوح
    public GameObject puzzlePanel;          // البازل (con)
    public GameObject closedBagImage;       // صورة الشنطة المغلقة

    private bool textShown = false;

    void Start()
    {
        if (wordText != null)
            wordText.gameObject.SetActive(false);

        if (startButton != null)
            startButton.SetActive(false);
    }

    public void OnCloseButtonClick()
    {
        if (!textShown)
        {
            wordText.gameObject.SetActive(true);
            textShown = true;
        }
        else
        {
            // ⛔ إخفاء كل العناصر
            if (wordText != null)
                wordText.gameObject.SetActive(false);

            if (openBagImage != null)
                openBagImage.SetActive(false);

            if (unlockedLockImage != null)
                unlockedLockImage.SetActive(false);

            if (puzzlePanel != null)
                puzzlePanel.SetActive(false);

            if (closedBagImage != null)
                closedBagImage.SetActive(false);

            // ✅ إظهار زر البداية
            if (startButton != null)
                startButton.SetActive(true);

            textShown = false; // لإعادة الدورة من جديد
        }
    }
}
