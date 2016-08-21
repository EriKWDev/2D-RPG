using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

	public bool activateByKey = true;
	public KeyCode activationKey = KeyCode.E;
	public string objectName = "";
	bool playerIsWithinRadius;

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
		GetComponent<SpriteRenderer> ().sortingOrder = (int)(transform.position.y * -1);
	}

	void Activate () {

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
