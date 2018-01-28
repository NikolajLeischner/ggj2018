using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceController : MonoBehaviour
{
	public float horizontalSpeed = 0.25f;

	float verticalSpeed = 0f;

	public KeyCode moveLeft = KeyCode.LeftArrow;

	public KeyCode moveRight = KeyCode.RightArrow;

	KeyCode moveUp = KeyCode.UpArrow;

	KeyCode moveDown = KeyCode.DownArrow;

	public LevelBoundaries boundaries;

	bool isActive = true;

	public AudioSource movementSound;

	public float activeVolume = 0.25f;

	public float backgroundVolume = 0.1f;

	void Start() {
		if (movementSound)
			movementSound.volume = backgroundVolume;
	}

	public void Toggle (bool active)
	{
		isActive = active;
	}

	public void MoveToRandomPosition ()
	{
		float randomPosition = Random.Range (boundaries.Left (), boundaries.Right ());
		UpdatePosition (randomPosition, transform.position.y);
	}

	void UpdatePosition (float x, float y)
	{
		var oldPos = transform.position;
		float actualX = Mathf.Clamp (boundaries.Left (), x, boundaries.Right ());
		float actualY = Mathf.Clamp (boundaries.Bottom (), y, boundaries.Top ());
		transform.position = new Vector3 (actualX, actualY, oldPos.z);
	}

	void Update ()
	{
		if (isActive && Scenes.INSTANCE.GameIsRunning()) {
			float horizontal = 0;
			if (Input.GetKey (moveLeft))
				horizontal = -1;
			else if (Input.GetKey (moveRight))
				horizontal = 1;
			float vertical = 0;
			if (Input.GetKey (moveDown))
				vertical = -1;
			else if (Input.GetKey (moveUp))
				vertical = 1;

			if (horizontal != 0) {
				if (movementSound)
					movementSound.volume = activeVolume;
				float movementX = horizontal * horizontalSpeed * Time.deltaTime;
				float movementY = vertical * verticalSpeed * Time.deltaTime;
				float tentativeX = transform.position.x + movementX;
				float tentativeY = transform.position.y + movementY;
				UpdatePosition (tentativeX, tentativeY);
			} else {
				if (movementSound)
					movementSound.volume = backgroundVolume;
			}
		}
	}
}
