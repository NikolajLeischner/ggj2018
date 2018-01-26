using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{
	public float moveSpeed = 0.25f;

	public LevelBoundaries boundaries;

	void Update ()
	{
		float dir = 0;
		if (Input.GetKey (KeyCode.LeftArrow))
			dir = -1;
		else if (Input.GetKey (KeyCode.RightArrow))
			dir = 1;

		if (dir != 0) {
			float movement = dir * moveSpeed * Time.deltaTime;

			float tentativePosition = transform.position.x + movement;

			var oldPos = transform.position;
			transform.position = new Vector3(Mathf.Clamp (boundaries.left, tentativePosition, boundaries.right), oldPos.y, oldPos.z);
		}
	}
}
