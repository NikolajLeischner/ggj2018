using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
	// The first text is shown for the worst outcome, the last for the best.
	public string[] scoreTexts = { "Bane of Gaia", "Sun God", "Tree Lover" };
	public string anouncementOne = "Time is up. Now, what shall I call you?";
	public string anouncementTwo = "To me, you are:";

	public Text scoreAnouncement;

	public Text scoreAnouncementTwo;

	public Text scoreText;

	public Text titleText;

	void Start() {
		// ShowScore (0, 10);
	}

	public void ShowScore (float actualScore, float maximumScore)
	{
		StartCoroutine (ShowText (0, anouncementOne, scoreAnouncement, 3f));
		StartCoroutine (ShowText (5f, anouncementTwo, scoreAnouncementTwo, 3f));

		float percentage = actualScore / maximumScore;
		int position = Mathf.FloorToInt (scoreTexts.Length * percentage);
		string awardedTitle = scoreTexts [position];
		StartCoroutine (ShowText (7f, awardedTitle, titleText, 6f));

		string score = "" + Mathf.FloorToInt (actualScore);
		StartCoroutine (ShowText (8f, score, scoreText, 5f));
	}

	IEnumerator ShowText (float initialDelay, string text, Text display, float fadeoutAfter)
	{
		yield return new WaitForSeconds (initialDelay);

		display.gameObject.SetActive (true);
		display.color = Color.white;
		display.text = text;

		yield return new WaitForSeconds (fadeoutAfter);
		Color color = display.color;
		float fade = 1;
		while (fade > 0) {
			fade -= 0.05f;
			display.color = Color.Lerp (new Color(1, 1, 1, 0), color, fade);
			yield return new WaitForSeconds (0.1f);
		}

		display.gameObject.SetActive (false);
		yield return null;
	}
}
