using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
	[SerializeField] private KeyCode pauseKey;
	[SerializeField] private GameObject pauseParent;
	
	[Space(15)]

	[SerializeField] private GameObject settingsPanel;
	[SerializeField] private GameObject pauseButton;
	[SerializeField] private GameObject TutoralButton;
	[SerializeField] private GameObject ThreadButton;
	[SerializeField] private GameObject InventoryButton;

	private MenuController menuController;

	private bool pressed = false;
	private void Start()
	{
		menuController = GetComponent<MenuController>();
		pauseParent.SetActive(false);
	}
	private void Update()
	{
		if (pressed) return;

		if (Input.GetKeyDown(pauseKey) && !pauseParent.activeSelf)
			Pause();
		if (Input.GetKeyDown(KeyCode.Escape) && pauseParent.activeSelf)
			Resume();
	}
	public void Pause()
	{
		if (pauseParent) pauseParent.SetActive(true);
		if (settingsPanel) settingsPanel.SetActive(false);
		if (pauseButton) pauseButton.SetActive(false);
		if (TutoralButton) TutoralButton.SetActive(false);
		if (ThreadButton) ThreadButton.SetActive(false);

		if (InventoryButton) InventoryButton.SetActive(false);


	}

	public void Resume()
	{
		if (pauseParent) pauseParent.SetActive(true);
		if (pauseButton) pauseButton.SetActive(true);
		if (pauseParent) pauseParent.SetActive(false);
		if (TutoralButton) TutoralButton.SetActive(true);
		if (ThreadButton) ThreadButton.SetActive(true);
		if (InventoryButton) InventoryButton.SetActive(true);

}

	// public void Pause()
	// {
	// 	PoliceStationCanvas.SetActive(false);
	// 	pauseParent.SetActive(true);
	// 	settingsPanel.SetActive(false);
	// 	pauseButton.SetActive(false);

	// 	pauseParent.SetActive(!pauseParent.activeSelf);
	// }
	// public void Resume()
	// {
	// 	PoliceStationCanvas.SetActive(true);
	// 	pauseButton.SetActive(true);
	// 	pauseParent.SetActive(!pauseParent.activeSelf);
	// }
	public void ResetCase(string sceneToGoTo)
	{
		if (pressed) return; pressed = true;

		SaveSystem.instance.DeleteSave();
		menuController.ChooseLevel(sceneToGoTo);

		StartCoroutine(FadeOut());
	}
	public void ExitCase(string sceneToGoTo)
	{
		if (pressed) return; pressed = true;

		menuController.ChooseLevel(sceneToGoTo);

		StartCoroutine(FadeOut());
	}

	private System.Collections.IEnumerator FadeOut()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		float duration = 1f;
		float elapsed = 0f;
		while (elapsed < duration)
		{
			elapsed += Time.deltaTime;
			canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
			yield return null;
		}

		canvasGroup.alpha = 0f;
	}
}
