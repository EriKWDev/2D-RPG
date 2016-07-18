using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

	public Transform[] parallaxLayers;
	public float[] parallaxScales;
	public float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;

	void Awake () {
		cam = Camera.main.transform;
	}

	void Start () {
		previousCamPos = cam.transform.position;
	}

	void Update () {
		for (int i = 0; i < parallaxLayers.Length; i++) {
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			if (i > 0) {
				float parallaxedX = parallaxLayers [i].position.x + parallax;
				Vector3 parallaxTargetPosition = new Vector3 (parallaxedX, parallaxLayers [i].position.y, parallaxLayers [i].position.z);
				parallaxLayers [i].position = Vector3.Lerp (parallaxLayers [i].position, parallaxTargetPosition, Time.deltaTime * smoothing);
			} else if (i == 0) {
				parallaxLayers[i].gameObject.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (parallaxLayers[i].gameObject.GetComponent<Renderer> ().material.mainTextureOffset.x - parallax * smoothing, 0f);
			}
		}

		previousCamPos = cam.position;
	}
}
