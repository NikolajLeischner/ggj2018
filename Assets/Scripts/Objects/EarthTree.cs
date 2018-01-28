using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : EnergyReceiver
{
	public Transform parent; // Scale the parent object, so the tree grows in one direction.
	public float sunEnergy = 0f;
	public float rainEnergy = 0f;
	public MeshRenderer treeMesh;
	public float maxEnergy = 2000;
	public Color color = Color.green;
	Vector3 initialScale;
	Vector3 maximumScale;
	float energyConsumptionPerSecond = 20;
	float waterConsumptionPerSecond = 20;
	float heightFactor = 0.05f;
	float widthFactor = 0.03f;
	public float lifeStatus = 4f;
	public StatusBar sunStatus;
	public StatusBar waterStatus;

	void Start ()
	{
		energyConsumptionPerSecond = Random.Range (energyConsumptionPerSecond * 0.3f, energyConsumptionPerSecond * 2f);
		waterConsumptionPerSecond = Random.Range (waterConsumptionPerSecond * 0.3f, waterConsumptionPerSecond * 2f);
		initialScale = parent.localScale;
		maximumScale = initialScale * 2;

		// keep the total energy to 1500 to be healthy
		sunEnergy = 1000f;  
		rainEnergy = 500f;
		maxEnergy = 3500;
		color = Color.white;

		float criticalThreshold = 50;
		sunStatus.Initialse (criticalThreshold, maxEnergy, sunEnergy);
		waterStatus.Initialse (criticalThreshold, maxEnergy, rainEnergy);

	}

	override public void AddEnergy (float addedEnergy, EnergyType energyType)
	{			
		if (energyType == EnergyType.Sunlight) {
			updateSunEnergy (addedEnergy);
		} else if (energyType == EnergyType.Rain) {
			updateRainEnergy (addedEnergy);
		}

		if (lifeStatus > 0 && addedEnergy > 0)
			UpdateGrowth ();
	}

	public void updateRainEnergy(float addedEnergy)
	{
		rainEnergy += addedEnergy;
		sunEnergy -= addedEnergy;
	}

	public void updateStatusBar(){
		waterStatus.UpdateStatus (rainEnergy);
		sunStatus.UpdateStatus (sunEnergy);
	}

	public void updateSunEnergy(float addedEnergy)
	{
		sunEnergy += addedEnergy;
		rainEnergy -= addedEnergy;
	}

	void Update ()
	{
		if (lifeStatus > 0) {
			ConsumeEnergy ();
			ComputeColor();
			UpdateLifeStatus ();
		}

		updateStatusBar ();
	}

	private void ConsumeEnergy ()
	{
		sunEnergy = sunEnergy - energyConsumptionPerSecond * Time.deltaTime;
		rainEnergy = rainEnergy - waterConsumptionPerSecond * Time.deltaTime;
	}

	private void UpdateGrowth ()
	{
		Vector3 growth = new Vector3 (widthFactor, heightFactor, widthFactor) * Time.deltaTime;
		Vector3 tentativeSize = parent.localScale + growth;
		parent.localScale = Vector3.Min (tentativeSize, maximumScale);
	}

	private void ComputeColor()
	{
		float hue = lifeStatus / 8;
		color = Color.Lerp(Color.black, Color.green, hue);
		treeMesh.material.color = color;
	}

	private void UpdateLifeStatus()
	{
		int rainHealth = getWaterHealth ();
		int sunHealth = getSunHealth ();
		lifeStatus = (rainHealth + sunHealth) / 2;
	}

	private int getWaterHealth()
	{
		if (rainEnergy < 100) {
			return 0;
		} else if (rainEnergy > 100 && rainEnergy <= 500) {
			return 1;
		} else if (rainEnergy > 500 && rainEnergy <= 1000) {
			return 2;
		} else if (rainEnergy > 1000 && rainEnergy <= 1500) {
			return 3;
		} else if (rainEnergy > 1500 && rainEnergy <= 2000) {
			return 4;
		} else if (rainEnergy > 2000 && rainEnergy <= 2500) {
			return 3;
		} else if (rainEnergy > 2500 && rainEnergy <= 3000) {
			return 2;
		} else if (rainEnergy > 3000 && rainEnergy <= 3500) {
			return 1;
		} else {
			return 0;
		}
	}

	private int getSunHealth()
	{
		if (sunEnergy < 100) {
			return 0;
		} else if (sunEnergy > 100 && sunEnergy <= 500) {
			return 1;
		} else if (sunEnergy > 500 && sunEnergy <= 1000) {
			return 2;
		} else if (sunEnergy > 1000 && sunEnergy <= 1500) {
			return 3;
		} else if (sunEnergy > 1500 && sunEnergy <= 2000) {
			return 4;
		} else if (sunEnergy > 2000 && sunEnergy <= 2500) {
			return 3;
		} else if (sunEnergy > 2500 && sunEnergy <= 3000) {
			return 2;
		} else if (sunEnergy > 3000 && sunEnergy <= 3500) {
			return 1;
		} else {
			return 0;
		}
	}

	public float getLifeStatus()
	{
		return lifeStatus;
	}
}
