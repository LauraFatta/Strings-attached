using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameItem gameItem;
    public TextMeshProUGUI descriptionText;
    private float displayDuration = 1f;
    private Coroutine descriptionCoroutine;

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (descriptionCoroutine != null)
            StopCoroutine(descriptionCoroutine);
            
        ShowDescription();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
 
        if (descriptionCoroutine != null)
            StopCoroutine(descriptionCoroutine);
            
        descriptionCoroutine = StartCoroutine(HideDescriptionAfterDelay());
    }

    private void ShowDescription()
    {
        if (descriptionText != null && gameItem != null)
        {
            descriptionText.text = gameItem.itemDescription;
            descriptionText.gameObject.SetActive(true);
        }
    }

    private System.Collections.IEnumerator HideDescriptionAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        descriptionText.gameObject.SetActive(false);
    }

 
    private void OnDestroy()
    {
        if (descriptionText != null)
            descriptionText.gameObject.SetActive(false);
    }
}