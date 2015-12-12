using UnityEngine;
using System.Collections;

public class BlockFactory : MonoBehaviour {

	public static BlockFactory instance;
	private const string SINGLE_BLOCK = "SingleBlock";

	private BlockFactory() {}

	void Start() {
		instance = this;
	}

	public GameObject getSingleBlock(float width, float x, float y) {
		GameObject block = (GameObject) Instantiate(Resources.Load (SINGLE_BLOCK), new Vector3(x, y), Quaternion.identity);
		block.transform.localScale = new Vector3 (width, 1);
		return block;
	}

	// TODO add GroundTunnel
}
