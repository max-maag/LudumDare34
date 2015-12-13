using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFollowScript : MonoBehaviour {

	public GameObject player;
	public float distanceToPlayer;

	public float xOffset;

	void Awake() {
		transform.position = new Vector3(
			player.transform.position.x + xOffset * player.transform.localScale.y,
			transform.position.y,
			-distanceToPlayer);
	}

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(
			player.transform.position.x + xOffset * player.transform.localScale.y,
			transform.position.y,
			-distanceToPlayer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(
			player.transform.position.x + xOffset * player.transform.localScale.y,
			transform.position.y,
			-distanceToPlayer);
	}
}
