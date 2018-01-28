using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : EnergyReceiver
{
	public Transform parent; // Scale the parent object, so the tree grows in one direction.
	public float sunEnergy = 0f;
	public float rainEnergy = 0f;
	public float energy = 0f;
	public MeshRenderer treeMesh;
	public float maxEnergy = 2000;
	public Color color = Color.green;
	public float height = 1;
	public float width = 1;
	float energyConsumptionPerSecond = 20;
	float waterConsumptionPerSecond = 20;
	float heightFactor = 0.05f;
	float widthFactor = 0.03f;
	float lifeStatus = 4f;
	public StatusBar sunStatus;
	public StatusBar waterStatus;



	// audio
	public AudioClip burning;
	public bool isBuringPlay;

	public AudioClip healthy;
	public bool isHealthyPlay;

	public AudioClip critical;
	public bool isCriticalPlay;

	public AudioClip dying;
	public bool isDyingPlay;

	public bool isPlaying;
	AudioSource mySource;

	void Start ()
	{
		height = parent.localScale.y;
		width = parent.localScale.x;
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
		waterStatus.UpdateStatus (rainEnergy);
	}

	public void updateSunEnergy(float addedEnergy)
	{
		sunEnergy += addedEnergy;
		rainEnergy -= addedEnergy;
		sunStatus.UpdateStatus (sunEnergy);
	}

	void Update ()
	{
		Debug.Log (lifeStatus);
		if (lifeStatus > 0) {
			ConsumeEnergy ();
			ComputeColor();
			UpdateLifeStatus ();
		}
	}

	private void ConsumeEnergy ()
	{
		sunEnergy = sunEnergy - energyConsumptionPerSecond * Time.deltaTime;
		rainEnergy = rainEnergy - waterConsumptionPerSecond * Time.deltaTime;
	}

	private void UpdateGrowth ()
	{
		height = height + heightFactor * Time.deltaTime;
		width = width + widthFactor * Time.deltaTime;
		var scale = parent.localScale;
		parent.localScale = new Vector3 (width, height, scale.z);
	}

	private void ComputeColor()
	{
		float hue = sunEnergy / maxEnergy;
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

	public float Remap (float value, float from1, float to1, float from2, float to2) {
		return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
	}

	private void PlaySound(AudioClip clip) {
		mySource.clip = clip;
		mySource.Play ();
	}

	public float getLifeStatus()
	{
		return lifeStatus;
	}

	public float GetHeight ()
	{
		return height;
	}

	public Color GetColor ()
	{
		return color;
	}

	public float GetSunEnergy ()
	{
		return sunEnergy;
	}
}
