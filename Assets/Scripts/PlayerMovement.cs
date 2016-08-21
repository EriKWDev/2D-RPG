using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator)), RequireComponent(typeof (DialogueReader))]
public class PlayerMovement : MonoBehaviour {

	public Player currentPlayer = Player.Angie;

	Animator playerAnimator;

	public enum Player {
		Angie,
		KillerAngie,
		Pudding,
		FancyPudding,
		Professor
	}

	public float movementSpeed = 1f;
	private string direction = "up";
	private bool canMove = true;

	void Start () {
		playerAnimator = GetComponent<Animator> ();
	}

	void LateUpdate () {
		GetComponent<SpriteRenderer> ().sortingOrder = (int)CoordinateToPixelPerfectPosition (transform.position.y * -1);
	}

	void Update () {
		UpdatePlayerSprite ();
		Walk ();
	}

	void Walk () {
		if (GetComponent<DialogueReader> ().isInDialogue == false) {
			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 velocity = input * Time.deltaTime * movementSpeed;

			playerAnimator.SetBool ("isWalking", input != Vector2.zero ? true : false);
			if (input != Vector2.zero) {
				playerAnimator.SetFloat ("inputX", input.x);
				playerAnimator.SetFloat ("inputY", input.y);


				if (input.x > 0) {
					direction = "right";
				} else if (input.x < 0) {
					direction = "left";
				}
				if (input.y > 0) {
					direction = "up";
				} else if (input.y < 0) {
					direction = "down";
				}
			}

			Collider2D[] tmp = Physics2D.OverlapCircleAll ((Vector2)transform.position, 0.5f);

			if(canMove)
				transform.Translate (CoordinateToPixelPerfectPosition (velocity));

		} else {
			playerAnimator.SetBool ("isWalking", false);
		}
	}

	void UpdatePlayerSprite () {
		switch (currentPlayer) {
		case Player.Angie:
			playerAnimator.SetBool ("isAngie", true);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.KillerAngie: 
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isKillerAngie", true);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.Pudding:
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", true);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.FancyPudding:
			playerAnimator.SetBool ("isAngie", true);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", true);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.Professor:
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", true);
			break;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		print (other.name);
		if (other.gameObject.tag == "Collisions") {
			canMove = false;
		}
	}

	public bool IsFacing (GameObject other) {
		return false;
	}

	public float CoordinateToPixelPerfectPosition (float coord) {
		return Mathf.Round (coord * 16f) / 16f;	
	}

	public Vector2 CoordinateToPixelPerfectPosition (Vector2 coord) {
		return new Vector2 (Mathf.Round (coord.x * 16f) / 16f, Mathf.Round (coord.y * 16f) / 16f);
	}
}
