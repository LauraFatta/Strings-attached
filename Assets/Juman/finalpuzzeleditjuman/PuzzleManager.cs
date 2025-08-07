using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public SpriteRenderer[] numberImages;   // الخانات الثلاثة اللي تعرض الأرقام
    public Sprite[] numberSprites;          // الصور من 0 إلى 9

    public GameObject puzzleGroup;          // الجروب اللي فيه الخلفية + البازل كامل
    public GameObject resultImage;          // صورة تظهر إذا الجواب صح (مثلاً قفل مفتوح)

    private int[] digits = new int[3];      // الأرقام الحالية داخل الخانات

    void Start()
    {
        UpdateNumbers();

        if (resultImage != null)
            resultImage.SetActive(false);   // نخفي صورة النجاح بالبداية
    }

    public void ChangeDigit(int index, int direction)
    {
        digits[index] += direction;

        if (digits[index] > 9)
            digits[index] = 0;
        else if (digits[index] < 0)
            digits[index] = 9;

        UpdateNumbers();
    }

    void UpdateNumbers()
    {
        for (int i = 0; i < numberImages.Length; i++)
        {
            numberImages[i].sprite = numberSprites[digits[i]];
        }
    }

    public void CheckResult()
    {
        if (digits[0] == 1 && digits[1] == 2 && digits[2] == 3)
        {
            Debug.Log("Right ✅");

            if (puzzleGroup != null)
                puzzleGroup.SetActive(false);

            if (resultImage != null)
                resultImage.SetActive(true);
        }
        else
        {
            Debug.Log("Wrong ❌");
        }
    }
}
