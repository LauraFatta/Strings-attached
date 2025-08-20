using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class UI_Sprite_SoundEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
	[Header("Sounds")]
	public AudioClip hoverSound;
	public AudioClip clickSound;

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.playOnAwake = false;
	}

	// --- UI events ---
	public void OnPointerEnter(PointerEventData eventData)
	{
		PlaySound(hoverSound);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		PlaySound(clickSound);
	}

	// --- Sprite/Collider events ---
	private void OnMouseEnter()
	{
		PlaySound(hoverSound);
	}

	private void OnMouseDown()
	{
		PlaySound(clickSound);
	}

	private void PlaySound(AudioClip clip)
	{
		if (clip != null && audioSource != null)
			audioSource.PlayOneShot(clip);
	}
}
