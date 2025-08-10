using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class UITypewriterOnClick : MonoBehaviour, IPointerClickHandler
{
    [Header("TextMeshPro Target")]
    public TextMeshProUGUI textUI; // النص في واجهة المستخدم
    [TextArea] public string fullText; // النص الكامل
    public float typingSpeed = 0.04f; // سرعة الكتابة

    private bool isTyping = false;

    void Start()
    {
        // نخلي النص فاضي من البداية
        textUI.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isTyping) // ما يبدأ إلا إذا ما كان يكتب
            StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        isTyping = true;
        textUI.text = ""; // تأكيد أن النص يبدأ فاضي

        foreach (char letter in fullText)
        {
            textUI.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
