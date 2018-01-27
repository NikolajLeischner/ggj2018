using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
	public EnergyReceiver energyReceiver;

	public HeatEffects heatEffects;

	void DrawEnergy (EnergyEmitter emitter)
	{
		var receivedEnergy = emitter.TransmitEnergy ();
		EnergyType energyType = emitter.GetEnergyType ();
		energyReceiver.AddEnergy (receivedEnergy, energyType);
		heatEffects.AddEnergy (receivedEnergy, energyType);
	}

	void OnTriggerStay (Collider collider)
	{
		var emitter = collider.gameObject.GetComponentInParent<EnergyEmitter> ();
		if (emitter) {
			DrawEnergy (emitter);
		}
	}
}
