// using UnityEngine;

// public class PuzzleManager : MonoBehaviour
// {
//     public SpriteRenderer[] numberImages;   // الخانات الثلاثة اللي تعرض الأرقام
//     public Sprite[] numberSprites;          // الصور من 0 إلى 9

//     public GameObject puzzleGroup;          // الجروب اللي فيه الخلفية + البازل كامل
//     public GameObject resultImage;          // صورة تظهر إذا الجواب صح (مثلاً قفل مفتوح)

//     private int[] digits = new int[3];      // الأرقام الحالية داخل الخانات

//     void Start()
//     {
//         UpdateNumbers();

//         if (resultImage != null)
//             resultImage.SetActive(false);   // نخفي صورة النجاح بالبداية
//     }

//     public void ChangeDigit(int index, int direction)
//     {
//         digits[index] += direction;

//         if (digits[index] > 9)
//             digits[index] = 0;
//         else if (digits[index] < 0)
//             digits[index] = 9;

//         UpdateNumbers();
//     }

//     void UpdateNumbers()
//     {
//         for (int i = 0; i < numberImages.Length; i++)
//         {
//             numberImages[i].sprite = numberSprites[digits[i]];
//         }
//     }

//     public void CheckResult()
//     {
//         if (digits[0] == 1 && digits[1] == 2 && digits[2] == 3)
//         {
//             Debug.Log("Right ✅");

//             if (puzzleGroup != null)
//                 puzzleGroup.SetActive(false);

//             if (resultImage != null)
//                 resultImage.SetActive(true);
//         }
//         else
//         {
//             Debug.Log("Wrong ❌");
//         }
//     }
// }




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class PuzzleManagerUI : MonoBehaviour
// {
//     [Header("Digit Images (left → right)")]
//     public Image[] numberImages;   // Digit_0/Number, Digit_1/Number_1, Digit_2/Number_2

//     [Header("Number Sprites (0..9 in order)")]
//     public Sprite[] numberSprites; // رتّبيها 0→9

//     [Header("Selective activation settings")]
//     [Tooltip("الجذر الذي سيتم داخله التفعيل الانتقائي (عادةً هو الـCanvas)")]
//     public Transform selectiveRoot;
//     [Tooltip("فعّل Canvas/GraphicRaycaster على الآباء إن كانت معطلة")]
//     public bool forceEnableCanvasComponents = true;

//     [Header("Extra toggles on open")]
//     public GameObject[] activateWhenNextUIOpens;   // عناصر تُفعَّل بعد الفتح (تضاف للقائمة البيضاء)
//     public GameObject[] deactivateWhenNextUIOpens; // عناصر تُطفأ بعد الفتح

//     [Header("Arrow Buttons")]
//     public Button upArrow_0, downArrow_0;
//     public Button upArrow_1, downArrow_1;
//     public Button upArrow_2, downArrow_2;

//     [Header("Check / Confirm")]
//     public Button checkButton;

//     [Header("Groups / Flow")]
//     public GameObject puzzleGroup;         // panel: buzzleroot
//     public RectTransform rightLockPiece;   // قطعة القفل اليمين
//     public float dropDistance = 120f;
//     public float dropDuration = 0.5f;
//     public float afterUnlockDelay = 2f;
//     public GameObject nextUI;              // اللوحة التالية

//     [Header("Code")]
//     public int[] correctCode    = new int[3] { 1, 2, 3 };
//     public int[] startingDigits = new int[3] { 0, 0, 0 };

//     [Header("Feedback (optional)")]
//     public AudioSource sfx;
//     public AudioClip clickSfx;
//     public AudioClip unlockSfx;
//     public AudioClip wrongSfx;
//     public RectTransform shakeTarget;
//     public float shakeDuration = 0.08f;
//     public float shakeStrength = 10f;

//     private readonly int[] digits = new int[3];
//     private bool isUnlocked = false;

//     void Awake()
//     {
//         if (numberImages == null || numberImages.Length < 3)
//             Debug.LogWarning("Assign 3 numberImages (left→right).");

//         for (int i = 0; i < 3; i++)
//             digits[i] = (startingDigits != null && startingDigits.Length > i) ? Mathf.Clamp(startingDigits[i], 0, 9) : 0;

//         UpdateNumbers();

//         if (upArrow_0)   upArrow_0.onClick.AddListener(() => ChangeDigit(0, +1));
//         if (downArrow_0) downArrow_0.onClick.AddListener(() => ChangeDigit(0, -1));
//         if (upArrow_1)   upArrow_1.onClick.AddListener(() => ChangeDigit(1, +1));
//         if (downArrow_1) downArrow_1.onClick.AddListener(() => ChangeDigit(1, -1));
//         if (upArrow_2)   upArrow_2.onClick.AddListener(() => ChangeDigit(2, +1));
//         if (downArrow_2) downArrow_2.onClick.AddListener(() => ChangeDigit(2, -1));
//         if (checkButton) checkButton.onClick.AddListener(CheckResult);

