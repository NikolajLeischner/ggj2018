using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatEffects : MonoBehaviour
{

	public ParticleSystem flames;

	float maxEmissionRate = 20;

	float minEmissionRate = 0.1f;

	float currentTemperature = 0;

	float burningThreshold = 100;

	float heatFactor = 0.25f;

	float maximumHeat = 3500f;

	float heatLossPerSecond = -10f;

	public void ChangeHeat (float heat)
	{
		float normalisedHeat = Mathf.Clamp (heat, 0, 1);
		if (normalisedHeat > 0) {
			flames.gameObject.SetActive (true);
			float newRate = Mathf.Lerp (minEmissionRate, maxEmissionRate, normalisedHeat);
			var emission = flames.emission;
			var rateOverTime = emission.rateOverTime;
			rateOverTime.constant = newRate;
			emission.rateOverTime = rateOverTime;
		} else {
			flames.gameObject.SetActive (false);
		}
	}

	public void AddEnergy(float addedEnergy, EnergyType energyType) {
		float finalEnergy = (energyType == EnergyType.Sunlight) ? addedEnergy : (-1 * addedEnergy);
		UpdateHeat(finalEnergy * heatFactor);
	}

	void UpdateHeat(float addedHeat) {
		currentTemperature += addedHeat;
		currentTemperature = Mathf.Clamp (currentTemperature, 0, maximumHeat);
		float normalizedBurn = (currentTemperature - burningThreshold) / (maximumHeat - burningThreshold);
		ChangeHeat (normalizedBurn);
	}

	void Update() {
		UpdateHeat (heatLossPerSecond * Time.deltaTime);
	}
}
