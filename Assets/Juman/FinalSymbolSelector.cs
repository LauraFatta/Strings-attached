using UnityEngine;
using UnityEngine.UI;

public class FinalSymbolSelector : MonoBehaviour
{
    public Sprite[] symbols;          // الرموز المتاحة (مثل: دائرة، مثلث، مربع، نجمة، قلب...)
    public Image[] symbolSlots;       // خانات العرض الثلاثة
    public Text resultText;           // لعرض النتيجة

    private int[] currentIndices = new int[3]; // الرمز الحالي لكل خانة

    // ✅ الترتيب الصحيح: دائرة (0)، مثلث (1)، مربع (2)
    [SerializeField]private int[] correctIndices = new int[] { 0, 1, 2 };

    void Start()
    {
        // نبدأ بعرض أول رمز في كل خانة
        for (int i = 0; i < 3; i++)
        {
            currentIndices[i] = 0;
            UpdateSlot(i);
        }

        resultText.text = "";
    }

    // 🔼 زيادة الرمز في خانة معينة
    public void Next(int slot)
    {
        currentIndices[slot] = (currentIndices[slot] + 1) % symbols.Length;
        UpdateSlot(slot);
    }

    // 🔽 تقليل الرمز في خانة معينة
    public void Prev(int slot)
    {
        currentIndices[slot] = (currentIndices[slot] - 1 + symbols.Length) % symbols.Length;
        UpdateSlot(slot);
    }

    // تحديث صورة الخانة حسب الرمز الحالي
    void UpdateSlot(int slot)
    {
        symbolSlots[slot].sprite = symbols[currentIndices[slot]];
        resultText.text = "";
    }

    // ✅ التحقق من الترتيب
    public void CheckSymbols()
    {
        for (int i = 0; i < 3; i++)
        {
            if (currentIndices[i] != correctIndices[i])
            {
                resultText.text = "Wrong";
                resultText.color = Color.red;
                return;
            }
        }

        resultText.text = "Correct";
        resultText.color = Color.green;
    }
}
