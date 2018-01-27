using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : MonoBehaviour
{
	public Transform parent; // Scale the parent object, so the tree grows in one direction.
	public float sunEnergy = 0;
	public float rainEnergy = 0;
	public float energy = 0;
	public MeshRenderer treeMesh;
	public float maxEnergy = 2000;
	public Color color = Color.green;
	public float height = 1;
	public float width = 1;
	float energyConsumptionPerSecond = 50;
	float heightFactor = 0.00005f;
	float widthFactor = 0.00003f;
	int lifeStatus = 0;

	public StatusBar sunStatus;
	public StatusBar waterStatus;


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
		sunEnergy = 200;
		rainEnergy = 200;
		maxEnergy = 2000;
		updateTotalEnergy ();
		color = Color.white;

		float criticalThreshold = 50;
		sunStatus.Initialse (criticalThreshold, maxEnergy, sunEnergy);
		waterStatus.Initialse (criticalThreshold, maxEnergy, rainEnergy);
	}

	public void AddEnergy (float addedEnergy, EnergyType energyType)
	{			
		if (energy < maxEnergy && addedEnergy > 0) {
			if (energyType == EnergyType.Sunlight) {
				updateSunEnergy (addedEnergy);
			} else if (energyType == EnergyType.Rain) {
				updateRainEnergy (addedEnergy);
			}
			UpdateGrowth ();
		}
		updateTotalEnergy ();
	}

	public void updateRainEnergy(float addedEnergy)
	{
		rainEnergy += addedEnergy;
		waterStatus.UpdateStatus (rainEnergy);
	}

	public void updateSunEnergy(float addedEnergy)
	{
		sunEnergy += addedEnergy;
		sunStatus.UpdateStatus (sunEnergy);
	}

	public void updateTotalEnergy()
	{
		energy = sunEnergy + rainEnergy;
	}

	void Update ()
	{
		if (energy < maxEnergy) {
			ConsumeEnergy ();
			UpdateHealth ();
		}
	}

	private void ConsumeEnergy ()
	{
		float consumedEnergy = energyConsumptionPerSecond * Time.deltaTime;
		energy = Mathf.Max (energy - consumedEnergy, 0);
	}

	private void UpdateGrowth ()
	{
		height = height + (energy * heightFactor) * Time.deltaTime;
		width = width + (energy * widthFactor) * Time.deltaTime;
		var scale = parent.localScale;
		parent.localScale = new Vector3 (width, height, scale.z);
	}

	private void UpdateHealth ()
	{
		// compute tree color
		ComputeColor();

		// check status
		UpdateLifeStatus();
	}

	private void ComputeColor()
	{
		float hue = sunEnergy / maxEnergy;
		color = Color.Lerp(Color.black, Color.green, hue);
		treeMesh.material.color = color;
	}

	private void UpdateLifeStatus()
	{
		if (energy < 500) {
			lifeStatus = 0;
		} else if (energy > 500 && energy <= 1000) {
			lifeStatus = 1;
		} else if (energy > 1000 && energy <= 2000) {
			lifeStatus = 2;
		} else {
			lifeStatus = 0;
		}
	}

	private void PlaySound(AudioClip clip) {
		mySource.clip = clip;
		mySource.Play ();
	}

	public int getLifeStatus()
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
