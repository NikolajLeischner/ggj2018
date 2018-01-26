using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthTree : MonoBehaviour {

	public int sunEnergy = 0;
	public float energy = 600000;
	public Color color;
	public float height = 1;
	public int energyConsumption = 500;
	// Use this for initialization
	void Start () {
		
	}
		
	// Update is called once per frame
	void Update () {

		Debug.Log (energy);
		// update energy
		updateEnergy ();

		// update the health
		updateHealth ();

		// update tree height
		updateHeight();
	}

	private void setHeight(int val) {
		height = val;
	}

	private void setColor(int val) {
		height = val;
	}

	private void updateEnergy() {
		if (sunEnergy > 0) {
			energy += sunEnergy;
		} else {
			energy = Mathf.Max (energy - (energyConsumption * Time.deltaTime), 0);
		}
	}

	private void updateHeight() {
		height = (sunEnergy > 0) ? sunEnergy * (0.001f * Time.deltaTime) : height;
	}

	private void updateHealth() {
		if (energy >= 1000) {
			color = Color.green;   // green
		} else if(energy > 500 && energy < 1000) {
			color = new Color(1, 0, 1, 1);   // magenta
		} else if (energy > 100 && energy < 500) {
			color = new Color(1, 0, 1, 1);   // gray
		} else {
			color = Color.gray;
		}
	}

	public float getHeight() {
		return height;
	}

	public Color getColor() {
		return color;
	}

	public float getEnergy() {
		return energy;
	}
}
