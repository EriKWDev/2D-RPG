using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PerfectPos : MonoBehaviour {

	public bool zPos = true;

	void LateUpdate () {
		if (zPos) {
			transform.position = CoordinateToPixelPerfectPosition ((Vector2)transform.position);
		} else {
			Vector3 pos = transform.position;
			pos.x = CoordinateToPixelPerfectPosition (pos.x);
			pos.y = CoordinateToPixelPerfectPosition (pos.y);
			transform.position = pos;
		}
		SpriteRenderer myRenderer = GetComponent<SpriteRenderer> ();

		if(myRenderer != null)
			myRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (myRenderer.bounds.min).y * -1;
	}

	public float CoordinateToPixelPerfectPosition (float coord) {
		return Mathf.Round (coord * 16f) / 16f;	
	}

	public Vector2 CoordinateToPixelPerfectPosition (Vector2 coord) {
		return new Vector2 (Mathf.Round (coord.x * 16f) / 16f, Mathf.Round (coord.y * 16f) / 16f);
	}
}