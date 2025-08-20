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
    public GameObject nextUI;

    [SerializeField] public GameObject AllButton;
    [SerializeField] public GameObject ButtonToHide;

    public int[] correctCode = new int[3] { 3, 7, 6 };
    public int[] startingDigits = new int[3] { 0, 0, 0 };

    [Header("Feedback (optional)")]
    public AudioSource sfx;
    public AudioClip clickSfx;
    public AudioClip unlockSfx;
    public AudioClip wrongSfx;
    public RectTransform shakeTarget;
    public float shakeDuration = 0.08f;
    public float shakeStrength = 10f;

    private readonly int[] digits = new int[3] ;
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

        if (afterUnlockDelay > 0f)
            yield return new WaitForSeconds(afterUnlockDelay);

        if (puzzleGroup) puzzleGroup.SetActive(false);
        if (nextUI) nextUI.SetActive(true);

        if (AllButton) AllButton.SetActive(true);
        if (ButtonToHide) ButtonToHide.SetActive(false);
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
