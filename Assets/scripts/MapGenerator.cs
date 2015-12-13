using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";

	public NormalDistribution positionDistribution;
	public NormalDistribution heightDistribution;
	public NormalDistribution gapSizeDistribution;

	private GameObject player;
	private GroundFactory groundFactory;
	private BlockFactory blockFactory;

	private float halfOfScreenWidth;

	/// new map elements are generated when the player reaches this x coordinate
	private float xNextGenerate;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag(PLAYER_TAG);
		groundFactory = GroundFactory.instance;
		blockFactory = BlockFactory.instance;

		float xLeftOfScreen = Camera.main.ViewportToWorldPoint (Vector2.zero).x;
		Debug.Log ("xLeftOfScreen " + xLeftOfScreen);	// FIXME why is this -11 in Start() when it should be -13!?
		float screenWidth = Camera.main.ViewportToWorldPoint (Vector2.right).x - xLeftOfScreen;

		groundFactory.getEarth (xLeftOfScreen, 0, screenWidth);
		halfOfScreenWidth = screenWidth / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x >= xNextGenerate) {
			Debug.Log ("I'm at x = " + xNextGenerate + " and I'll generate some level now");
			float xRightOfScreen = Camera.main.ViewportToWorldPoint(Vector3.right).x;

			groundFactory.getEarth (xRightOfScreen, 0, halfOfScreenWidth / 2);
			blockFactory.getSingleBlock (xRightOfScreen + halfOfScreenWidth / 2, (float) heightDistribution.NextNormal(), halfOfScreenWidth / 2, 10);

//			Vector3 pos = new Vector3(
//					rightEdge + 1,
//					player.transform.position.y +
//					,
//					1f);
//			
//			GameObject newBlock = (GameObject) Instantiate(block, pos, Quaternion.identity);
//			newBlock.transform.GetChild(0).localPosition +=
//				new Vector3(0,
//					(float) (3*gapSizeDistribution.dev + gapSizeDistribution.NextNormal()),
//					0);
			
			xNextGenerate += halfOfScreenWidth;
			Debug.Log ("next level generation at x = " + xNextGenerate);
		}
	}
}
