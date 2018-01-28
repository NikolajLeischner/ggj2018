﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyType
{
	Sunlight,
	Rain
}

public class EnergyEmitter : MonoBehaviour
{

	public float emissionPerSecond = 100;
	public float initialEnergy = 0;
	public EnergyType energyType = EnergyType.Sunlight;

	float remainingEnergy;

	public Light spotLight;

	Color initialColor;

	Color finalColor = Color.red;

	bool isActive = true;

	public void initialize (float startEnergy)
	{
		initialEnergy = startEnergy;
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
		return emissionPerSecond * Time.deltaTime;
	}
}
