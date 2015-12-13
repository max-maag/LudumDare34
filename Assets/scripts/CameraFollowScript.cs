using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFollowScript : MonoBehaviour {

	public GameObject player;
	public float distanceToPlayer;

	public float xOffset;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			-distanceToPlayer);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < player.transform.position.x + xOffset * player.transform.localScale.y) {
			transform.position = new Vector3(
				player.transform.position.x + xOffset * player.transform.localScale.y,
				transform.position.y,
				-distanceToPlayer);
		}
	}
}
