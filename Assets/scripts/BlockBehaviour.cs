using UnityEngine;
using System.Collections;

public class BlockBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D() {
		PlayerMovementScript.instance.Respawn();
	}

	void OnBecameInvisible() {
		Destroy(this);
	}
}
