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
	private bool isOnGround;
	public static PlayerMovementScript instance;

	private PlayerAnimationController animationController;

	private Rigidbody2D body;

	void Start() {
		body = this.gameObject.GetComponent<Rigidbody2D>();
		instance = this;

		isOnGround = true;

		// is on ground
		animationController = gameObject.GetComponent<PlayerAnimationController>();
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

	void OnCollisionEnter2D(Collision2D c) {
		if(dead) {
			Respawn ();
		}

		// touching ground after being in the air
		if(!isOnGround && c.gameObject.CompareTag(Tags.GROUND_TAG)) {
			isOnGround = true;
			animationController.onIsOnGroundChanged (isOnGround);
		}
	}

	void OnCollisionExit2D(Collision2D c) {
		// in the air after touching the ground
		if(isOnGround && c.gameObject.CompareTag(Tags.GROUND_TAG)) {
			isOnGround = false;
			animationController.onIsOnGroundChanged (isOnGround);
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

		animationController.onSpawn ();
	}
}
