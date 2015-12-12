using UnityEngine;
using System.Collections;

/**
 * class managing the movement of the player
 */
public class PlayerMovementScript : MonoBehaviour {
	
	public float movementSpeed;
	public float maxSpeed;
	public float fallingThreshold;
	private bool dead;
	public static PlayerMovementScript instance;

	private Rigidbody2D body;

	void Start() {
		body = this.gameObject.GetComponent<Rigidbody2D>();
		instance = this;
	}	

	void Update () {
		// control movement
		if(body.velocity.magnitude < maxSpeed) {
			body.AddForce(new Vector2(movementSpeed, 0.0f));
		}

		// check the falling velocity
		if(body.velocity.y < fallingThreshold) {
			Debug.Log("Player fall to much");
			dead = true;
		}
	}

	void OnCollisionEnter2D() {
		if(dead) {
			Respawn ();
		}
	}

	// check if player is outside the camera
	void OnBecameInvisible() {
		Debug.Log("player died");
		Respawn();
	}

	// respawns the player
	public void Respawn() {
		dead = false;
		body.velocity = new Vector2(0.0f, 0.0f);
		this.transform.position = new Vector3(0.0f, 0.5f, 0.0f);
	}
}
