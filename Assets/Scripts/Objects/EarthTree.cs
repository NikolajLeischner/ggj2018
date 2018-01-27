using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : MonoBehaviour
{
	public Transform parent; // Scale the parent object, so the tree grows in one direction.
	public MeshRenderer renderer;  // the material 
	public float energy = 600000;
	public float maxEnergy = 2000;
	public Color color = Color.green;
	public float height = 1;
	public float width = 1;
	float energyConsumptionPerSecond = 50;
	float heightFactor = 0.0005f;
	float widthFactor = 0.0003f;
	int lifeStatus = 0;


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
		energy = 100;
		maxEnergy = 2000;
		color = Color.white;
	}

	public void ChangeSunEnergy (float addedEnergy)
	{
		energy += addedEnergy;
		UpdateGrowth (addedEnergy);
	}

	void Update ()
	{
		ConsumeEnergy ();
		UpdateHealth ();
	}

	private void ConsumeEnergy ()
	{
		float consumedEnergy = energyConsumptionPerSecond * Time.deltaTime;
		energy = Mathf.Max (energy - consumedEnergy, 0);
	}

	private void UpdateGrowth (float addedEnergy)
	{
		height = height + (addedEnergy * heightFactor);
		width = width + (addedEnergy * widthFactor);
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
		float hue = energy / maxEnergy;
		color = Color.Lerp(Color.black, Color.green, hue);
		renderer.material.color = color;   
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

	public float GetEnergy ()
	{
		return energy;
	}
}
