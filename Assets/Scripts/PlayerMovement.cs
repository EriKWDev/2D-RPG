using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Animator))]
public class PlayerMovement : MonoBehaviour {

	public Player currentPlayer = Player.MrFancy;
	Animator playerAnimator;
	Rigidbody2D myRigidbody;
	public bool gridMove = true;
	private bool animate = false;

	public enum Player {
		Angie,
		BedTimeAngie,
		KillerAngie,
		Pudding,
		FancyPudding,
		Professor,
		MrFancy
	}

	public float movementSpeed = 1f;
	private string direction = "up";
	private bool canMove = true;
	public float timer = 0.15f;
	private float t2 = 0f;

	void Start () {
		t2 = timer;
		playerAnimator = GetComponent<Animator> ();
		myRigidbody = GetComponent<Rigidbody2D> ();
	}

	void LateUpdate () {
		SpriteRenderer myRenderer = GetComponent<SpriteRenderer> ();

		myRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (myRenderer.bounds.min).y * -1;
	}

	void Update () {
		UpdateAnimatorValues ();
		Walk ();
	}

	void Walk () {
		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		//if (gridMove) {
		if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) {
			input.y = 0;
		} else {
			input.x = 0;
		}
		//}

		playerAnimator.SetBool ("isWalking", ((input != Vector2.zero ? true : false)));

		if (gridMove ? input != Vector2.zero && !isMoving : input != Vector2.zero) {
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

		if (input != Vector2.zero) {
			t2 -= Time.deltaTime;
		} else {
			t2 = timer;
		}

		animate = (t2 <= 0f ? true : false);

		if (!gridMove) {
			Vector2 velocity = Time.deltaTime * input * movementSpeed;
			if (canMove) {
				//myRigidbody.MovePosition (CoordinateToPixelPerfectPosition (transform.position + new Vector3 (velocity.x, velocity.y, 0f)));
				//myRigidbody.velocity = velocity * 5f;
				transform.Translate (CoordinateToPixelPerfectPosition (velocity));
				//transform.position = PixelPerfect.RoundToArtPixelGrid (transform.position + new Vector3 (velocity.x, velocity.y, 0f));
			}
		} else if (!isMoving && input != Vector2.zero && animate) {
			StartCoroutine (GridWalk (input));
		}
	}

	private float t1 = 0f;
	private float gridSize = 1f;
	private float factor = 1f;
	public bool isMoving = false;
	private Vector2 startPosition = Vector2.zero;
	private Vector2 endPosition = Vector2.zero;

	IEnumerator GridWalk (Vector2 input) {
		isMoving = true;
		playerAnimator.SetBool ("isWalking", (gridMove == true ? isMoving : (input != Vector2.zero ? true : false)));
		startPosition = transform.position;
		t1 = 0;
		endPosition = startPosition + input;

		while (t1 < 1f) {
			t1 += Time.deltaTime * (movementSpeed/gridSize) * factor;
			transform.position = Vector2.MoveTowards(startPosition, endPosition, t1);
			yield return null;
		}
		isMoving = false;
	}

	void UpdateAnimatorValues () {
		playerAnimator.SetBool ("isAngie", (currentPlayer == Player.Angie ? true : false));
		playerAnimator.SetBool ("isBedTimeAngie", (currentPlayer == Player.BedTimeAngie ? true : false));
		playerAnimator.SetBool ("isKillerAngie", (currentPlayer == Player.KillerAngie ? true : false));
		playerAnimator.SetBool ("isPudding", (currentPlayer == Player.Pudding ? true : false));
		playerAnimator.SetBool ("isFancyPudding", (currentPlayer == Player.FancyPudding ? true : false));
		playerAnimator.SetBool ("isProfessor", (currentPlayer == Player.Professor ? true : false));
		playerAnimator.SetBool ("isMrFancy", (currentPlayer == Player.MrFancy ? true : false));

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
