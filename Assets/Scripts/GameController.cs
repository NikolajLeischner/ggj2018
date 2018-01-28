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
	public float score = 0;
	public float timer = 0;
	public float currentTime = 0;
	public bool gameFinished = false;
	public int healthyEnergy = 0;
	float elapsed = 0f;

	void Start () {
		allTrees =  earthObjects.getChildren ();
		maxScore = allTrees.Length * allTrees.Length * 4 * timer; 
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
		elapsed += Time.deltaTime;
		if (elapsed >= 1f) {
			elapsed = elapsed % 1f;

			// iterate over all trees and compute increment scors.
			foreach (EarthTree tree in allTrees) {
				score += tree.getLifeStatus ();
			}		
		}
	} 
}
