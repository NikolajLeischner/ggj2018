using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{

	public float initialEnergy = 100;

	public float energyConsumptionPerSecond = 1;

	public float energyDrawnPerSecond = 5;

	public float maxScale = 3;
	public float minScale = 0.3f;

	float currentEnergy;

	void Start ()
	{
		currentEnergy = initialEnergy;
	}

	void Update ()
	{
		float consumed = energyConsumptionPerSecond * Time.deltaTime;
		currentEnergy = Mathf.Max (0, currentEnergy - consumed);
	}

	void DrawEnergy (EnergyEmitter emitter)
	{
		float demand = energyDrawnPerSecond * Time.deltaTime;
		var receivedEnergy = emitter.TransmitEnergy (demand);
		currentEnergy += receivedEnergy;
	}

	void OnTriggerStay (Collider collider)
	{
		var emitter = collider.gameObject.GetComponentInParent<EnergyEmitter> ();
		if (emitter) {
			Debug.Log ("drawing..");
			DrawEnergy (emitter);
		}
	}
}
