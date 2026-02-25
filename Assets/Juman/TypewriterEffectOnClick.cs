// using UnityEngine;
// using UnityEngine.EventSystems;
// using TMPro;
// using System.Collections;

// public class UITypewriterOnClick : MonoBehaviour, IPointerClickHandler
// {
//     [Header("TextMeshPro Target")]
//     public TextMeshProUGUI textUI; // النص في واجهة المستخدم
//     [TextArea] public string fullText; // النص الكامل
//     public float typingSpeed = 0.04f; // سرعة الكتابة

//     private bool isTyping = false;

//     void Start()
//     {
//         // نخلي النص فاضي من البداية
//         textUI.text = "";
//     }

//     public void OnPointerClick(PointerEventData eventData)
//     {
//         if (!isTyping) // ما يبدأ إلا إذا ما كان يكتب
//             StartCoroutine(TypeText());
//     }

//     IEnumerator TypeText()
//     {
//         isTyping = true;
//         textUI.text = ""; // تأكيد أن النص يبدأ فاضي

//         foreach (char letter in fullText)
//         {
//             textUI.text += letter;
//             yield return new WaitForSeconds(typingSpeed);
//         }
//     }
// }


using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections;

public class UITypewriterAuto : MonoBehaviour, IPointerClickHandler
{
    [Header("TextMeshPro Target")]
    [SerializeField] private TextMeshProUGUI textUI;  
    [TextArea] [SerializeField] private string fullText;
    [SerializeField] private float typingSpeed = 0.1f; 

    private bool isTyping = false;

    private Coroutine typingRoutine;

    void OnEnable()
    {
        StartTyping();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isTyping)
        {
            SkipToEnd();
        }
        
    }

    private void StartTyping()
    {
        if (textUI == null) { Debug.LogWarning("[UITypewriterAuto] textUI غير معيّن."); return; }
        if (string.IsNullOrEmpty(fullText)) { Debug.LogWarning("[UITypewriterAuto] fullText فاضي."); return; }

        if (typingRoutine != null) StopCoroutine(typingRoutine);
        typingRoutine = StartCoroutine(TypeTextRealtime());
    }

    private IEnumerator TypeTextRealtime()
    {
        isTyping = true;
        textUI.text = "";

        foreach (char letter in fullText)
        {
            textUI.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
    }

    private void SkipToEnd()
    {
        if (typingRoutine != null) StopCoroutine(typingRoutine);
        textUI.text = fullText;
        isTyping = false;
    }
}
