using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class Pickup : MonoBehaviour, IPointerDownHandler
{
    public GameItem gameItem;
    public static List<Pickup> activePickups = new List<Pickup>();

    private Inventory inventory;
    private Renderer objectRenderer;

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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryUI ui = FindObjectOfType<InventoryUI>();
        if (ui != null)
        {
            ui.NotifyPickupClick();
        }

        if (inventory.AddItem(gameItem))
        {
            // Add save system integration
            if (SaveSystem.instance != null)
            {
                SaveSystem.instance.MarkItemCollected(gameItem);
            }

            if (gameItem.itemType == ItemType.ComponentOnly)
            {
                ShowUncollectedVisual(true);
                // Add "pop" animation for ComponentOnly items
                PlayPopAnimation();
            }
            else
            {
                // Use pickup animation instead of immediate destroy
                var pref = Resources.Load<GameObject>("Prefabs/sns/PickupAnimator");
                if (pref != null)
                {
                    var animatorObj = Instantiate(pref, transform.position, Quaternion.identity);
                    transform.SetParent(animatorObj.transform);
                    Destroy(this);
                }
                else
                {
                    // Fallback to immediate destroy if animation prefab not found
                    Destroy(gameObject);
                }
            }
        }
    }

    public void ShowUncollectedVisual(bool show)
    {
        if (objectRenderer != null && gameItem.itemType == ItemType.ComponentOnly)
        {
            objectRenderer.material.color = show ? Color.red : Color.white;
        }
    }

    private void PlayPopAnimation()
    {
        // Create a simple "pop" scale animation
        StartCoroutine(PopAnimationCoroutine());
    }

    private System.Collections.IEnumerator PopAnimationCoroutine()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 popScale = originalScale * 1.2f;
        float animTime = 0.3f;
        float elapsed = 0f;

        // Scale up
        while (elapsed < animTime / 2)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / (animTime / 2);
            transform.localScale = Vector3.Lerp(originalScale, popScale, t);
            yield return null;
        }

        // Scale back down
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