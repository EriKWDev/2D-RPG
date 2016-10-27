using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour {

	public bool activateByKey = true;
	public KeyCode activationKey = KeyCode.E;

	public AudioClip voice;

	bool playerIsWithinRadius;
	GameObject player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update () {
		if (playerIsWithinRadius) {
			if (activateByKey) {
				if (Input.GetKeyDown (activationKey)) {
					Activate ();
				}
			} else {
				Activate ();
			}
		}
	}

	void LateUpdate () {
		FixSortingLayer ();
		transform.position = CoordinateToPixelPerfectPosition ((Vector2)transform.position);
	}

	void FixSortingLayer () {
		SpriteRenderer myRenderer = GetComponent<SpriteRenderer> ();

		myRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (myRenderer.bounds.min).y * -1;
	}

	void Activate () {
		
	}

	public float CoordinateToPixelPerfectPosition (float coord) {
		return Mathf.Round (coord * 16f) / 16f;	
	}

	public Vector2 CoordinateToPixelPerfectPosition (Vector2 coord) {
		return new Vector2 (Mathf.Round (coord.x * 16f) / 16f, Mathf.Round (coord.y * 16f) / 16f);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			playerIsWithinRadius = true;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Player") {
			playerIsWithinRadius = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			playerIsWithinRadius = false;
		}
	}
}
