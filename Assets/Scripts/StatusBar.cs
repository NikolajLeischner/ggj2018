﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBar : MonoBehaviour {

	public Color healthyColor = Color.green;

	public Color normalColor = Color.blue;

	public Color criticalColor = Color.red;

	public GameObject barHandle;

	public MeshRenderer barRenderer;

	float minValue = 0;
	float minHealthy = 20;
	float maxValue = 100;

	float maxScale = 1;
	float minScale = 0.01f;

	public void Initialse(float minHealthy, float maxValue, float initialValue) {
		this.minHealthy = minHealthy;
		this.maxValue = maxValue;
		UpdateStatus (initialValue);
	}

	public void UpdateStatus(float newValue) {
		//Debug.Log (maxScale + " " + newValue);
		float percentage = newValue / maxValue;
		if (newValue > minHealthy) {
			float range = maxValue - minHealthy;
			float healthyPercentage = (newValue - minHealthy) / range;
			barRenderer.material.color = Color.Lerp (normalColor, healthyColor, healthyPercentage);
		} else {
			float range = minHealthy - minValue;
			float criticalPercentage = newValue / range;
			barRenderer.material.color = Color.Lerp (normalColor, healthyColor, criticalPercentage);
		}

		float newScale = Mathf.Lerp (minScale, maxScale, percentage);
		var scale = barHandle.transform.localScale;
		barHandle.transform.localScale = new Vector3 (newScale, scale.y, scale.z);
	}
}
