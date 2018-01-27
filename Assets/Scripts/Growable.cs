using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growable : MonoBehaviour
{
	public EarthTree earthTree;

	void DrawEnergy (EnergyEmitter emitter)
	{
		var receivedEnergy = emitter.TransmitEnergy ();
		earthTree.ChangeSunEnergy (receivedEnergy);
	}

	void OnTriggerStay (Collider collider)
	{
		var emitter = collider.gameObject.GetComponentInParent<EnergyEmitter> ();
		if (emitter) {
			Debug.Log ("..");
			DrawEnergy (emitter);
		}
	}
}
