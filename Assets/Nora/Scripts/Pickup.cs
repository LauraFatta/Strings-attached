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

            // Check if the item should disappear on pickup
            if (gameItem.shouldDisappearOnPickup)
            {
                var pref = Resources.Load<GameObject>("Prefabs/sns/PickupAnimator");
                if (pref != null)
                {
                    var animatorObj = Instantiate(pref, transform.position, Quaternion.identity);
                    transform.SetParent(animatorObj.transform);
                    Destroy(this);
                }
                else
                {
                    Destroy(gameObject);
                }
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