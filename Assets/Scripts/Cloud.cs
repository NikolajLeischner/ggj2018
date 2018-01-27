using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	public EnergyEmitter emitter;

	public SourceController sourceController;

	public Material cloudMaterial;

	public GameObject cloud;

	public float fadeTimeInSeconds = 3;

	float fade = 1;

	Color startColor;

	bool isHitBySun = false;

	void Start ()
	{
		startColor = cloudMaterial.color;
	}

	void Update ()
	{
		float change = (1 / fadeTimeInSeconds) * Time.deltaTime;
		if (isHitBySun) {
			fade -= change;
		} else {
			fade += change;
		}
		fade = Mathf.Clamp (fade, 0, 1);
		cloudMaterial.color = Color.Lerp (Color.clear, startColor, fade);
	}

	void Toggle(bool active) {
		sourceController.Toggle (active);
		emitter.Toggle (active);
	}

	void OnTriggerEnter (Collider collider)
	{
		if (ColliderIsSun (collider)) {
			isHitBySun = true;
		}
	}

	void OnTriggerExit (Collider collider)
	{
		if (ColliderIsSun (collider)) {
			isHitBySun = false;
		}
	}

	bool ColliderIsSun (Collider collider)
	{
		var emitter = collider.GetComponent<EnergyEmitter> ();
		return emitter && emitter.GetEnergyType () == EnergyType.Sunlight;
	}
}
