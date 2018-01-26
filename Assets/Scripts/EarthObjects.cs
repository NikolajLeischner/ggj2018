using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthObjects : MonoBehaviour {

	Component[] children;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		// get all children
	}
		
	public EarthTree[] getChildren() {
		return GetComponentsInChildren<EarthTree> ();
	}

}
