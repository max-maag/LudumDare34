using UnityEngine;
using System.Collections;

public class BlockFactory : MonoBehaviour {

	public static BlockFactory instance;
	private const string MULTI_BLOCK_OBST = "MultiBlockObstacle";
	private const string SINGLE_BLOCK = "SingleBlock";
	private const string CANNON = "Canon";

	private BlockFactory() {}

	void Awake() {
		instance = this;
	}

	/// <summary>
	/// Gets a single block obstacle with the given properties at the given position.
	/// </summary>
	/// <returns>a single block obstacle</returns>
	/// <param name="xLeft">X part of the top left anchor of the obstacle</param>
	/// <param name="yTop">Y part of the top left anchor of the obstacle</param>
	/// <param name="width">the width of the obstacle</param>
	/// <param name="height">the height of the obstacle</param>
	public GameObject getSingleBlockObstacle(float xLeft, float yTop, float width, float height) {
		GameObject obstacle = (GameObject)Instantiate (Resources.Load (MULTI_BLOCK_OBST), Vector3.zero, Quaternion.identity);
		GameObject singleBlock = (GameObject)Instantiate (Resources.Load (SINGLE_BLOCK), Vector3.zero, Quaternion.identity);
		singleBlock.transform.parent = obstacle.transform;
		singleBlock.transform.localPosition = new Vector3 (0, - height / 2);
		singleBlock.transform.localScale = new Vector3 (width, height);
		obstacle.transform.position = new Vector3 (width/2 + xLeft, yTop);
		return obstacle;
	}

	/// <summary>
	/// Gets a single block obstacle with the given properties at the given position.
	/// </summary>
	/// <returns>a multi block obstacle</returns>
	/// <param name="xLeft">X part of the top left anchor of the obstacle</param>
	/// <param name="yTop">Y part of the top left anchor of the obstacle</param>
	/// <param name="width">the width of the obstacle</param>
	/// <param name="heights">the heights of the individual blocks</param>
	/// <param name="sepHeights">the heights of the separators between the blocks (i.e. one separator less than there are blocks)</param>
	public GameObject getMultiBlockObstacle(float xLeft, float yTop, float width, float[] heights, float[] sepHeights) {
		return getMultBlockObstacleWithCanon(xLeft, yTop, width, heights , sepHeights, new bool[heights.Length]);
	}

	/// <summary>
	/// Gets a multi block obstacle including cannons with the given properties at the given position.
	/// </summary>
	/// <returns>a multi block obstacle</returns>
	/// <param name="xLeft">X part of the top left anchor of the obstacle</param>
	/// <param name="yTop">Y part of the top left anchor of the obstacle</param>
	/// <param name="width">the width of the obstacle</param>
	/// <param name="heights">the heights of the individual blocks</param>
	/// <param name="sepHeights">the heights of the separators between the blocks (i.e. one separator less than there are blocks)</param>
	/// <param name="cannonsOnBlock"> if a cannon should be spawn on the individual block or not 
	public GameObject getMultBlockObstacleWithCanon(float xLeft, float yTop, float width, float[] heights, float[] sepHeights, bool[] cannonsOnBlock) {
		if (heights.Length -1 != sepHeights.Length) {
			Debug.LogError ("The number of separator heights must be the number of heights minus one (was: " + sepHeights.Length + " and " + heights.Length + ")");
		}
		if (heights.Length != cannonsOnBlock.Length) {
			Debug.LogError ("The number of heights and number of cannons on the block must be the same (was: " + heights.Length + " and " +cannonsOnBlock.Length + ")");
		}

		int numberOfBlocks = heights.Length;
		GameObject obstacle = (GameObject)Instantiate (Resources.Load (MULTI_BLOCK_OBST), Vector3.zero, Quaternion.identity);

		float heightAccu = 0.0f;
		for(int i = 0; i < numberOfBlocks; i++) {
			GameObject singleBlock = (GameObject)Instantiate (Resources.Load (SINGLE_BLOCK), Vector3.zero, Quaternion.identity);
			singleBlock.transform.parent = obstacle.transform;
			singleBlock.transform.localPosition = new Vector3 (0, heightAccu - heights [i] / 2);
			singleBlock.transform.localScale = new Vector3 (width, heights [i]);

			// create a cannon on the block
			if(cannonsOnBlock[i]) {
				GameObject cannon = (GameObject) Instantiate (Resources.Load (CANNON), Vector3.zero, Quaternion.identity);
				cannon.transform.parent = obstacle.transform;
				cannon.transform.localPosition = singleBlock.transform.localPosition+ new Vector3(0.0f,cannon.GetComponent<SpriteRenderer>().bounds.extents.y  + singleBlock.GetComponent<SpriteRenderer>().bounds.extents.y);
			}

			if(i != numberOfBlocks -1) {
				heightAccu += -heights [i] - sepHeights [i];
			}
		}

		obstacle.transform.position = new Vector3 (width/2 + xLeft, yTop);
		return obstacle;
	}
}
