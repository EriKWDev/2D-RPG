using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Controller2D : RaycastController {

	[HideInInspector]
	public Vector2 playerInput;
	public bool facingRight = true;
	public bool facingUp = false;
	Animator anim;
	
	public override void Start() {
		base.Start ();
		anim = GetComponent<Animator> ();
	}

	public void Move(Vector2 moveAmount, Vector2 input) {
		playerInput = input;

		anim.SetFloat ("hSpeed", (float)Mathf.Abs (input.x));
		anim.SetFloat ("vSpeed", (float)Mathf.Abs (input.y));

		if (moveAmount.x != 0) {
			int xDir = (int)(moveAmount.x);
			facingUp = (xDir > 0 ? true : false);
			anim.SetBool ("facingRight", facingRight);
		}

		if (moveAmount.y != 0) {
			int yDir = (int)(moveAmount.y);
			facingRight = (yDir > 0 ? true : false);
			anim.SetBool ("facingUp", facingUp);
		}

		transform.Translate (moveAmount);
		transform.position = new Vector3 (Mathf.Round (transform.position.x * 16f) / 16f, Mathf.Round (transform.position.y * 16f) / 16f, transform.position.z);
	}
}
