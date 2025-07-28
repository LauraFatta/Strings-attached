using UnityEngine;
using UnityEngine.UI;

public class SimplePuzzleController : MonoBehaviour
{
    public Image[] slots;              // الخانات الثلاثة (الصور اللي تتغير)
    public Sprite[] digitSprites;     // صور الأرقام من 0 إلى 9
    private int[] currentDigits;      // الأرقام الحالية داخل الخانات

    public Text resultText;           // النص اللي يظهر "صح" أو "غلط"
    public GameObject successImage;   // ✅ الصورة اللي تظهر عند الحل الصحيح

    void Start()
    {
        currentDigits = new int[3]; // 3 خانات
        UpdateSlotImages();
        resultText.text = "";

        if (successImage != null)
            successImage.SetActive(false); // نخفي الصورة بالبداية
    }

    public void ChangeDigit(int slotIndex, int delta)
    {
        currentDigits[slotIndex] += delta;

        if (currentDigits[slotIndex] > 9) currentDigits[slotIndex] = 0;
        if (currentDigits[slotIndex] < 0) currentDigits[slotIndex] = 9;

        UpdateSlotImages();
    }

    public void CheckCode()
    {
        if (currentDigits[0] == 1 && currentDigits[1] == 2 && currentDigits[2] == 3)
        {
            resultText.text = "right";

            if (successImage != null)
                successImage.SetActive(true); // نُظهر الصورة
        }
        else
        {
            resultText.text = "wrong";

            if (successImage != null)
                successImage.SetActive(false); // نخفيها لو غلط
        }
    }

    private void UpdateSlotImages()
    {
        for (int i = 0; i < 3; i++)
        {
            slots[i].sprite = digitSprites[currentDigits[i]];
        }
    }

    // 🔽⬆️ دوال الأسهم ↑ ↓ للخانات الثلاثة
    public void IncreaseSlot0() => ChangeDigit(0, 1);
    public void DecreaseSlot0() => ChangeDigit(0, -1);

    public void IncreaseSlot1() => ChangeDigit(1, 1);
    public void DecreaseSlot1() => ChangeDigit(1, -1);

    public void IncreaseSlot2() => ChangeDigit(2, 1);
    public void DecreaseSlot2() => ChangeDigit(2, -1);
}
