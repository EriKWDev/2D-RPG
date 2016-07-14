using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PixelCamera : MonoBehaviour {

	public Transform player;
	public float PPUScale = 1f;

	void Update() {
		GetComponent<Camera> ().orthographicSize = (Screen.height / (PPUScale * 16f)) * 0.5f;
	}

	void LateUpdate() {
		Vector3 roundPos = new Vector3 (RoundToNearestPixel(player.position.x, Camera.main), RoundToNearestPixel(player.position.y, Camera.main), -10f);
		transform.position = Vector3.Lerp (transform.position, roundPos, Time.deltaTime * 15f);
	}

	public static float RoundToNearestPixel (float unityUnits, Camera viewingCamera) {
		float valueInPixels = (Screen.height / (viewingCamera.orthographicSize * 2)) * unityUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float adjustedUnityUnits = valueInPixels / (Screen.height / (viewingCamera.orthographicSize * 2));
        return adjustedUnityUnits;
	}
}
