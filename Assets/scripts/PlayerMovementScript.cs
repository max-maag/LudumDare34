using UnityEngine;
using System.Collections;

/**
 * class managing the movement of the player
 */
public class PlayerMovementScript : MonoBehaviour {
	
	public float movementSpeed;
	public float maxSpeed;

	private Rigidbody2D body;

	void Start() {
		body = this.gameObject.GetComponent<Rigidbody2D>();
	}	

	void Update () {
		// control movement
		if(body.velocity.magnitude < maxSpeed) {
			body.AddForce(new Vector2(movementSpeed, 0.0f));
		}
	}

	// check if player is outside the camera
	void OnBecameInvisible() {
		Debug.Log("player died");
		Respawn();
	}

	// respawns the player
	void Respawn() {
		body.velocity = new Vector2(0.0f, 0.0f);
		this.transform.position = new Vector3(0.0f, 0.5f, 0.0f);
	}
}
