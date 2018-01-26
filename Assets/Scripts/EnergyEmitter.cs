using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEmitter : MonoBehaviour {

	public float initialEnergy = 1000;

	float remainingEnergy;

	public Light light;

	Color initialColor;

	Color finalColor = Color.red;

	void Start () {
		remainingEnergy = initialEnergy;
		initialColor = light.color;
	}

	void UpdateColor() {
		float percentage = remainingEnergy / initialEnergy;
		light.color = Color.Lerp (finalColor, initialColor, percentage);
	}

	public float TransmitEnergy(float demand) {
		float provided = Mathf.Min (remainingEnergy, demand);
		remainingEnergy = Mathf.Max (0, remainingEnergy - provided);

		UpdateColor ();

		return provided;
	}
}
