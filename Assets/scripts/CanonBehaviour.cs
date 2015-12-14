using UnityEngine;
using System.Collections;

public class CanonBehaviour : MonoBehaviour {

	// cooldown of the canon
	private float cooldown;

	// shooting rate of the canon
	public float shootingRate;

	public GameObject bulletPrefab;
	public float BulletSpeed;

	private Animator animator;

	const string ANIMATOR_STATE_PARAMETER = "animState";
	const int STATE_SHOOT = 1;

	void Start() {
		animator = gameObject.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {

		cooldown -= Time.deltaTime;
	
		if(cooldown <= 0.0f) {
			cooldown = shootingRate;
			shoot();
		}
	}

	void shoot() {
		animator.SetInteger (ANIMATOR_STATE_PARAMETER, STATE_SHOOT);
	}

	void OnShootAnimationFinished() {
		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

		// sign if the bullet should be spawn on the left or right of the cannon
		int sign = -1;
		if(renderer.flipX) 
			sign = 1; 

		Vector3 spawnPosition = transform.position + sign*new Vector3(renderer.bounds.extents.x, 0.0f,0.0f);
		GameObject bullet = (GameObject) GameObject.Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(sign*BulletSpeed, 0);
	}
}
