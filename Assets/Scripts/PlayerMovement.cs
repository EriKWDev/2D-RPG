using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
public class PlayerMovement : MonoBehaviour {

	public Player currentPlayer = Player.Angie;
	Animator playerAnimator;
	Rigidbody2D myRigidbody;

	public enum Player {
		Angie,
		BedTimeAngie,
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
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void LateUpdate () {
		SpriteRenderer myRenderer = GetComponent<SpriteRenderer> ();

		myRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (myRenderer.bounds.min).y * -1;
	}

	void Update () {
		UpdatePlayerSprite ();
		Walk ();
	}

	void Walk () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 velocity = input * Time.deltaTime * movementSpeed;

		playerAnimator.SetBool ("isWalking", input != Vector2.zero ? true : false);
		if (input != Vector2.zero) {
			playerAnimator.SetFloat ("inputX", input.x);
			playerAnimator.SetFloat ("inputY", input.y);


			if (input.x > 0) {
				direction = "right";
				playerAnimator.SetFloat ("direction", 2f);
			} else if (input.x < 0) {
				direction = "left";
				playerAnimator.SetFloat ("direction", 1f);
			}
			if (input.y > 0) {
				direction = "up";
				playerAnimator.SetFloat ("direction", 3f);
			} else if (input.y < 0) {
				direction = "down";
				playerAnimator.SetFloat ("direction", 0f);
			}
		}

		if (canMove) {
			//myRigidbody.MovePosition (CoordinateToPixelPerfectPosition (transform.position + new Vector3 (velocity.x, velocity.y, 0f)));
			//myRigidbody.velocity = velocity * 5f;
			transform.Translate (CoordinateToPixelPerfectPosition (velocity));
			//transform.position = PixelPerfect.RoundToArtPixelGrid (transform.position + new Vector3 (velocity.x, velocity.y, 0f));
		}
	}

	void UpdatePlayerSprite () {
		switch (currentPlayer) {
		case Player.Angie:
			playerAnimator.SetBool ("isAngie", true);
			playerAnimator.SetBool ("isBedTimeAngie", false);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.BedTimeAngie:
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isBedTimeAngie", true);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.KillerAngie: 
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isBedTimeAngie", false);
			playerAnimator.SetBool ("isKillerAngie", true);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.Pudding:
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isBedTimeAngie", false);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", true);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.FancyPudding:
			playerAnimator.SetBool ("isAngie", true);
			playerAnimator.SetBool ("isBedTimeAngie", false);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", true);
			playerAnimator.SetBool ("isProfessor", false);
			break;

		case Player.Professor:
			playerAnimator.SetBool ("isAngie", false);
			playerAnimator.SetBool ("isBedTimeAngie", false);
			playerAnimator.SetBool ("isKillerAngie", false);
			playerAnimator.SetBool ("isPudding", false);
			playerAnimator.SetBool ("isFancyPudding", false);
			playerAnimator.SetBool ("isProfessor", true);
			break;
		}

		playerAnimator.SetBool ("isPressingE", Input.GetKey (KeyCode.E));
	}

	void OnCollisionEnter2D (Collision2D other) {
		print (other.gameObject.name);
		if (other.gameObject.tag == "Colliders") {
			canMove = true;
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

	public Vector2 CoordinateToPixelPerfectPosition (Vector3 coord) {
		return new Vector2 (Mathf.Round (coord.x * 16f) / 16f, Mathf.Round (coord.y * 16f) / 16f);
	}
}
