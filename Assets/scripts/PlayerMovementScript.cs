﻿using UnityEngine;
using System.Collections;

/**
 * class managing the movement of the player
 */
public class PlayerMovementScript : MonoBehaviour {

	private const string ROBOT_CHAINS = "RobotChains";
	
	public float movementSpeed;
	public float maxSpeed;
	public float collisionNudgeThreshold;
	public float epsilon;
	private bool isTouchingFloor;
	private bool isDead;

	private PlayerAnimationController animationController;

	private Rigidbody2D body;

	void Start() {
		body = this.gameObject.GetComponent<Rigidbody2D>();

		// is on ground
		animationController = gameObject.GetComponent<PlayerAnimationController>();
	}

	void Update () {
		// control movement
		if(body.velocity.magnitude < maxSpeed) {
			body.AddForce(new Vector2(movementSpeed, 0.0f));
		}
	}

	void OnCollisionEnter2D(Collision2D c) {
		if(c.gameObject.CompareTag(Tags.BLOCK_TAG) || c.gameObject.CompareTag(Tags.GROUND_TAG)) {
			Bounds boundBlock = c.collider.bounds;
			Bounds boundPlayer = gameObject.GetComponent<Collider2D>().bounds;

			// check if player is below the nudge threshold
			if ((boundBlock.max.y - boundPlayer.min.y) > boundPlayer.extents.y * collisionNudgeThreshold) {
				Die();
				return;
			}
		}
			
		// touching ground after being in the air
		bool collisionWithGroundOrBlock = c.gameObject.CompareTag(Tags.GROUND_TAG) || c.gameObject.CompareTag(Tags.BLOCK_TAG);
		if(!isTouchingFloor && collisionWithGroundOrBlock) {
			isTouchingFloor = true;
			animationController.onTouchingFloorChanged (isTouchingFloor);
		}

		// die if hit by bullet
		if(c.gameObject.CompareTag(Tags.BULLET_TAG)) {
			Die();
			Destroy(c.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D c) {
		
		if(c.gameObject.CompareTag(Tags.BLOCK_TAG) || c.gameObject.CompareTag(Tags.GROUND_TAG)) {
			Bounds boundBlock = c.bounds;
			Bounds boundPlayer = gameObject.GetComponent<Collider2D>().bounds;

			// player is inside the nudge threshold -> move the player up
			if((boundBlock.max.y - boundPlayer.min.y) < boundPlayer.extents.y * collisionNudgeThreshold) {
				transform.position = new Vector3(transform.position.x+epsilon, boundBlock.max.y+boundPlayer.extents.y+epsilon, transform.position.z);
			}
		}
	}

	void OnCollisionExit2D(Collision2D c) {
		// in the air after touching the ground
		bool collisionWithGroundOrBlock = c.gameObject.CompareTag(Tags.GROUND_TAG) || c.gameObject.CompareTag(Tags.BLOCK_TAG);
		if(isTouchingFloor && collisionWithGroundOrBlock) {
			isTouchingFloor = false;
			animationController.onTouchingFloorChanged (isTouchingFloor);
			if(c.gameObject.CompareTag(Tags.BLOCK_TAG)) {
				animationController.onLand();
			}
		}
	}

	// check if player is outside the camera
	void OnBecameInvisible() {
		if(Camera.main != null && gameObject.transform.position.y < Camera.main.ViewportToWorldPoint (new Vector2(0.5f, 0.5f)).y)
			Die();
	}

	// respawns the player
	public void Respawn() {
		body.velocity = new Vector2(0.0f, 0.0f);
		transform.position = new Vector3(0.0f, 0.5f, 0.0f);

		animationController.onSpawn ();
	}

	void Die() {
		if (!isDead) {
			isDead = true;

			// no more acceleration
			maxSpeed = 0;
			movementSpeed = 0;
			// keeping the drag at 0 would keep the player falling to the right indefinitely when dying by leaving the screen which would also keep the camera movement
			body.drag = 1.5f;

			animationController.onDie ();

			// spawn two chain prefabs (one in front of the robot, one behind) and toss them around
			Vector3 frontChainsLocalPosition = new Vector3 (-0.342f, -0.33f, -1f);
			GameObject frontChains = (GameObject)Instantiate (Resources.Load (ROBOT_CHAINS), gameObject.transform.position + frontChainsLocalPosition, Quaternion.identity);
			Rigidbody2D frontChainsBody = frontChains.GetComponent<Rigidbody2D> ();
			frontChainsBody.AddForce (new Vector2 (-200f, 300f));
			frontChainsBody.angularVelocity = 1000f;

			Vector3 backChainsLocalPosition = new Vector3 (0.32f, -0.33f, 1f);
			GameObject backChains = (GameObject)Instantiate (Resources.Load (ROBOT_CHAINS), gameObject.transform.position + backChainsLocalPosition, Quaternion.identity);
			Rigidbody2D backChainsBody = backChains.GetComponent<Rigidbody2D> ();
			backChainsBody.AddForce (new Vector2 (50f, 400f));
			backChainsBody.angularVelocity = 1500f;
		}
	}
}
