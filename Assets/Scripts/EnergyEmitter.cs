using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyType
{
	Sunlight,
	Rain
}

public class EnergyEmitter : MonoBehaviour
{

	public float initialEnergy = 1000000;

	public float emissionPerSecond = 100;

	public EnergyType energyType = EnergyType.Sunlight;

	float remainingEnergy;

	public Light spotLight;

	Color initialColor;

	Color finalColor = Color.red;

	bool isActive = true;

	void Start ()
	{
		remainingEnergy = initialEnergy;
		if (spotLight)
			initialColor = spotLight.color;
	}

	void UpdateColor ()
	{
		float percentage = remainingEnergy / initialEnergy;
		if (spotLight)
			spotLight.color = Color.Lerp (finalColor, initialColor, percentage);
	}

	public EnergyType GetEnergyType ()
	{
		return energyType;
	}

	public void Toggle(bool active) {
		isActive = active;
	}

	public bool IsEmitting() {
		return isActive && (remainingEnergy > 0);
	}

	public float TransmitEnergy ()
	{
		if (isActive) {
			float demand = emissionPerSecond * Time.deltaTime;
			float provided = Mathf.Min (remainingEnergy, demand);
			remainingEnergy = Mathf.Max (0, remainingEnergy - provided);

			UpdateColor ();

			return provided;
		} else {
			return 0;
		}
	}
}
