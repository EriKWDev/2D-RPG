using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	private float accelerationTimeAirborne = .2f;
	private float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;

	Vector3 velocity;
	private Vector2 velocitySmoothing;
	Controller2D controller;
	Vector2 directionalInput;

	void Start() {
		controller = GetComponent<Controller2D> ();
	}

	void Update() {
		CalculateVelocity ();
		controller.Move (velocity * Time.deltaTime, directionalInput);
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
	}

	public void OnJumpInputDown() {}

	public void OnJumpInputUp() {}

	void CalculateVelocity() {
		Vector2 targetVelocity = directionalInput * moveSpeed;
		velocity = Vector2.SmoothDamp (velocity, targetVelocity, ref velocitySmoothing, accelerationTimeGrounded);
	}
}
