using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";

	public GameObject block;
	public NormalDistribution positionDistribution;
	public NormalDistribution heightDistribution;
	public NormalDistribution gapSizeDistribution;


	private GameObject player;
	private float nextBlockGenPos;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag(PLAYER_TAG);
		nextBlockGenPos = player.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x >= nextBlockGenPos) {
			float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0,0)).x;

			Vector3 pos = new Vector3(
					rightEdge + 1,
					player.transform.position.y +
					(float) heightDistribution.NextNormal(),
					1f);
			
			GameObject newBlock = (GameObject) Instantiate(block, pos, Quaternion.identity);
			newBlock.transform.GetChild(0).localPosition +=
				new Vector3(0,
					(float) (3*gapSizeDistribution.dev + gapSizeDistribution.NextNormal()),
					0);
			
			nextBlockGenPos += (float) positionDistribution.NextNormal();
		}
	}
}
