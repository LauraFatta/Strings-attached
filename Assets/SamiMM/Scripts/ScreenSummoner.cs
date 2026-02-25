using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public static class ScreenSummoner
{
	public static void SummonScreen(Color screenColor, float lerpTime, bool transToFilled)
	{
		CoroutineRunner.instance.StartCoroutine(SummonScreenCoroutine(screenColor, lerpTime, transToFilled));
	}

	private static IEnumerator SummonScreenCoroutine(Color screenColor, float lerpTime, bool transToFilled)
	{
		GameObject screenPrefab = Resources.Load<GameObject>("Prefabs/sns/Screen");
		GameObject temp = Object.Instantiate(screenPrefab);
		Image img = temp.GetComponentInChildren<Image>();

		float startAlpha = transToFilled ? 0f : 1f;
		float endAlpha = transToFilled ? 1f : 0f;
		float timeElapsed = 0f;

		while (timeElapsed < lerpTime)
		{
			timeElapsed += Time.deltaTime;
			float t = timeElapsed / lerpTime;
			float currentAlpha = Mathf.Lerp(startAlpha, endAlpha, t);
			img.color = new Color(screenColor.r, screenColor.g, screenColor.b, currentAlpha);
			yield return null;
		}

		img.color = new Color(screenColor.r, screenColor.g, screenColor.b, endAlpha);

		if (!transToFilled)
			Object.Destroy(temp);
	}
}
public static class SceneManagerTransitioner
{
	public static void LoadSceneWithTransition(Color screenColor, float lerpTime, string sceneName)
	{
		CoroutineRunner.instance.StartCoroutine(SceneLoader(screenColor, lerpTime, sceneName));
		GameObject.DontDestroyOnLoad(CoroutineRunner.instance.gameObject);
	}
	public static void LoadSceneWithTransition(Color screenColor, float lerpTime, Scene scene)
	{
		CoroutineRunner.instance.StartCoroutine(SceneLoader(screenColor, lerpTime, scene.name));
		GameObject.DontDestroyOnLoad(CoroutineRunner.instance.gameObject);
	}
	private static IEnumerator SceneLoader(Color screenColor, float lerpTime, string sceneName)
	{
		ScreenSummoner.SummonScreen(screenColor, lerpTime, true);
		yield return new WaitForSeconds(lerpTime);

		yield return new WaitForSeconds(.5f);
		SceneManager.LoadScene(sceneName);

		// Wait a frame to ensure scene is fully loaded
		yield return null;

		ScreenSummoner.SummonScreen(screenColor, lerpTime, false);
	}

}