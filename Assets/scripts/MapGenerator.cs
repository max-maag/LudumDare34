﻿using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";

	public NormalDistribution numberOfBlocksDistribution;
	public NormalDistribution yOffsetOfBlockDistribution;
	public NormalDistribution heightOfBlockDistribution;
	public NormalDistribution widthOfBlockDistribution;
	public NormalDistribution widthOfGroundDistribution;

	private GroundFactory groundFactory;
	private BlockFactory blockFactory;

	/// new elements are generated when the camera's right edge crosses this threshold
	private float xNextGenerate;

	/// x coordinate at which the next element should be placed at (or: x coordinate up to which the level is defined)
	private float xNextElement;

	// Use this for initialization
	void Start () {
		groundFactory = GroundFactory.instance;
		blockFactory = BlockFactory.instance;

		// FIXME for some reason, xLeftOfScreen (and thereby xEndOfInitialGround) varies from time to time - this should not happen
		// create the ground the player spawns on - hard code the first block
		float xLeftOfScreen = Camera.main.ViewportToWorldPoint (Vector2.zero).x;
		GameObject initialGround = groundFactory.getEarth (xLeftOfScreen, 0, 17);
		float xEndOfInitialGround = initialGround.GetComponent<Collider2D> ().bounds.max.x;

		float widthOfInitialBlock = 5;
		blockFactory.getSingleBlockObstacle (xEndOfInitialGround, 1, widthOfInitialBlock, 4);
		xNextElement = xEndOfInitialGround + widthOfInitialBlock;
	}
	
	// Update is called once per frame
	void Update () {
		float xRightOfCamera = Camera.main.ViewportToWorldPoint(Vector3.right).x;
		if(xRightOfCamera >= xNextGenerate) {

			// there is still a lot TODO here
			int numberOfBlocks = (int) numberOfBlocksDistribution.NextNormal ();
			float widthOfAllBlocksAdded = -xNextElement;
			for(int i = 0; i < numberOfBlocks; i++) {
				float heightOfBlock = (float) heightOfBlockDistribution.NextNormal ();
				float widthOfBlock = (float) widthOfBlockDistribution.NextNormal ();
				float yOffsetOfBlock = (float)yOffsetOfBlockDistribution.NextNormal ();

				if(Random.Range (0, 2) % 2 == 0) {
					blockFactory.getMultiBlockObstacle (xNextElement, yOffsetOfBlock, widthOfBlock, new float[] { 2.0f, 3.0f }, new float[] { 3 });
				} else {
					blockFactory.getSingleBlockObstacle (xNextElement, yOffsetOfBlock, widthOfBlock, heightOfBlock);
				}
				xNextElement += widthOfBlock;
			}
			widthOfAllBlocksAdded += xNextElement;
			xNextGenerate += widthOfAllBlocksAdded;
		}
	}
}
