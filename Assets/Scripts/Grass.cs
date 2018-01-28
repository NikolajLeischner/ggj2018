using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : EnergyReceiver
{

	public GameObject grassPrefab;

	public GameObject grassBox;

	public int minCount = 10;

	public int maxCount = 30;

	public Vector3 initialScale = new Vector3 (1, 1, 1);

	public Vector3 maxScale = new Vector3 (1, 2, 1);

	public Color primaryColor = Color.green;

	public Color secondaryColor = Color.yellow;

	public Color burntColor = Color.black;

	List<GameObject> shrubs = new List<GameObject> ();

	List<Color> colors = new List<Color> ();

	public float energyConsumptionPerSecond = 10;

	public float maximumEnergy = 500;

	public float energy = 0;

	public float burningThreshold = 150f;

	float maximumHeat = 400;

	public float currentTemperature = 0;

	void Start ()
	{
		energy = 0;
		currentTemperature = 0;
		int count = Random.Range (minCount, maxCount);
		for (int i = 0; i < count; ++i) {
			shrubs.Add (SpawnGrass ());
			Color color = shrubs [i].GetComponentInChildren<MeshRenderer> ().material.color;
			colors.Add (color);
		}
	}

	GameObject SpawnGrass ()
	{
		var grass = Instantiate (grassPrefab);
		grass.transform.localScale = initialScale;
		grass.transform.SetParent (grassBox.transform);

		var bounds = grassBox.GetComponent<BoxCollider> ().bounds;
		float x = Random.Range (bounds.min.x, bounds.max.x);
		float y = Random.Range (bounds.min.y, bounds.max.y);
		float z = Random.Range (bounds.min.z, bounds.max.z);
		grass.transform.position = new Vector3 (x, y, z);

		UpdateColor (grass, Color.Lerp (primaryColor, secondaryColor, Random.value));
		return grass;
	}

	void UpdateColor (GameObject shrub, Color color)
	{
		var material = shrub.GetComponentInChildren<MeshRenderer> ().material;
		material.color = color;
	}

	void Update ()
	{
		energy = Mathf.Clamp (energy - (energyConsumptionPerSecond * Time.deltaTime), 0, maximumEnergy);
		if (energy > 0 && energy < maximumEnergy) {
			float percentage = energy / maximumEnergy;
			for (int i = 0; i < shrubs.Count; ++i) {
				shrubs [i].transform.localScale = Vector3.Lerp (initialScale, maxScale, percentage);
			}
		}
	}

	void ChangeHeat (float heat)
	{
		if (heat < 0.1f) {
			for (int i = 0; i < shrubs.Count; ++i) {
				UpdateColor (shrubs [i], colors [i]);
			}
		} else {
			for (int i = 0; i < shrubs.Count; ++i) {
				UpdateColor (shrubs [i], Color.Lerp(colors[i], burntColor, heat + 0.5f));
			}
		}
	}

	void UpdateHeat (float addedHeat)
	{
		currentTemperature += addedHeat;
		currentTemperature = Mathf.Clamp (currentTemperature, 0, maximumHeat);
		float normalizedBurn = (currentTemperature - burningThreshold) / (maximumHeat - burningThreshold);

		if (normalizedBurn < 0.2) {
			ChangeHeat (normalizedBurn);
		}
	}

	override public void AddEnergy (float addedEnergy, EnergyType energyType)
	{
		if (energyType == EnergyType.Sunlight) {
			UpdateHeat (addedEnergy);
		} else if (energyType == EnergyType.Rain) {
			UpdateHeat (-addedEnergy);
		}
		energy += addedEnergy;
	}
}
