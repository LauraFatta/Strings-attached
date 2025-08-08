using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI descriptionText;
    private float displayDuration = 1f;
    private Coroutine descriptionCoroutine;
    private GameItem gameItem;
	private void Start()
	{
		gameItem = GetComponent<Pickup>().gameItem;
		if (gameItem == null)
		{
			Debug.Log("GameItem not found with ItemDescription");
			return;
		}
	}

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

    private void OnDestroy()
    {
        if (descriptionCoroutine != null)
        {
            StopCoroutine(descriptionCoroutine);
        }

        if (descriptionText != null && descriptionText.gameObject != null)
        {
            descriptionText.gameObject.SetActive(false);
        }
    }

    private System.Collections.IEnumerator HideDescriptionAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (descriptionText != null && descriptionText.gameObject != null)
        {
            descriptionText.gameObject.SetActive(false);
        }
    }
}

