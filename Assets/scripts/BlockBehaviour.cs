using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().bounds.max.x <
			Camera.main.ViewportToWorldPoint(Vector3.zero).x) {
			Destroy(gameObject);
		}
	}
}
