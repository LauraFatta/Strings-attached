using UnityEngine;
using UnityEngine.UI;

public class CanvasShowOnlyOne : MonoBehaviour
{
    public Canvas[] canvases;

    public bool focusSortingOnTarget = true;

    public bool affectAllSceneCanvases = false;

    public int focusStartOrder = 100;

    private static int sNextOrder = 0;

    void Awake()
    {
        if (sNextOrder < focusStartOrder) sNextOrder = focusStartOrder;
    }

    public void Show(Canvas target)
    {
        if (!target) return;

        foreach (var c in canvases)
        {
            if (!c) continue;

            bool isTarget = (c == target);

            c.enabled = isTarget;

            var gr = c.GetComponent<GraphicRaycaster>();
            if (gr) gr.enabled = isTarget;

            var cg = c.GetComponent<CanvasGroup>();
            if (!cg) cg = c.gameObject.AddComponent<CanvasGroup>();
            cg.blocksRaycasts = isTarget;
            cg.interactable   = isTarget;
        }

        if (focusSortingOnTarget)
        {
            if (affectAllSceneCanvases)
            {
                var all = Canvas.FindObjectsOfType<Canvas>(true);
                foreach (var c in all)
                {
                    if (!c) continue;
                    c.overrideSorting = false;
                    c.sortingOrder = 0;
                }
            }
            else
            {
                foreach (var c in canvases)
                {
                    if (!c) continue;
                    c.overrideSorting = false;
                    c.sortingOrder = 0;
                }
            }

            target.overrideSorting = true;
            target.sortingOrder = ++sNextOrder;
            target.transform.SetAsLastSibling();
        }
    }

    public void Show(GameObject targetCanvasGO)
    {
        if (!targetCanvasGO) return;
        var cv = targetCanvasGO.GetComponent<Canvas>();
        if (cv) Show(cv);
    }

    public void ShowByIndex(int index)
    {
        if (canvases == null || index < 0 || index >= canvases.Length) return;
        Show(canvases[index]);
    }
}
