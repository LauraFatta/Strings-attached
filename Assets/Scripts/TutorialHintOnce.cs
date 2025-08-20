using UnityEngine;
using System.Collections;

public class TutorialHintOnce : MonoBehaviour
{
    public GameObject target;        
    [Header("Timings (seconds)")]
    public float fadeIn  = 0.5f;
    public float hold    = 1.2f;
    public float fadeOut = 0.25f;

    [Header("Options")]
    public bool autoPlayOnStart = true;   
    public bool useUnscaledTime = true;  
    private CanvasGroup cg;
    private Coroutine co;

    void Start()
    {
        if (autoPlayOnStart) Play();
    }

    public void Play()
    {
        if (target == null) return;
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        target.SetActive(true);

        cg = target.GetComponent<CanvasGroup>();
        if (cg == null) cg = target.AddComponent<CanvasGroup>();
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.alpha = 0f;

        // Fade In
        float t = 0f;
        while (t < fadeIn)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            cg.alpha = Mathf.Clamp01(t / Mathf.Max(0.0001f, fadeIn));
            yield return null;
        }
        cg.alpha = 1f;

        // Hold
        if (useUnscaledTime) yield return new WaitForSecondsRealtime(hold);
        else                 yield return new WaitForSeconds(hold);

        // Fade Out
        t = 0f;
        while (t < fadeOut)
        {
            t += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            cg.alpha = 1f - Mathf.Clamp01(t / Mathf.Max(0.0001f, fadeOut));
            yield return null;
        }
        cg.alpha = 0f;

        // رجّعه Inactive
        target.SetActive(false);
        co = null;
    }
}
