using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : MonoBehaviour
{
	public float energy = 600000;
	public Color color = Color.green;
	public float height = 1;
	int energyConsumptionPerSecond = 50;
	// Use this for initialization
	void Start ()
	{
		
	}

	public void ChangeSunEnergy(float addedEnergy) {
		energy += addedEnergy;
		updateHeight (addedEnergy);
	}
		
	// Update is called once per frame
	void Update ()
	{

		Debug.Log (energy);
		// update energy
		consumeEnergy ();

		// update the health
		updateHealth ();
	}

	private void consumeEnergy ()
	{
		energy = Mathf.Max (energy - (energyConsumptionPerSecond * Time.deltaTime), 0);
	}

	private void updateHeight (float addedEnergy)
	{
		height = height + (addedEnergy * 0.001f);
	}

	private void updateHealth ()
	{
		// Color.Lerp (Color.white, Color.black, 1f);
		if (energy >= 1000) {
			color = Color.green;   // green
		} else if (energy > 500 && energy < 1000) {
			color = new Color (1, 0, 1, 1);   // magenta
		} else if (energy > 100 && energy < 500) {
			color = new Color (1, 0, 1, 1);   // gray
		} else {
			color = Color.gray;
		}
	}

	public float getHeight ()
	{
		return height;
	}

	public Color getColor ()
	{
		return color;
	}

	public float getEnergy ()
	{
		return energy;
	}
}
