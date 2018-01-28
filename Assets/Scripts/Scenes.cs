using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Scenes : MonoBehaviour
{

	public string[] scenes;

	public string startScene;

	public string creditsScene;

	int currentScene = 0;

	public static Scenes INSTANCE;

	bool levelInProgress = true;

	void Awake ()
	{
		DontDestroyOnLoad (this);
		if (INSTANCE == null)
			INSTANCE = this;
	}

	public bool GameIsRunning() {
		return levelInProgress;
	}

	public void EndLevel() {
		levelInProgress = false;
	}

	public void ShowStart ()
	{
		SceneManager.LoadScene (startScene);
		levelInProgress = true;
	}

	public void ShowCredits ()
	{
		SceneManager.LoadScene (creditsScene);
		levelInProgress = true;
	}

	public void ReloadCurrentLevel() {
		SceneManager.LoadScene (scenes [currentScene]);
		levelInProgress = true;
	}

	public void LoadNextLevel ()
	{
		currentScene += 1;
		if (currentScene < scenes.Length - 1)
			SceneManager.LoadScene (scenes [currentScene]);
		else
			ShowStart ();
		levelInProgress = true;
	}
}
