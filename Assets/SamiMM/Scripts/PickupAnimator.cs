using UnityEngine;
using System.Collections;

public class PickupAnimator : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float animDuration = 1f;
    [SerializeField] private AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private bool isScalingDown;
    private float scaleTimer;
    private Vector3 startScale, targetScale;

    private void Start()
    {
        if (!TryGetComponent(out anim))
            Debug.LogError($"No animator found on {gameObject.name}");
        InitializeAnimation();
    }

    private void InitializeAnimation()
    {
        StartCoroutine(MoveToInventory());
    }

    private IEnumerator MoveToInventory()
    {
        // initial "pop" animation, then delay
        yield return new WaitForSeconds(.8f);
        anim.SetTrigger("Float");
        Vector3 endPos;
        if (GetComponentInParent<RectTransform>())
            endPos = FindInventoryButton().position;
        else
            endPos = UIHelper.GetWorldPositionOfOverlayUI(FindInventoryButton(), Camera.main);
        Vector3 startPos = transform.position;
        float elapsed = 0f;
        isScalingDown = true;
        scaleTimer = 0f;
        startScale = transform.localScale;
        targetScale = Vector3.zero;

        while (elapsed < animDuration)
        {
            float t = elapsed / animDuration;
            float easeT = moveCurve.Evaluate(t);
            transform.position = Vector3.Lerp(startPos, endPos, easeT);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (!isScalingDown)
            return;
        scaleTimer += Time.deltaTime;
        float t = Mathf.Clamp01(scaleTimer / animDuration);
        float easeT = moveCurve.Evaluate(t);
        transform.localScale = Vector3.Lerp(startScale, targetScale, easeT);
    }

    private RectTransform FindInventoryButton()
    {
        var invUI = FindFirstObjectByType<InventoryUI>();
        if (invUI == null)
        {
            Debug.LogError("Assign the InventoryUI component in your hierarchy!");
            return null;
        }

        foreach (var btn in FindObjectsByType<UnityEngine.UI.Button>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            var unityEvent = btn.onClick;
            int count = unityEvent.GetPersistentEventCount();
            for (int i = 0; i < count; i++)
            {
                if (unityEvent.GetPersistentMethodName(i) != nameof(InventoryUI.ToggleInventory))
                    continue;
                if (unityEvent.GetPersistentTarget(i) as InventoryUI != invUI)
                    continue;
                Debug.Log($"Found inventory button: {btn.gameObject.name}");
                return btn.GetComponent<RectTransform>();
            }
        }
        Debug.LogWarning("No Button hooking up InventoryUI.ToggleInventory() was found.");
        return null;
    }
}

public static class UIHelper
{
    public static Vector3 GetWorldPositionOfOverlayUI(RectTransform uiElement, Camera worldCam)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, uiElement.position);
        float zDepth = Mathf.Abs(worldCam.transform.position.z);
        return worldCam.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, zDepth));
    }
}