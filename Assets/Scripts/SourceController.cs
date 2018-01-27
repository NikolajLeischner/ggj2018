using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceController : MonoBehaviour
{
	public float moveSpeed = 0.25f;

	public KeyCode moveLeft = KeyCode.LeftArrow;

	public KeyCode moveRight = KeyCode.RightArrow;

	public LevelBoundaries boundaries;

	bool isActive = true;

	public void Toggle (bool active)
	{
		isActive = active;
	}

	void Update ()
	{
		if (isActive) {
			float dir = 0;
			if (Input.GetKey (moveLeft))
				dir = -1;
			else if (Input.GetKey (moveRight))
				dir = 1;

			if (dir != 0) {
				float movement = dir * moveSpeed * Time.deltaTime;

				float tentativePosition = transform.position.x + movement;

				var oldPos = transform.position;
				transform.position = new Vector3 (Mathf.Clamp (boundaries.Left (), tentativePosition, boundaries.Right ()), oldPos.y, oldPos.z);
			}
		}
	}
}
