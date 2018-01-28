﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
	public void RestartLevel ()
	{
		Scenes.INSTANCE.ReloadCurrentLevel ();
	}

	public void Exit() {
		Application.Quit ();
	}
}
