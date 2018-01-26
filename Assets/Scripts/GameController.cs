using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	EarthTree[] allTrees;

	public EarthObjects earthObjects;

	void Start () {
		allTrees = earthObjects.getChildren ();
	}

	void Update () {


		//foreach (EarthTree earthObjects in allTrees)
		//	Debug.Log(earthObjects.getColor ());
	}
}
