using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public EarthObjects earthObjects;
	private EarthTree[] allTrees;
	public GameMenu gameMenu;

	// game related 
	public int levelNumber = 1;
	public int maxScore = 0;
	public int score = 0;
	public double timer = 0;
	bool gameFinished = false;
	void Start () {
		allTrees = earthObjects.getChildren ();
		maxScore = allTrees.Length * 2;
	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			bool menuIsActive = gameMenu.gameObject.activeSelf;
			gameMenu.gameObject.SetActive (!menuIsActive);
		}
			
		if (timer < 0 && gameFinished == false) {
			ComputeScore ();
			string result = "you got(" + score.ToString() + ")out of " + maxScore.ToString() + "score";
			Debug.Log(result);
			gameFinished = true;
		} else {
			// update the timer
			timer = timer - (Time.deltaTime * 2);
		}
	}

	public void ComputeScore()
	{
		// get the earth objects
		Debug.Log (allTrees.Length);
		foreach (EarthTree tree in allTrees) {
			score += tree.getLifeStatus ();
		}
	} 
}
