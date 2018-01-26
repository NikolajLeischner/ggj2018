using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Component[] earthObjects;
	public EarthTree[] allTrees;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		earthObjects = GetComponentsInChildren<EarthObjects> ();
		foreach (EarthObjects earthObjects in earthObjects)
			allTrees = earthObjects.getChildren ();


		//foreach (EarthTree earthObjects in allTrees)
		//	Debug.Log(earthObjects.getColor ());
	}
}
