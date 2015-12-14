using UnityEngine;
using System.Collections;

/// <summary>
/// Creates jump sections where the player has to jump between through a gap of two grounds elements.
/// </summary>
public class FlappyBirdSectionGenerator : IMapSectionGenerator {

	private const float MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE = 1f;
	private const float CENTERING_FACTOR = 0.65f;

	private const float MINIMUM_OBSTACLE_WIDTH = 4f;
	private const float MAXIMUM_OBSTACLE_WIDTH = 7f;
	private const float OBSTACLE_HEIGHT = 20f;

	private const float MINIMUM_GROUND_WIDTH = 2f;

	private const float MAXIMUM_BLOCK_HOLE_OFFSET = 4f;

	private const float MINIMUM_HOLE_SIZE = 2.5f;
	private const float MAXIMUM_HOLE_SIZE = 4.5f;

	private const float MINIMUM_GAP_SIZE = 2.5f;
	private const float MAXIMUM_GAP_SIZE = 4f;

	private const float Y_OFFSET_BLOCK_TO_HOLE_DEVIATION = 6;
	private const float Y_OFFSET_OF_BLOCK_DEVIATION = 1;

	private NormalDistribution yOfBlockDistribution;
	private NormalDistribution yBlockHoleOffsetDistribution;
	private NormalDistribution heightOfElementDistribution;
	private NormalDistribution widthOfGroundDistribution;

	public FlappyBirdSectionGenerator() {
		yOfBlockDistribution = new NormalDistribution(0, 5);
		yBlockHoleOffsetDistribution = new NormalDistribution (0, 4);
		heightOfElementDistribution = new NormalDistribution(15, 5);
		widthOfGroundDistribution = new NormalDistribution(2, 1);
	}

	public GameObject GenerateSection(float difficulty, float xNextElement, GameObject lastElement) {
		string tileset = GroundFactory.STONE;
		Vector2 lastElementBounds = lastElement.GetComponentInChildren<Collider2D> ().bounds.max;
	
		// determine a fair y value for the block
		float yBlockLowest = Camera.main.ViewportToWorldPoint (Vector2.zero).y + MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE;
		float yBlockHighest = Camera.main.ViewportToWorldPoint (Vector2.one).y - MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE;

		float yBlockDistributionMode = CENTERING_FACTOR * lastElementBounds.y;
		float yBlock = Random.Range(yBlockDistributionMode - Y_OFFSET_OF_BLOCK_DEVIATION, yBlockDistributionMode + Y_OFFSET_OF_BLOCK_DEVIATION);
		if(yBlock > yBlockHighest) {
			yBlock = yBlockHighest;
		} else if(yBlock < yBlockLowest) {
			yBlock = yBlockLowest;
		}

		// create the block
		float blockWidth = Random.Range(MINIMUM_OBSTACLE_WIDTH, MAXIMUM_OBSTACLE_WIDTH);
		float blockHeight = OBSTACLE_HEIGHT;
		BlockFactory.instance.getSingleBlockObstacle(
			xNextElement,
			yBlock,
			blockWidth,
			blockHeight);
		xNextElement += blockWidth;

		// determine a fair y value for the hole by calculating a normally distributed offset from the block y value
		float holeSize = MAXIMUM_HOLE_SIZE - (MAXIMUM_HOLE_SIZE - MINIMUM_HOLE_SIZE) * difficulty;
		float yHoleLowest = yBlockLowest + holeSize / 2;
		float yHoleHighest = yBlockHighest - holeSize / 2;
		NormalDistribution yOffsetBlockToHoleDistribution = new NormalDistribution (yBlock, Y_OFFSET_BLOCK_TO_HOLE_DEVIATION);
		float yHole = yBlock + (float) yOffsetBlockToHoleDistribution.NextNormal ();
		if(yHole > yHoleHighest) {
			yHole = yHoleHighest;
		} else if(yHole < yHoleLowest) {
			yHole = yHoleLowest;
		}
		float yActualOffsetBlockToHole = yHole - yBlock;

		// create the gap between block and ground
		float gapSize = Random.Range(MINIMUM_GAP_SIZE, MAXIMUM_GAP_SIZE);
		xNextElement += gapSize;

		// generate the grounds
		float groundWidth = Mathf.Max ((float) widthOfGroundDistribution.NextNormal(), MINIMUM_GROUND_WIDTH);
		GameObject newLastElement = GroundFactory.GetGround (
			xNextElement,
			yHole - holeSize / 2,
			groundWidth,
			tileset);
		GroundFactory.GetFloatingGround (
			xNextElement,
			yHole + holeSize / 2, 
			groundWidth,
			tileset);
		
		return newLastElement;
	}
}
