using UnityEngine;
using System.Collections;

public class CanonBehaviour : MonoBehaviour {

	public GameObject bulletPrefab;
	public float BulletSpeed;

	public AudioSource audioSource;

	void OnShootAnimationFinished() {
		SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();

		if(renderer.isVisible){
			// sign if the bullet should be spawn on the left or right of the cannon
			int sign = -1;
			if(renderer.flipX) 
				sign = 1; 

			Vector3 spawnPosition = transform.position + sign*new Vector3(renderer.bounds.extents.x, 0.0f,0.0f);
			GameObject bullet = (GameObject) GameObject.Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(sign*BulletSpeed, 0);

			audioSource.Play();
		}
	}
}
