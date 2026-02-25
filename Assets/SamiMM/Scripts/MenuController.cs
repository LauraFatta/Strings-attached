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
	[SerializeField] private bool loadSavedScene = false;
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
			&& !saveSys.dontOverrideScene
			&& loadSavedScene)
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

	public AudioMixer masterMixer; // All three groups live in this one mixer asset
	public AudioMixer musicMixer;  // Music subgroup
	public AudioMixer sfxMixer;    // SFX subgroup

	// Keep track of the *linear* t from each slider (0…1)
	float _masterT = 1f;
	float _musicT = 1f;
	float _sfxT = 1f;

	// Utility: map sliderValue (say –10…0 or 0…1) to a 0…1 “t”
	float SliderToT(float sliderValue)
	{
		// if your slider is 0…1, just return sliderValue
		// if your slider is –10…0 dB, you can do:
		return Mathf.InverseLerp(-10f, 0f, sliderValue);
	}

	// Utility: apply a curve and map t (0…1) into –80…0 dB
	float TtoDb(float t)
	{
		float curved = 1f - Mathf.Pow(1f - t, 4f);
		return Mathf.Lerp(-80f, 0f, curved);
	}

	// Call this whenever any slider changes
	void ApplyVolumes()
	{
		// 1) Compute effective linear gain for each channel
		float musicEffT = _masterT * _musicT;
		float sfxEffT = _masterT * _sfxT;

		// 2) Map to dB
		float masterDb = TtoDb(_masterT);      // you might want to hear master itself
		float musicDb = TtoDb(musicEffT);
		float sfxDb = TtoDb(sfxEffT);

		// 3) Set on each exposed parameter
		masterMixer.SetFloat("volume", masterDb);
		musicMixer.SetFloat("volume", musicDb);
		sfxMixer.SetFloat("volume", sfxDb);
	}

	// These get hooked up to your UI sliders:
	public void OnMasterSliderChanged(float sliderValue)
	{
		_masterT = SliderToT(sliderValue);
		ApplyVolumes();
	}

	public void OnMusicSliderChanged(float sliderValue)
	{
		_musicT = SliderToT(sliderValue);
		ApplyVolumes();
	}

	public void OnSfxSliderChanged(float sliderValue)
	{
		_sfxT = SliderToT(sliderValue);
		ApplyVolumes();
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

