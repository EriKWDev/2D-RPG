using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour {

	public bool activateByKey = true;
	public KeyCode activationKey = KeyCode.E;
	public string name;

	Dialogue dialogue;

	bool playerIsWithinRadius;

	void Start () {
		dialogue = new Dialogue ();
		dialogue.monologue = true;
		List<Dialogue.Speaker> speaker = new List<Dialogue.Speaker> ();
		List<Dialogue.Line> line = new List<Dialogue.Line> ();
		line.Add (new Dialogue.Line ("It's a " + name, 25f, true, false, speaker[0], false, 0f, null));
		if (name == "") {
			name = gameObject.name;
		}
		speaker.Add (new Dialogue.Speaker (name, line, dialogue));
		dialogue.speakers = speaker;
	}

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
		GameObject.FindGameObjectWithTag ("Player").GetComponent<DialogueReader> ().ReadDialogue (dialogue);
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
