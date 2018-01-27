using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
	public EnergyEmitter emitter;

	public SourceController sourceController;

	MeshRenderer cloudRenderer;

	public GameObject cloud;

	public GameObject disappearPrefab;

	public float fadeTimeInSeconds = 3;

	public float respawnTimeInSeconds = 2;

	public Color startColor = Color.white;

	float fade = 1;

	float respawnCountdown = 0;

	bool isHitBySun = false;

	bool respawning = false;

	void Start ()
	{
		cloudRenderer = cloud.GetComponent<MeshRenderer> ();
		cloudRenderer.material.color = startColor;
	}

	void Update ()
	{
		if (fade == 0 && !respawning) {
			Hide ();
		} else if (respawning) {
			respawnCountdown -= (1 / respawnTimeInSeconds) * Time.deltaTime;

			if (respawnCountdown <= 0)
				Respawn ();
		} else {
			float change = (1 / fadeTimeInSeconds) * Time.deltaTime;
			if (isHitBySun) {
				fade -= change;
			} else {
				fade += change;
			}
			fade = Mathf.Clamp (fade, 0, 1);
			cloudRenderer.material.color = Color.Lerp (Color.clear, startColor, fade);
		}
	}

	void Hide ()
	{
		respawning = true;
		respawnCountdown = respawnTimeInSeconds;
		var go = Instantiate (disappearPrefab);
		go.transform.position = cloud.transform.position;
		Toggle (false);
	}

	void Respawn ()
	{
		fade = 0.05f;
		respawning = false;
		sourceController.MoveToRandomPosition ();
		Toggle (true);
	}

	void Toggle (bool active)
	{
		sourceController.Toggle (active);
		emitter.Toggle (active);
		cloud.SetActive (active);
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
