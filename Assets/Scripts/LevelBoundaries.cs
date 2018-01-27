using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundaries : MonoBehaviour {

	public BoxCollider boundaries;

	public float Left() {
		return boundaries.bounds.min.x;
	}

	public float Right() {
		return boundaries.bounds.max.x;
	}

	public float Top() {
		return boundaries.bounds.max.y;
	}

	public float Bottom() {
		return boundaries.bounds.min.y;
	}
}
