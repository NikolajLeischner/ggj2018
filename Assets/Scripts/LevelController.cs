using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public SunController sunController;

	private AnotherScript anotherScript;

	public int energy;

	// Use this for initialization
	void Start () {
		anotherScript = GetComponent<AnotherScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
