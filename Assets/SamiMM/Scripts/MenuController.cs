using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
	private bool pressed = false;
	public void ChooseLevel(string sceneToLoad)
	{
		if (pressed) return; pressed = true;

		StartCoroutine(CheckSaveThenLoad(sceneToLoad));
	}
	private IEnumerator CheckSaveThenLoad(string originalScene)
	{
		SaveSystem saveSys = null;
		yield return StartCoroutine(
			SceneUtils.LoadSceneAndGetComponent<SaveSystem>(
				originalScene,
				"SaveSystemRoot",
				ss => saveSys = ss
			)
		);

		string savedScene = SaveManager.GetCurrentScene();
		string finalScene = originalScene;

		if (!string.IsNullOrEmpty(savedScene)
			&& saveSys != null
			&& !saveSys.dontOverrideScene)
		{
			Debug.Log($"Overriding level load: using '{savedScene}' instead of '{originalScene}'");
			finalScene = savedScene;
		}

		yield return SceneManager.UnloadSceneAsync(originalScene);

		SceneManagerTransitioner.LoadSceneWithTransition(Color.black, 1f, finalScene);
	}
	public void Quit()
    {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
	}

	#region SettingsMenu

	public AudioMixer audioMixer;
	public TMP_Dropdown resolutionDropdown;
	Resolution[] resolutions;
	private void Start()
	{
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();
		List<string> options = new List<string>();
		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(option);

			if (resolutions[i].width == Screen.currentResolution.width && 
				resolutions[i].height == Screen.currentResolution.height)
				currentResolutionIndex = i;
		}

		resolutionDropdown.AddOptions(options);
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}
	public void SetCurrentMixer(AudioMixer mixer)
	{
		audioMixer = mixer;
	}
	public void SetVolume(float volume)
	{
		float t = Mathf.InverseLerp(-10f, 0f, volume);

		float k = 4f;
		float curved = 1f - Mathf.Pow(1f - t, k);

		float targetDb = Mathf.Lerp(-80f, 0f, curved);

		audioMixer.SetFloat("volume", targetDb);
	}


	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	#endregion
}

public static class SceneUtils
{
	public static IEnumerator LoadSceneAndGetComponent<T>(
			string sceneName,
			string objectName,
			Action<T> callback
		) where T : Component
	{
		if (!SceneManager.GetSceneByName(sceneName).isLoaded)
		{
			var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			while (!op.isDone) yield return null;
		}

		yield return null;

		var scene = SceneManager.GetSceneByName(sceneName);
		T found = null;
		if (scene.isLoaded)
		{
			foreach (var root in scene.GetRootGameObjects())
			{
				if (root.name == objectName)
				{
					found = root.GetComponent<T>();
					break;
				}
			}
		}

		if (found == null)
			Debug.LogWarning($"[{nameof(LoadSceneAndGetComponent)}] Could not find '{objectName}' with {typeof(T)} in '{sceneName}'.");

		callback?.Invoke(found);
	}
}

