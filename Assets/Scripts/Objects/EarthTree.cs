using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : MonoBehaviour
{
	public Transform parent; // Scale the parent object, so the tree grows in one direction.
	public float energy = 600000;
	public Color color = Color.green;
	public float height = 1;
	float energyConsumptionPerSecond = 50;
	float growthFactor = 0.0005f;
	// Use this for initialization
	void Start ()
	{
		height = parent.localScale.y;
	}

	public void ChangeSunEnergy (float addedEnergy)
	{
		energy += addedEnergy;
		UpdateHeight (addedEnergy);
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

	private void UpdateHeight (float addedEnergy)
	{
		height = height + (addedEnergy * growthFactor);
		var scale = parent.localScale;
		parent.localScale = new Vector3 (scale.x, height, scale.z);
	}

	private void UpdateHealth ()
	{
		// Color.Lerp (Color.white, Color.black, 1f);
		if (energy >= 1000) {
			color = Color.green;   // green
		} else if (energy > 500) {
			color = new Color (1, 0, 1, 1);   // magenta
		} else if (energy > 100) {
			color = new Color (1, 0, 1, 1);   // gray
		} else {
			color = Color.gray;
		}
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