//         if (nextUI) nextUI.SetActive(false);
//         if (!shakeTarget && puzzleGroup) shakeTarget = puzzleGroup.GetComponent<RectTransform>();
//     }

//     public void ChangeDigit(int index, int direction)
//     {
//         if (isUnlocked) return;
//         if (index < 0 || index > 2) return;

//         digits[index] = (digits[index] + (direction > 0 ? 1 : 9)) % 10;
//         Play(clickSfx);
//         UpdateNumbers();
//     }

//     private void UpdateNumbers()
//     {
//         for (int i = 0; i < 3 && i < numberImages.Length; i++)
//         {
//             if (!numberImages[i]) continue;
//             int d = Mathf.Clamp(digits[i], 0, 9);
//             if (numberSprites != null && numberSprites.Length > d && numberSprites[d])
//                 numberImages[i].sprite = numberSprites[d];
//         }
//     }

//     public void CheckResult()
//     {
//         if (isUnlocked) return;

//         bool ok = true;
//         for (int i = 0; i < 3; i++)
//         {
//             int target = (correctCode != null && correctCode.Length > i) ? Mathf.Clamp(correctCode[i], 0, 9) : 0;
//             if (digits[i] != target) { ok = false; break; }
//         }

//         if (ok)
//         {
//             StartCoroutine(UnlockSequence());
//         }
//         else
//         {
//             Play(wrongSfx);
//             if (shakeTarget) StartCoroutine(Shake(shakeTarget, shakeDuration, shakeStrength));
//         }
//     }

//     private IEnumerator UnlockSequence()
//     {
//         isUnlocked = true;

//         SetButtonsInteractable(false);
//         Play(unlockSfx);

//         yield return StartCoroutine(DropRightLock());

//         if (afterUnlockDelay > 0f)
//             yield return new WaitForSeconds(afterUnlockDelay);

//         // —— التفعيل الانتقائي داخل selectiveRoot —— //
//         // نبني قائمة أهداف التفعيل (nextUI + أي عناصر إضافية)
//         List<GameObject> targets = new List<GameObject>();
//         if (nextUI) targets.Add(nextUI);
//         if (activateWhenNextUIOpens != null)
//             foreach (var go in activateWhenNextUIOpens) if (go) targets.Add(go);

//         // فعّل فقط ما في القائمة داخل selectiveRoot، وأبقِ باقي الأشقاء مطفأة
//         SelectiveActivateWithinRoot(targets, selectiveRoot);

//         // أطفئ عناصر إضافية (خارج/داخل الجذر) لو طلبتِ
//         ToggleList(deactivateWhenNextUIOpens, false);

//         // أخفِ واجهة القفل
//         if (puzzleGroup) puzzleGroup.SetActive(false);
//     }

//     private IEnumerator DropRightLock()
//     {
//         if (!rightLockPiece || dropDuration <= 0f) yield break;

//         Vector2 start = rightLockPiece.anchoredPosition;
//         Vector2 target = start + new Vector2(0f, -Mathf.Abs(dropDistance));

//         float t = 0f;
//         while (t < dropDuration)
//         {
//             t += Time.unscaledDeltaTime;
//             float k = Mathf.Clamp01(t / dropDuration);
//             float ease = 1f - Mathf.Pow(1f - k, 3f);
//             rightLockPiece.anchoredPosition = Vector2.LerpUnclamped(start, target, ease);
//             yield return null;
//         }
//         rightLockPiece.anchoredPosition = target;
//     }

//     private IEnumerator Shake(RectTransform rt, float duration, float strength)
//     {
//         if (!rt || duration <= 0f) yield break;

//         Vector2 original = rt.anchoredPosition;
//         float t = 0f;
//         while (t < duration)
//         {
//             t += Time.unscaledDeltaTime;
//             rt.anchoredPosition = original + Random.insideUnitCircle * strength;
//             yield return null;
//         }
//         rt.anchoredPosition = original;
//     }

//     private void SetButtonsInteractable(bool v)
//     {
//         if (upArrow_0) upArrow_0.interactable = v;
//         if (downArrow_0) downArrow_0.interactable = v;
//         if (upArrow_1) upArrow_1.interactable = v;
//         if (downArrow_1) downArrow_1.interactable = v;
//         if (upArrow_2) upArrow_2.interactable = v;
//         if (downArrow_2) downArrow_2.interactable = v;
//         if (checkButton) checkButton.interactable = v;
//     }

//     private void ToggleList(GameObject[] list, bool state)
//     {
//         if (list == null) return;
//         foreach (var go in list)
//             if (go) go.SetActive(state);
//     }

