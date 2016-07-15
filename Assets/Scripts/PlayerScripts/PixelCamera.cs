using UnityEngine;
using System.Collections;

public class PixelCamera : MonoBehaviour {
	public Transform player;
	private BoxCollider2D cameraBox;

	void Start () {
		cameraBox = GetComponent<BoxCollider2D> ();
	}

	void Update() {
		AspectRatioBoxChange ();
		FollowPlayer ();
	}

	void AspectRatioBoxChange () {
		if (Camera.main.aspect >= 1.6f && Camera.main.aspect < 1.7f) {
			cameraBox.size = new Vector2 (23, 14.3f);
		}

		if (Camera.main.aspect >= 1.7f && Camera.main.aspect < 1.8f) {
			cameraBox.size = new Vector2 (25.47f, 14.3f);
		}

		if (Camera.main.aspect >= 1.25f && Camera.main.aspect < 1.3f) {
			cameraBox.size = new Vector2 (18f, 14.3f);
		}

		if (Camera.main.aspect >= 1.3f && Camera.main.aspect < 1.4f) {
			cameraBox.size = new Vector2 (19.13f, 14.3f);
		}

		if (Camera.main.aspect >= 1.5f && Camera.main.aspect < 1.6f) {
			cameraBox.size = new Vector2 (21.6f, 14.3f);
		}
	}

	void FollowPlayer () {
		GameObject boundary = GameObject.Find ("Boundary");
		if (boundary != null) {
			Vector3 pos = new Vector3 (Mathf.Clamp (player.position.x, boundary.GetComponent<BoxCollider2D>  ().bounds.min.x + cameraBox.size.x / 2f, boundary.GetComponent<BoxCollider2D> ().bounds.max.x - cameraBox.size.x / 2f),
											  Mathf.Clamp (player.position.y, boundary.GetComponent<BoxCollider2D>  ().bounds.min.y + cameraBox.size.y / 2f, boundary.GetComponent<BoxCollider2D> ().bounds.max.y - cameraBox.size.y / 2f),
										      transform.position.z);

			transform.position = Vector3.Lerp (transform.position, pos, Time.deltaTime * 15f);
		}
	}
}
