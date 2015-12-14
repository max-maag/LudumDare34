using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D c) {
		// Destroy the bullet if it hits a block or ground
		if(c.gameObject.CompareTag(Tags.BLOCK_TAG) || c.gameObject.CompareTag(Tags.GROUND_TAG)) {
			Destroy(this.gameObject);
		}
	}
}
