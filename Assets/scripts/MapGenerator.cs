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

		float screenWidth = Camera.main.ViewportToWorldPoint (Vector2.right).x - xLeftOfScreen;

		groundFactory.getEarth (xLeftOfScreen, 0, screenWidth);
		halfOfScreenWidth = screenWidth / 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.x >= xNextGenerate) {
			
			float xRightOfScreen = Camera.main.ViewportToWorldPoint(Vector3.right).x;

			groundFactory.getEarth (xRightOfScreen, 0, halfOfScreenWidth / 2);
			if(Random.Range (0, 2) % 2 == 0) {
				blockFactory.getMultiBlockObstacle(xRightOfScreen + halfOfScreenWidth / 2, (float) heightDistribution.NextNormal(), halfOfScreenWidth / 2, new float[] {2.0f, 1.5f, 3.0f}, new float[] {1.5f, 2.0f});
			} else {
				blockFactory.getSingleBlockObstacle (xRightOfScreen + halfOfScreenWidth / 2, (float) heightDistribution.NextNormal(), halfOfScreenWidth / 2, 10);
			}


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
		}
	}
}
