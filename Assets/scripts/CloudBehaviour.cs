using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);

		if(gameObject.GetComponent<SpriteRenderer>().sprite != null && transform.position.x + gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.x < Camera.main.ViewportToWorldPoint(Vector3.zero).x) {
			Destroy(gameObject);
		}
	}
}
