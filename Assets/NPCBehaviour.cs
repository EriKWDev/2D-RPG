﻿using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour {

	public bool activateByKey = true;
	public KeyCode activationKey = KeyCode.E;

	public AudioClip voice;
	public Dialogue dialogue;

	bool playerIsWithinRadius;

	void Update () {
		if (playerIsWithinRadius) {
			if (activateByKey) {
				if (Input.GetKeyDown (activationKey) && GameObject.FindGameObjectWithTag ("Player").GetComponent<DialogueReader> ().isInDialogue == false) {
					Activate ();
				}
			} else {
				Activate ();
			}
		}
	}

	void LateUpdate () {
		GetComponent<SpriteRenderer> ().sortingOrder = (int)(transform.position.y * -1);
		transform.position = CoordinateToPixelPerfectPosition ((Vector2)transform.position);
	}

	void Activate () {
		if(voice != null)
			GameObject.FindGameObjectWithTag ("Player").GetComponent<DialogueReader> ().ReadDialogue (dialogue, voice);
		else
			GameObject.FindGameObjectWithTag ("Player").GetComponent<DialogueReader> ().ReadDialogue (dialogue);
	}

	void LookAtPlayer () {
		
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
