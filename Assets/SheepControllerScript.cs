using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Sheep Controller Script")]
[DisallowMultipleComponent]
public class SheepControllerScript : MonoBehaviour {

	public GameObject otherPlayer;

	[SerializeField]
	private float maxSpeed = 10f;
	[SerializeField]
	private bool facingRight = true;

	private Animator anim;

	public bool grounded = false;
	[SerializeField]
	private Transform groundCheck;
	[SerializeField]
	private float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	[SerializeField]
	private float jumpForce = 700f;

	[SerializeField]
	private Transform edgeCheckFront;
	[SerializeField]
	private Transform edgeCheckBack;
	[SerializeField]
	private Transform walkingIntoWallCheck;

	public Vector3 spawnPoint;
	[SerializeField]
	private bool onEdge = false;

	void Start () {
		anim = GetComponent<Animator> ();
		spawnPoint = transform.position;
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if(!grounded && GetComponent<Rigidbody2D>().velocity.y <= 0) {
			onEdge = true;
			anim.SetBool ("onEdge", onEdge);
		}else if(grounded || GetComponent<Rigidbody2D>().velocity.y >= 0 || GetComponent<Rigidbody2D>().velocity.y <= -10){
			onEdge = false;
			anim.SetBool ("onEdge", onEdge);
		}
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


		float move = Input.GetAxis ("Horizontal");

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(!Physics2D.OverlapCircle (walkingIntoWallCheck.position, groundRadius, whatIsGround))
			anim.SetFloat ("Speed", Mathf.Abs(move));
		else
			anim.SetFloat ("Speed", 0f);

		if (move < 0 && facingRight) flip ();
		else if (move > 0 && !facingRight) flip ();
	}

	void Update () {
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
		}

		if (Input.GetKeyUp (KeyCode.LeftControl)) {
			Camera.main.GetComponent<PixelCamera> ().player = otherPlayer.transform;
			GetComponent<BeBehind> ().enabled = true;
			otherPlayer.GetComponent<SheepControllerScript> ().facingRight = true;
			otherPlayer.transform.localScale = Vector3.one;
			otherPlayer.GetComponent<BeBehind> ().enabled = false;
			otherPlayer.GetComponent<SheepControllerScript> ().enabled = true;
			GetComponent<SheepControllerScript> ().enabled = false;
		}

		anim.SetBool ("Running", Input.GetKey (KeyCode.LeftShift) && (Mathf.Abs (Input.GetAxis ("Horizontal")) > 0.2f));	
		
	}

	void flip() {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}

	public void turn(bool dir) {							//true -> turn right
		if(!facingRight && dir || facingRight && !dir) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		facingRight = dir;
	}
}













