using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
	public EarthTree earthTree;

	public HeatEffects heatEffects;

	void DrawEnergy (EnergyEmitter emitter)
	{
		var receivedEnergy = emitter.TransmitEnergy ();
		EnergyType energyType = emitter.GetEnergyType ();
		earthTree.AddEnergy (receivedEnergy, energyType);
		heatEffects.AddEnergy (receivedEnergy);
	}

	void OnTriggerStay (Collider collider)
	{
		var emitter = collider.gameObject.GetComponentInParent<EnergyEmitter> ();
		if (emitter) {
			DrawEnergy (emitter);
		}
	}
}
