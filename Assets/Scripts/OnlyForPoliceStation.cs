using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalUICloser : MonoBehaviour
{
    public static GlobalUICloser Instance;

    [Header("Close button + grouping")]
    [SerializeField] private Button closeButton;
    [SerializeField] private float groupWindow = 0.05f;

    [Header("Fallback to Police Station")]
    [SerializeField] private GameObject policeStationCanvas;
    [SerializeField] private GameObject[] enableOnReturn;  // e.g. background, HUD, etc.
    [SerializeField] private GameObject[] disableOnReturn; // optional

    private readonly Stack<List<GameObject>> stack = new Stack<List<GameObject>>();
    private List<GameObject> currentGroup;
    private Coroutine groupCoro;

    void Awake()
    {
        Instance = this;
        if (closeButton) closeButton.onClick.AddListener(CloseTop);
        UpdateCloseButton();
    }

    public static void RegisterActive(GameObject go)
    {
        if (!Instance || !go) return;
        Instance.RegisterInternal(go);
    }

    private void RegisterInternal(GameObject go)
    {
        if (!go.activeInHierarchy) return;

        if (currentGroup == null)
        {
            currentGroup = new List<GameObject>();
            if (groupCoro != null) StopCoroutine(groupCoro);
            groupCoro = StartCoroutine(FinalizeGroupSoon());
        }

        if (!currentGroup.Contains(go))
            currentGroup.Add(go);
    }

    private IEnumerator FinalizeGroupSoon()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSecondsRealtime(groupWindow);

        if (currentGroup != null && currentGroup.Count > 0)
        {
            stack.Push(currentGroup);
            currentGroup = null;
            UpdateCloseButton();
        }
        groupCoro = null;
    }

    public void CloseTop()
    {
        if (stack.Count == 0) { UpdateCloseButton(); ActivateFallbackIfNeeded(); return; }

        var bundle = stack.Pop();
        foreach (var go in bundle)
        {
            if (go && go.activeSelf) go.SetActive(false);
        }

        UpdateCloseButton();
        ActivateFallbackIfNeeded();
    }

    public void ClearAll()
    {
        while (stack.Count > 0)
        {
            var b = stack.Pop();
            foreach (var go in b)
                if (go) go.SetActive(false);
        }
        UpdateCloseButton();
        ActivateFallbackIfNeeded();
    }

    private void UpdateCloseButton()
    {
        if (closeButton) closeButton.gameObject.SetActive(stack.Count > 0);
    }

    private void ActivateFallbackIfNeeded()
    {
        if (stack.Count > 0) return; // still overlays open

        if (policeStationCanvas) policeStationCanvas.SetActive(true);

        if (enableOnReturn != null)
            foreach (var go in enableOnReturn)
                if (go) go.SetActive(true);

        if (disableOnReturn != null)
            foreach (var go in disableOnReturn)
                if (go) go.SetActive(false);
    }

    // Optional: call this from anywhere to force return
    public void ReturnToPoliceNow()
    {
        ClearAll();
    }
}
