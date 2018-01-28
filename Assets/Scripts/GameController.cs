using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	
	private EarthTree[] trees;
	public GameObject plants;
	public GameObject restartButton;
	public ScoreDisplay scoreDisplay;

	// game related 
	public float maxScore = 0;
	public float score = 0;
	public float levelDurationInSeconds = 0;
	public float currentTime = 0;
	public bool gameFinished = false;

	void Start () {
		trees = plants.GetComponentsInChildren<EarthTree> ();
		maxScore = trees.Length * 4 * levelDurationInSeconds; 
		StartCoroutine (ComputeScore ());
	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			bool menuIsActive = restartButton.activeSelf;
			restartButton.SetActive (!menuIsActive);
		}

		if (currentTime > levelDurationInSeconds && !gameFinished) {
			gameFinished = true;
			Scenes.INSTANCE.EndLevel ();
			scoreDisplay.ShowScore (score, maxScore);
		}

		currentTime += Time.deltaTime;
	}

	IEnumerator ComputeScore() {
		while (!gameFinished) {
			foreach (EarthTree tree in trees) {
				score += tree.getLifeStatus ();
			}
			//Debug.Log ("Score: " + score);
 			yield return new WaitForSeconds (1);
		}
		yield return null;
	}
}
