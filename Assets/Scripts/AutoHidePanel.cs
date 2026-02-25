using UnityEngine;
using System.Collections;

public class AutoHidePanel : MonoBehaviour
{
    [SerializeField] float showTime = 2f;
    [SerializeField] float popDuration = 0.15f;
    [SerializeField] float fadeOutDuration = 0.25f;
    [SerializeField] float popScaleFrom = 0.88f;   
    [SerializeField] bool  useUnscaledTime = true;

    CanvasGroup cg;
    RectTransform rt;
    Vector3 baseScale;
    Coroutine co;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        baseScale = rt ? rt.localScale : Vector3.one;
        cg = GetComponent<CanvasGroup>();
        if (!cg) cg = gameObject.AddComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        if (co != null) StopCoroutine(co);
        co = StartCoroutine(Sequence());
    }

    void OnDisable()
    {
        if (co != null) StopCoroutine(co);
        cg.alpha = 1f; cg.blocksRaycasts = true; cg.interactable = true;
        if (rt) rt.localScale = baseScale;
    }

    IEnumerator Sequence()
    {
        // POP-IN
        cg.alpha = 0f; cg.blocksRaycasts = true; cg.interactable = true;
        if (rt) rt.localScale = baseScale * popScaleFrom;

        float t = 0f;
        while (t < popDuration)
        {
            t += Delta();
            float k = Mathf.Clamp01(t / popDuration);
            float e = OutBack(k); 
            cg.alpha = k;
            if (rt)
            {
                float s = popScaleFrom + (1f - popScaleFrom) * e; 
                rt.localScale = baseScale * s;
            }
            yield return null;
        }
        cg.alpha = 1f; if (rt) rt.localScale = baseScale;

        // WAIT
        yield return Wait(showTime);

        // FADE-OUT
        cg.interactable = false;
        t = 0f;
        while (t < fadeOutDuration)
        {
            t += Delta();
            float k = Mathf.Clamp01(t / fadeOutDuration);
            cg.alpha = 1f - k;
            if (k >= 0.6f) cg.blocksRaycasts = false; 
            if (rt) rt.localScale = baseScale * (1f - 0.02f * k); 
            yield return null;
        }
        cg.alpha = 0f; cg.blocksRaycasts = false; cg.interactable = false;
        co = null;
    }

    public void Restart(float newShowTime = -1f)
    {
        if (newShowTime >= 0f) showTime = newShowTime;
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        else { if (co != null) StopCoroutine(co); co = StartCoroutine(Sequence()); }
    }

    // Helpers
    float Delta() => useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
    IEnumerator Wait(float secs)
    {
        if (useUnscaledTime) yield return new WaitForSecondsRealtime(secs);
        else yield return new WaitForSeconds(secs);
    }
    static float OutBack(float x)
    {
        const float s = 1.70158f;
        float a = x - 1f;
        return 1f + (s + 1f) * a * a * a + s * a * a;
    }
}