//     // ===================== التفعيل الانتقائي =====================

//     /// <summary>
//     /// يفعّل فقط العناصر المطلوبة داخل جذر محدد (selectiveRoot).
//     /// يضمن تفعيل سلسلة الآباء، ثم يطفئ أي أشقاء خارج القائمة البيضاء.
//     /// </summary>
//     private void SelectiveActivateWithinRoot(List<GameObject> targets, Transform root)
//     {
//         if (targets == null || targets.Count == 0) return;

//         // لو ما حددتي جذر، نحاول استخدام أقرب Canvas للأهداف
//         if (root == null)
//         {
//             foreach (var g in targets)
//             {
//                 var c = g ? g.GetComponentInParent<Canvas>(true) : null;
//                 if (c) { root = c.transform; break; }
//             }
//         }
//         if (root == null)
//         {
//             Debug.LogWarning("SelectiveActivateWithinRoot: no selectiveRoot found.");
//             // fallback: فعّل الأهداف مباشرة
//             foreach (var g in targets) if (g) g.SetActive(true);
//             return;
//         }

//         // 1) ابنِ قائمة بيضاء: كل المسارات من كل هدف حتى الجذر
//         var whitelist = new HashSet<Transform>();
//         foreach (var g in targets)
//             AddPathToRoot(g ? g.transform : null, root, whitelist);

//         // ضيفي الجذر نفسه
//         whitelist.Add(root);

//         // 2) فعّل جميع العناصر في القائمة البيضاء من الجذر إلى الأوراق
//         foreach (var tr in whitelist)
//         {
//             if (!tr) continue;
//             EnsureOn(tr.gameObject);
//         }

//         // 3) على كل "مستوى" داخل الجذر: أطفي أي طفل ليس في القائمة البيضاء
//         // نمشي BFS ابتداءً من الجذر
//         var queue = new Queue<Transform>();
//         queue.Enqueue(root);

//         while (queue.Count > 0)
//         {
//             var current = queue.Dequeue();
//             if (!current) continue;

//             for (int i = 0; i < current.childCount; i++)
//             {
//                 var child = current.GetChild(i);

//                 if (whitelist.Contains(child))
//                 {
//                     // هذا ضمن المسار/المطلوب: خلّه شغّال وكمّل نزول
//                     EnsureOn(child.gameObject);
//                     queue.Enqueue(child);
//                 }
//                 else
//                 {
//                     // هذا أخ/فرع خارج المطلوب: أطفيه إن اشتغل
//                     if (child.gameObject.activeSelf)
//                         child.gameObject.SetActive(false);
//                 }
//             }
//         }

//         // 4) فعّل الأهداف نفسها صراحةً (لو بقيت مطفّية لأي سبب)
//         foreach (var g in targets) if (g) g.SetActive(true);
//     }

//     private void AddPathToRoot(Transform leaf, Transform root, HashSet<Transform> set)
//     {
//         var t = leaf;
//         while (t != null)
//         {
//             set.Add(t);
//             if (t == root) break;
//             t = t.parent;
//         }
//     }

//     private void EnsureOn(GameObject go)
//     {
//         if (!go) return;
//         if (!go.activeSelf) go.SetActive(true);

//         if (forceEnableCanvasComponents)
//         {
//             var canvas = go.GetComponent<Canvas>();
//             if (canvas && !canvas.enabled) canvas.enabled = true;

//             var gr = go.GetComponent<GraphicRaycaster>();
//             if (gr && !gr.enabled) gr.enabled = true;
//         }
//     }

//     // ===================== /التفعيل الانتقائي =====================

//     private void Play(AudioClip clip)
//     {
//         if (sfx && clip) sfx.PlayOneShot(clip);
//     }
// }




