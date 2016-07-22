using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator)), RequireComponent(typeof (Rigidbody2D)), RequireComponent(typeof (DialogueReader))]
public class PlayerMovement : MonoBehaviour {

	//Rigidbody2D myRigidbody;
	Animator animator;
	public float movementSpeed = 1f;

	void Start () {
		//myRigidbody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
	}

	void LateUpdate () {
		GetComponent<SpriteRenderer> ().sortingOrder = (int)CoordinateToPixelPerfectPosition (transform.position.y * -1);
	}

	void Update () {
		if (GetComponent<DialogueReader> ().isInDialogue == false) {
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 velocity = input * Time.deltaTime * movementSpeed;

			animator.SetBool ("isWalking", input != Vector2.zero ? true : false);
			if (input != Vector2.zero) {
				animator.SetFloat ("inputX", input.x);
				animator.SetFloat ("inputY", input.y);
			}

			transform.Translate (CoordinateToPixelPerfectPosition (velocity));
		} else {
			animator.SetBool ("isWalking", false);
		}
	}

	public float CoordinateToPixelPerfectPosition (float coord) {
		return Mathf.Round (coord * 16f) / 16f;	
	}

	public Vector2 CoordinateToPixelPerfectPosition (Vector2 coord) {
		return new Vector2 (Mathf.Round (coord.x * 16f) / 16f, Mathf.Round (coord.y * 16f) / 16f);
	}
}
