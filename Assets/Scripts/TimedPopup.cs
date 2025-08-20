using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialHintFader : MonoBehaviour
{
    [SerializeField] private GameObject shapesRoot;

    [SerializeField] private float fadeInDuration = 0.6f;
    [SerializeField] private float holdDuration   = 1.5f;
    [SerializeField] private float fadeOutDuration = 0.3f; 
    [Header("Options")]
    [SerializeField] private bool playOnStart = false;     
    [SerializeField] private bool setInactiveAtEnd = true;  
    private CanvasGroup cg;
    private Coroutine routine;

    void Awake()
    {
        if (shapesRoot == null) shapesRoot = gameObject;
        EnsureCanvasGroup();
        SetAlpha(0f);

        void Start()
        {
            if (playOnStart) Play();
        }
    }

    public void Play()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(FadeRoutine());
    }

    public void StopAndHide()
    {
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(FadeOutAndHideImmediate());
    }

    private IEnumerator FadeRoutine()
    {
        shapesRoot.SetActive(true);
        SetAlpha(0f);

        // Fade In
        yield return StartCoroutine(FadeTo(1f, fadeInDuration));

        yield return new WaitForSecondsRealtime(holdDuration);

        // Fade Out
        yield return StartCoroutine(FadeTo(0f, fadeOutDuration));

        if (setInactiveAtEnd) shapesRoot.SetActive(false); 
        routine = null;
    }

    private IEnumerator FadeOutAndHideImmediate()
    {
        yield return StartCoroutine(FadeTo(0f, fadeOutDuration));
        if (setInactiveAtEnd) shapesRoot.SetActive(false);
        routine = null;
    }

    private IEnumerator FadeTo(float target, float duration)
    {
        float start = GetCurrentAlpha();
        if (duration <= 0f) { SetAlpha(target); yield break; }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / duration; 
            SetAlpha(Mathf.Lerp(start, target, t));
            yield return null;
        }
        SetAlpha(target);
    }

    private void EnsureCanvasGroup()
    {
        cg = shapesRoot.GetComponent<CanvasGroup>();
        if (cg == null) cg = shapesRoot.AddComponent<CanvasGroup>();
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.alpha = 0f;
    }

    private float GetCurrentAlpha()
    {
        if (cg != null) return cg.alpha;
        return 1f;
    }

    private void SetAlpha(float a)
    {
        if (cg != null)
        {
            cg.alpha = a;
        }
        else
        {
            foreach (var g in shapesRoot.GetComponentsInChildren<Graphic>(true))
            {
                var c = g.color; c.a = a; g.color = c;
            }
            foreach (var sr in shapesRoot.GetComponentsInChildren<SpriteRenderer>(true))
            {
                var c = sr.color; c.a = a; sr.color = c;
            }
        }
    }
}
