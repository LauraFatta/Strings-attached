using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class MenuController : MonoBehaviour
{
	private bool pressed = false;
	public void ChooseLevel(string sceneToLoad)
	{
		if (pressed) return; pressed = true;

		// Try to get a saved override
		string savedScene = SaveManager.GetCurrentScene();
		if (!string.IsNullOrEmpty(savedScene))
		{
			Debug.Log($"Overriding level load: using saved scene '{savedScene}' instead of '{sceneToLoad}'");
			sceneToLoad = savedScene;
		}

		SceneManagerTransitioner.LoadSceneWithTransition(Color.black, 1f, sceneToLoad);
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

	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("volume", volume);
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
