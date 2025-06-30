using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimatorFunctions : MonoBehaviour
{
	[SerializeField] MenuButtonController menuButtonController;
	[SerializeField] ParticleSystem buttonParticleEffect;
	[SerializeField] string sceneToLoad = "Level Selection";
	public bool disableOnce;

	void PlaySound(AudioClip whichSound){
		if(!disableOnce){
			menuButtonController.audioSource.PlayOneShot (whichSound);
		}else{
			disableOnce = false;
		}
	}

	void PlayParticleEffect(){
		if(buttonParticleEffect != null){
			buttonParticleEffect.Play();
		}
	}

	void LoadScene(){
		if(!string.IsNullOrEmpty(sceneToLoad)){
			SceneManager.LoadScene(sceneToLoad);
		}
	}

	void QuitGame(){
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}	
