using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEmitter : MonoBehaviour {

	public float initialEnergy = 1000;

	float remainingEnergy;

	void Start () {
		remainingEnergy = initialEnergy;
	}

	void Update() {
		
	}

	public float TransmitEnergy(float demand) {
		float provided = Mathf.Min (remainingEnergy, demand);
		remainingEnergy = Mathf.Max (0, remainingEnergy - provided);
		return provided;
	}
}
