using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public EarthObjects earthObjects;
	private EarthTree[] allTrees;
	public GameMenu gameMenu;

	// game related 
	public int levelNumber = 1;
	public float maxScore = 0;
	public int score = 0;
	public float timer = 0;
	public float currentTime = 0;
	public bool gameFinished = false;

	void Start () {
		allTrees = earthObjects.getChildren ();
		maxScore = allTrees.Length * allTrees.Length * 2 * timer; 
		Debug.Log (maxScore);
		Debug.Log (timer);
		currentTime = Time.realtimeSinceStartup;
		Debug.Log (currentTime);

	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			bool menuIsActive = gameMenu.gameObject.activeSelf;
			gameMenu.gameObject.SetActive (!menuIsActive);
		}

		// compute the score during the game
		ComputeScore ();

		// check if the timer is finished
		if (currentTime > timer && gameFinished == false) {
			string result = "you got(" + score.ToString() + ")out of " + maxScore.ToString() + "score";
			Debug.Log(result);
			gameFinished = true;
		}

		// update timer
		currentTime = Time.realtimeSinceStartup;
	}

	public void ComputeScore()
	{
		// iterate over all trees and compute increment scors.
		foreach (EarthTree tree in allTrees) {
			score += tree.getLifeStatus ();
		}
	} 
}
