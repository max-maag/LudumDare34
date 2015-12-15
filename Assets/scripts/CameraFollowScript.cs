using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraFollowScript : MonoBehaviour {

	public GameObject player;
	public float distanceToPlayer;

	// offset which is added on the cameara, so that the player will be on the left of the screen
	float xOffset;

	float onePixelInWorldPoint;

	// Use this for initialization
	void Start () {
		// calculates how much one pixel from the camera is in world coordinate if projected on the position of the player 
		Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, distanceToPlayer-player.transform.position.z));
		Vector3 p2 = Camera.main.ScreenToWorldPoint(new Vector3(1, 0,  distanceToPlayer-player.transform.position.z));
		float onePixelInWorldPoint = Mathf.Abs(p.x-p2.x);

		// sets the xOffset to the left screen end - the size of the player so, that he will be on the left of the camera
		xOffset = (Screen.width/2)*onePixelInWorldPoint - player.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
		transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			-distanceToPlayer);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x < player.transform.position.x + xOffset * player.transform.localScale.y) {
					transform.position = new Vector3(
						player.transform.position.x +xOffset,
						transform.position.y,
						-distanceToPlayer);
		}
	}
}
