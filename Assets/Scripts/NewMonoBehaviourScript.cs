using System.Collections.Generic;
using UnityEngine;

public class UIAutoRegisterChildren : MonoBehaviour
{
    public bool includeSelf = false;   // يسجّل الأب نفسه لو صار Active
    public bool recursive   = true;    // يتابع كل الأحفاد ولا أولاد بس
    public int  checkEveryNFrames = 1; // كل كم فريم يفحص

    private readonly List<GameObject> nodes = new List<GameObject>();
    private readonly Dictionary<GameObject, bool> last = new Dictionary<GameObject, bool>();
    private int frame;

    void OnEnable()                 { Rebuild(); }
    void OnTransformChildrenChanged(){ Rebuild(); }

    void Rebuild()
    {
        nodes.Clear();
        last.Clear();

        if (includeSelf) nodes.Add(gameObject);

        if (recursive)
        {
            var rts = GetComponentsInChildren<RectTransform>(true);
            foreach (var rt in rts)
                if (rt && rt.gameObject != gameObject) nodes.Add(rt.gameObject);
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var go = transform.GetChild(i).gameObject;
                if (go.GetComponent<RectTransform>()) nodes.Add(go);
            }
        }

        foreach (var go in nodes)
            last[go] = go && go.activeInHierarchy;
    }

    void Update()
    {
        if (!GlobalUICloser.Instance) return;
        if (checkEveryNFrames > 1 && (++frame % checkEveryNFrames) != 0) return;

        for (int i = 0; i < nodes.Count; i++)
        {
            var go = nodes[i];
            if (!go) continue;

            bool cur = go.activeInHierarchy;
            bool prev = last.TryGetValue(go, out var p) ? p : false;

            if (cur && !prev)
                GlobalUICloser.RegisterActive(go);

            if (prev != cur)
                last[go] = cur;
        }
    }
}
