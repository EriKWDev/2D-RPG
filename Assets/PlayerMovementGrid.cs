using System.Collections;
using UnityEngine;

class PlayerMovementGrid : MonoBehaviour {
	public float moveSpeed = 3f;
	public float gridSize = 1f;
	public enum Orientation {
		Horizontal,
		Vertical
	};
	public Orientation gridOrientation = Orientation.Horizontal;
	public bool allowDiagonals = false;
	public bool correctDiagonalSpeed = true;
	public Vector2 input;
	public bool isMoving = false;
	public Vector3 startPosition;
	public Vector3 endPosition;
	public float t;
	public float factor;

	public void Update() {
		if (!isMoving) {
			input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (!allowDiagonals) {
				if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
					input.y = 0;
				} else {
					input.x = 0;
				}
			}

			if (input != Vector2.zero) {
				StartCoroutine(move(transform));
			}
		}
	}

	public IEnumerator move(Transform transform) {
		isMoving = true;
		startPosition = transform.position;
		t = 0;

		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
				startPosition.y, startPosition.z + System.Math.Sign(input.y) * gridSize);
		} else {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
				startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
		}

		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
			factor = 0.7071f;
		} else {
			factor = 1f;
		}

		while (t < 1f) {
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}

		isMoving = false;
		yield return 0;
	}
}
