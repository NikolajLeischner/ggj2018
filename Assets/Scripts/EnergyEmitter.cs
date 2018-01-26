using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyEmitter : MonoBehaviour
{

	public float initialEnergy = 1000000;

	public float emissionPerSecond = 100;

	float remainingEnergy;

	public Light spotLight;

	Color initialColor;

	Color finalColor = Color.red;

	void Start ()
	{
		remainingEnergy = initialEnergy;
		initialColor = spotLight.color;
	}

	void UpdateColor ()
	{
		float percentage = remainingEnergy / initialEnergy;
		spotLight.color = Color.Lerp (finalColor, initialColor, percentage);
	}

	public float TransmitEnergy ()
	{
		float demand = emissionPerSecond * Time.deltaTime;
		float provided = Mathf.Min (remainingEnergy, demand);
		remainingEnergy = Mathf.Max (0, remainingEnergy - provided);

		UpdateColor ();

		return provided;
	}
}
