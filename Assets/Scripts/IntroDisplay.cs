using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroDisplay : MonoBehaviour {

	public EarthTree earthTree;

	void Update () {
		if (earthTree.HasMaximumSize ()) {
			Scenes.INSTANCE.LoadFirstLevel ();
		}
	}
}
