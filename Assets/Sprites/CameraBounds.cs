using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraBounds : MonoBehaviour {

	public BoxCollider2D currentRoomBounds;
	private Vector3 currentRoomMinBounds;
	private Vector3 currentRoomMaxBounds;
	private float halfHeight;
	private float halfWidth;
	public GameObject playerObject;
	public int scaling = 1;

	void Start () {
		if (currentRoomBounds != null) {
			currentRoomMinBounds = currentRoomBounds.bounds.min;
			currentRoomMaxBounds = currentRoomBounds.bounds.max;
		}

		halfHeight = Camera.main.orthographicSize;
		halfWidth = halfHeight * Screen.width / Screen.height;
			
		//playerObject = GameObject.FindGameObjectWithTag ("Player"); 
		Camera.main.orthographicSize = (Screen.height / 16f / 2f) / scaling;
	}

	void Update () {
		transform.position = new Vector3 (CoordinateToPixelPerfectPosition (playerObject.transform.position.x), CoordinateToPixelPerfectPosition (playerObject.transform.position.y), -10f);
		//float clampedX = Mathf.Clamp (transform.position.x, currentRoomMaxBounds.x + halfWidth, currentRoomMinBounds.x - halfWidth);
		//float clampedY = Mathf.Clamp (transform.position.y, currentRoomMaxBounds.y + halfHeight, currentRoomMinBounds.y - halfHeight);
		//transform.position = new Vector3 (clampedX, clampedY, transform.position.z);
	}

	public float CoordinateToPixelPerfectPosition (float coord) {
		return Mathf.Round (coord * 16f) / 16f;	
	}
}
