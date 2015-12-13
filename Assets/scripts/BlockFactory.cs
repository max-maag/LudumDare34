using UnityEngine;
using System.Collections;

public class BlockFactory : MonoBehaviour {

	public static BlockFactory instance;
	private const string SINGLE_BLOCK_OBST = "SingleBlockObstacle";

	private BlockFactory() {}

	void Start() {
		instance = this;
	}

	public GameObject getSingleBlock(float xLeft, float yTop, float width, float height) {
		GameObject block = (GameObject) Instantiate(Resources.Load (SINGLE_BLOCK_OBST), Vector3.zero, Quaternion.identity);
		for(int i=0; i < block.transform.childCount; i++)
			block.transform.GetChild(i).localScale = new Vector3 (width, height);
		block.transform.position = new Vector3 (width / 2 + xLeft, -height / 2 + yTop);
		return block;
	}

	// TODO add GroundTunnel
}
