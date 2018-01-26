using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	EarthTree[] allTrees;

	public EarthObjects earthObjects;

	public GameMenu gameMenu;

	void Start () {
		allTrees = earthObjects.getChildren ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			bool menuIsActive = gameMenu.gameObject.activeSelf;
			gameMenu.gameObject.SetActive (!menuIsActive);
		}


		//foreach (EarthTree earthObjects in allTrees)
		//	Debug.Log(earthObjects.getColor ());
	}
}