using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManagerUI : MonoBehaviour
{
    [Header("Digit Images (left → right)")]
    public Image[] numberImages;   
    [Header("Number Sprites (0..9 in order)")]
    public Sprite[] numberSprites;

    [Header("Arrow Buttons")]
    public Button upArrow_0, downArrow_0;
    public Button upArrow_1, downArrow_1;
    public Button upArrow_2, downArrow_2;

    [Header("Check / Confirm")]
    public Button checkButton;

    [Header("Groups / Flow")]
    public GameObject puzzleGroup;         
    public RectTransform rightLockPiece;   
        public float dropDistance = 120f;     
    public float dropDuration = 0.5f;      
    public float afterUnlockDelay = 2.0f;  
    public GameObject resultImage;         
    public GameObject nextUI;              

       public int[] correctCode = new int[3] { 1, 2, 3 };
        public int[] startingDigits = new int[3] { 0, 0, 0 };

    [Header("Feedback (optional)")]
    public AudioSource sfx;
    public AudioClip clickSfx;
    public AudioClip unlockSfx;
    public AudioClip wrongSfx;
    public RectTransform shakeTarget;      
    public float shakeDuration = 0.08f;
    public float shakeStrength = 10f;

    private readonly int[] digits = new int[3];
    private bool isUnlocked = false;

    void Awake()
    {
        if (numberImages == null || numberImages.Length < 3)
            Debug.LogWarning("Assign 3 numberImages (left→right).");

        for (int i = 0; i < 3; i++)
            digits[i] = (startingDigits != null && startingDigits.Length > i) ? Mathf.Clamp(startingDigits[i], 0, 9) : 0;

        UpdateNumbers();

        if (upArrow_0)   upArrow_0.onClick.AddListener(() => ChangeDigit(0, +1));
        if (downArrow_0) downArrow_0.onClick.AddListener(() => ChangeDigit(0, -1));
        if (upArrow_1)   upArrow_1.onClick.AddListener(() => ChangeDigit(1, +1));
        if (downArrow_1) downArrow_1.onClick.AddListener(() => ChangeDigit(1, -1));
        if (upArrow_2)   upArrow_2.onClick.AddListener(() => ChangeDigit(2, +1));
        if (downArrow_2) downArrow_2.onClick.AddListener(() => ChangeDigit(2, -1));
        if (checkButton) checkButton.onClick.AddListener(CheckResult);

        if (resultImage) resultImage.SetActive(false);
        if (nextUI) nextUI.SetActive(false);
        if (!shakeTarget && puzzleGroup) shakeTarget = puzzleGroup.GetComponent<RectTransform>();
    }

    public void ChangeDigit(int index, int direction)
    {
        if (isUnlocked) return;
        if (index < 0 || index > 2) return;

        digits[index] = (digits[index] + (direction > 0 ? 1 : 9)) % 10;
        Play(clickSfx);
        UpdateNumbers();
    }

    private void UpdateNumbers()
    {
        for (int i = 0; i < 3 && i < numberImages.Length; i++)
        {
            if (!numberImages[i]) continue;

            int d = Mathf.Clamp(digits[i], 0, 9);
            if (numberSprites != null && numberSprites.Length > d && numberSprites[d])
                numberImages[i].sprite = numberSprites[d];
        }
    }

    public void CheckResult()
    {
        if (isUnlocked) return;

        bool ok = true;
        for (int i = 0; i < 3; i++)
        {
            int target = (correctCode != null && correctCode.Length > i) ? Mathf.Clamp(correctCode[i], 0, 9) : 0;
            if (digits[i] != target) { ok = false; break; }
        }

        if (ok)
        {
            StartCoroutine(UnlockSequence());
        }
        else
        {
            Play(wrongSfx);
            if (shakeTarget) StartCoroutine(Shake(shakeTarget, shakeDuration, shakeStrength));
        }
    }

    private IEnumerator UnlockSequence()
    {
        isUnlocked = true;

        SetButtonsInteractable(false);
        Play(unlockSfx);

        yield return StartCoroutine(DropRightLock());

        if (resultImage) resultImage.SetActive(true);

    
        if (afterUnlockDelay > 0f)
            yield return new WaitForSeconds(afterUnlockDelay);

        if (puzzleGroup) puzzleGroup.SetActive(false);
        if (nextUI) nextUI.SetActive(true);
    }

    private IEnumerator DropRightLock()
    {
        if (!rightLockPiece || dropDuration <= 0f) yield break;

        Vector2 start = rightLockPiece.anchoredPosition;
        Vector2 target = start + new Vector2(0f, -Mathf.Abs(dropDistance));

        float t = 0f;
        while (t < dropDuration)
        {
            t += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(t / dropDuration);
            float ease = 1f - Mathf.Pow(1f - k, 3f);
            rightLockPiece.anchoredPosition = Vector2.LerpUnclamped(start, target, ease);
            yield return null;
        }
        rightLockPiece.anchoredPosition = target;
    }

    private IEnumerator Shake(RectTransform rt, float duration, float strength)
    {
        if (!rt || duration <= 0f) yield break;

        Vector2 original = rt.anchoredPosition;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            rt.anchoredPosition = original + Random.insideUnitCircle * strength;
            yield return null;
        }
        rt.anchoredPosition = original;
    }

    private void SetButtonsInteractable(bool v)
    {
        if (upArrow_0) upArrow_0.interactable = v;
        if (downArrow_0) downArrow_0.interactable = v;
        if (upArrow_1) upArrow_1.interactable = v;
        if (downArrow_1) downArrow_1.interactable = v;
        if (upArrow_2) upArrow_2.interactable = v;
        if (downArrow_2) downArrow_2.interactable = v;
        if (checkButton) checkButton.interactable = v;
    }

    private void Play(AudioClip clip)
    {
        if (sfx && clip) sfx.PlayOneShot(clip);
    }
}
