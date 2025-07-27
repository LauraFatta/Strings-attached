using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class VignettePulse : MonoBehaviour
{
	[Header("Pulse Settings")]
	[Tooltip("Seconds for one full in‑and‑out pulse")]
	public float pulseDuration = 2f;

	[Tooltip("Curve mapping normalized time (0→1) to intensity (0→1)")]
	public AnimationCurve pulseCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

	private Vignette _vignette;
	private float _timer;

	void Start()
	{
		// Grab the Volume and find its Vignette override
		var volume = GetComponent<Volume>();
		if (volume.profile.TryGet<Vignette>(out _vignette))
		{
			// ensure override is active
			_vignette.active = true;
		}
		else
		{
			Debug.LogError("[VignettePulse] No Vignette in Volume profile!", this);
			enabled = false;
		}
	}

	void Update()
	{
		// advance and wrap timer
		_timer += Time.deltaTime;
		if (_timer > pulseDuration)
			_timer -= pulseDuration;

		// normalized 0→1
		float t = _timer / pulseDuration;

		// evaluate your curve and assign
		_vignette.intensity.value = pulseCurve.Evaluate(t);
	}
}
