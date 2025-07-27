using TMPro;
using UnityEngine;
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [TextArea] public string fullText = "go to the past ";
    public float letterDelay = 0.2f;

    void Start()
    {
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {
        textComponent.text = "";

        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
