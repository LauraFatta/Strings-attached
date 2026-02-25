using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
public class Pickup : MonoBehaviour, IPointerDownHandler
{
    public GameItem gameItem;
    public static List<Pickup> activePickups = new List<Pickup>();

    private Inventory inventory;
    private Renderer objectRenderer;
    private Image objectImage;

    private bool isCollected = false;
    public bool isMarkedForLinking = false;
    public bool IsMarkedForLinking => isMarkedForLinking;

    private void OnEnable()
    {
        activePickups.Add(this);
    }

    private void OnDisable()
    {
        activePickups.Remove(this);
    }

    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        objectRenderer = GetComponent<Renderer>();
        objectImage = GetComponent<Image>();
	}
    public void Clone()
    {
		TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
		if (tmp == null)
			tmp = GetComponentInChildren<TextMeshProUGUI>();

		if (tmp != null)
		{
			// Duplicate the GameObject containing the TMP
			GameObject clone = Instantiate(tmp.gameObject, tmp.transform.position, tmp.transform.rotation, tmp.transform.parent);

			// Remove ALL other components except Transform + TextMeshProUGUI
			foreach (var comp in clone.GetComponents<Component>())
			{
				if (!(comp is Transform) && !(comp is TextMeshProUGUI) && !(comp is CanvasRenderer))
				{
					DestroyImmediate(comp);
				}
			}

			// Optionally: also remove children (if you want ONLY the text object)
			for (int i = clone.transform.childCount - 1; i >= 0; i--)
			{
				DestroyImmediate(clone.transform.GetChild(i).gameObject);
			}

			// Make sure the clone does not block clicks/raycast
			var cloneTMP = clone.GetComponent<TextMeshProUGUI>();
			if (cloneTMP != null)
				cloneTMP.raycastTarget = false;
		}
	}
    public static bool HasAnyMarked()
    {
        foreach (Pickup p in activePickups)
        {
            if (p != null && p.isMarkedForLinking)
            {
                return true;
            }
        }
        return false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryUI ui = FindObjectOfType<InventoryUI>();
        if (ui != null)
        {
            ui.NotifyPickupClick();
        }

        if (gameItem.itemType == ItemType.ComponentOnly)
        {
            if (!isCollected)
            {
                if (inventory.AddItem(gameItem))
                {
                    isCollected = true;
                    PlayPopAnimation();

                    if (SaveSystem.instance != null)
                        SaveSystem.instance.MarkItemCollected(gameItem);
                }
            }
            else if (!isMarkedForLinking)
            {
                isMarkedForLinking = true;
                ShowUncollectedVisual(true);
                Inventory.Instance.TryBuildComposite();
            }
            return;
        }

        // if (inventory.AddItem(gameItem))
        // {
        //     if (SaveSystem.instance != null)
        //         SaveSystem.instance.MarkItemCollected(gameItem);

        //     var pref = Resources.Load<GameObject>("Prefabs/sns/PickupAnimator");
        //     if (pref != null)
        //     {
        //         var animatorObj = Instantiate(pref, transform.position, Quaternion.identity);
        //         transform.SetParent(animatorObj.transform);
        //         Destroy(this);
        //     }
        //     else
        //     {
        //         Destroy(gameObject);
        //     }
        // }
        if (inventory.AddItem(gameItem))
        {
            if (SaveSystem.instance != null)
                SaveSystem.instance.MarkItemCollected(gameItem);

			Canvas parentCanvas = GetComponentInParent<Canvas>();

			// Check if the item should disappear on pickup
			
                GameObject pref;

                if (parentCanvas != null)
					pref = Resources.Load<GameObject>("Prefabs/sns/PickupAnimatorUI");
                else
                    pref = Resources.Load<GameObject>("Prefabs/sns/PickupAnimator");

                if (pref != null)
                {
                    var savedScale = transform.localScale;

                    var animatorObj = Instantiate(pref, transform.position, Quaternion.identity);
					transform.SetParent(animatorObj.transform);

					if (parentCanvas != null)
                    {
						animatorObj.transform.SetParent(parentCanvas.transform);
                        transform.localScale = savedScale;
					}

					Destroy(this);
                }
                else
                {
                    Destroy(gameObject);
                }
        }

    }

    public void ShowUncollectedVisual(bool show)
    {

        if (objectImage != null)  
        {
            objectImage.color = show ? Color.red : Color.white;  
        }
    }

    private void PlayPopAnimation()
    {
        StartCoroutine(PopAnimationCoroutine());
    }

    private System.Collections.IEnumerator PopAnimationCoroutine()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 popScale = originalScale * 1.2f;
        float animTime = 0.3f;
        float elapsed = 0f;

        while (elapsed < animTime / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (animTime / 2);
            transform.localScale = Vector3.Lerp(originalScale, popScale, t);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < animTime / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (animTime / 2);
            transform.localScale = Vector3.Lerp(popScale, originalScale, t);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}