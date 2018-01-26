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

	void Awake ()
	{
		DontDestroyOnLoad (this);
		if (INSTANCE == null)
			INSTANCE = this;
	}

	public void ShowStart ()
	{
		SceneManager.LoadScene (startScene);
	}

	public void ShowCredits ()
	{
		SceneManager.LoadScene (creditsScene);
	}

	public void ReloadCurrentLevel() {
		SceneManager.LoadScene (scenes [currentScene]);
	}

	public void LoadNextLevel ()
	{
		currentScene += 1;
		if (currentScene < scenes.Length - 1)
			SceneManager.LoadScene (scenes [currentScene]);
		else
			ShowStart ();
	}
}
