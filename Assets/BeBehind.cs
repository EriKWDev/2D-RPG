using UnityEngine;
using System.Collections;

public class BeBehind : MonoBehaviour {

	public Transform objectToBeBehind;

	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, objectToBeBehind.transform.position - (1.1f * objectToBeBehind.right) * objectToBeBehind.localScale.x, 0.08f);

		Vector3 almost = new Vector3 (Mathf.Round (transform.position.x), Mathf.Round (transform.position.y), Mathf.Round (transform.position.z));
		Vector3 almost2 = new Vector3 (Mathf.Round (objectToBeBehind.position.x), Mathf.Round (objectToBeBehind.position.y), Mathf.Round (objectToBeBehind.position.z));

		GetComponent<Animator> ().SetBool ("Walking", (almost == almost2));
		GetComponent<Animator> ().SetFloat ("Speed", (almost == almost2 ? 1f : 0f));

		transform.localScale = objectToBeBehind.localScale;
	}
}
