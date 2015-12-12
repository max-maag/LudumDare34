using UnityEngine;
using System.Collections;

/**
 * class managing the movement of the player
 */
public class PlayerMovementScript : MonoBehaviour {
	
	public float movementSpeed;
	public float maxSpeed;
	public float collisionNudgeThreshold;
	public float epsilon;
	private bool isOnGround = true;

	private PlayerAnimationController animationController;

	private Rigidbody2D body;

	void Start() {
		body = this.gameObject.GetComponent<Rigidbody2D>();

		animationController = gameObject.GetComponent<PlayerAnimationController>();
	}

	void Update () {
		// control movement
		if(body.velocity.magnitude < maxSpeed) {
			body.AddForce(new Vector2(movementSpeed, 0.0f));
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		if(c.gameObject.tag == Tags.BLOCK_TAG) {
			Bounds boundBlock = c.collider.bounds;
			Bounds boundPlayer = this.gameObject.GetComponent<Collider2D>().bounds;

			// check if player is below the nudge threshold
			if ((boundBlock.max.y - boundPlayer.min.y) > boundBlock.extents.y * collisionNudgeThreshold) {
				Respawn();
				return;
			}

			// player is inside the nudge threshold -> move the player up
			if((boundBlock.max.y - boundPlayer.min.y) > 0) {
				this.transform.position = new Vector3(transform.position.x, boundBlock.max.y+boundBlock.extents.y+epsilon, transform.position.z);
			}
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
		isOnGround = true;
		body.velocity = new Vector2(0.0f, 0.0f);
		this.transform.position = new Vector3(0.0f, 0.5f, 0.0f);

		animationController.onSpawn ();
	}
}
