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

	public float emissionPerSecond = 100;

	public EnergyType energyType = EnergyType.Sunlight;

	public Light spotLight;

	Color initialColor;

	Color finalColor = Color.red;

	bool isActive = true;

	public void initialize (float startEnergy)
	{
		if (spotLight)
			initialColor = spotLight.color;
	}

	public EnergyType GetEnergyType ()
	{
		return energyType;
	}

	public void Toggle (bool active)
	{
		isActive = active;
	}

	public bool IsEmitting ()
	{
		return isActive;
	}

	public float TransmitEnergy ()
	{
		return emissionPerSecond * Time.deltaTime;
	}
}
