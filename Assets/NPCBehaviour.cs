using UnityEngine;
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
	}

	void Activate () {
		if(voice != null)
			GameObject.FindGameObjectWithTag ("Player").GetComponent<DialogueReader> ().ReadDialogue (dialogue, voice);
		else
			GameObject.FindGameObjectWithTag ("Player").GetComponent<DialogueReader> ().ReadDialogue (dialogue);
	}

	void LookAtPlayer () {
		
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
