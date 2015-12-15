using UnityEngine;
using System.Collections;

public class BlockAndGroundGenerator : IMapSectionGenerator {

	private const float MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE = 1f;
	private const float CENTERING_FACTOR = 0.8f;

	private const float Y_OFFSET_OF_BLOCK_DEVIATION = 2;
	private const float Y_OFFSET_OF_GROUND_DEVIATION_FACTOR_UPWARDS = 0.7f;
	private const float Y_OFFSET_OF_GROUND_DEVIATION_FACTOR_DOWNWARDS = -1f;

	private const float MINIMUM_BLOCK_WIDTH = 4.5f;
	private const float MAXIMUM_BLOCK_WIDTH = 6f;

	private const float MINIMUM_BLOCK_HEIGHT = 3f;

	private const float MINIMUM_GROUND_WIDTH_HARDEST = 2.5f;
	private const float MINIMUM_GROUND_WIDTH_EASIEST = 4;
	private const float MAXIMUM_GROUND_WIDTH = 7;

	private NormalDistribution blockWidthDist;
	private NormalDistribution blockHeightDist;
	private NormalDistribution groundWidthDist;

	public BlockAndGroundGenerator() {
		blockWidthDist = new NormalDistribution(4.5, 3);
		blockHeightDist = new NormalDistribution(8, 5);
		groundWidthDist = new NormalDistribution(4, 2.5);
	}

	public GameObject GenerateSection(float difficulty, float lastX, float lastY, GameObject lastElement) {
		// determine a fair y value for the block
		float yLowest = Camera.main.ViewportToWorldPoint (Vector2.zero).y + MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE;
		float yHighest = Camera.main.ViewportToWorldPoint (Vector2.one).y - MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE;

		// sample the y coordinate of the block from a uniform distribution
		float yBlockDistributionMode = CENTERING_FACTOR * lastY;
		float yBlock = yBlockDistributionMode + Random.Range(-Y_OFFSET_OF_BLOCK_DEVIATION, Y_OFFSET_OF_BLOCK_DEVIATION);
		if(yBlock > yHighest) {
			yBlock = yHighest;
		} else if(yBlock < yLowest) {
			yBlock = yLowest;
		}

		// sample the width of the block from a normal distribution
		float blockWidth = (float) blockWidthDist.NextNormal();
		if(blockWidth < MINIMUM_BLOCK_WIDTH) {
			blockWidth = MINIMUM_BLOCK_WIDTH;
		} else if(blockWidth > MAXIMUM_BLOCK_WIDTH) {
			blockWidth = MAXIMUM_BLOCK_WIDTH;
		}

		// sample the height of the block from a normal distribution
		float blockHeight = (float) blockHeightDist.NextNormal();
		if(blockHeight < MINIMUM_BLOCK_HEIGHT) {
			blockHeight = MINIMUM_BLOCK_HEIGHT;
		}

		BlockFactory.instance.getSingleBlockObstacle(lastX, yBlock, blockWidth, blockHeight);
		lastX += blockWidth;


		// sample the width of the ground from a normal distribution
		float groundWidth = (float) groundWidthDist.NextNormal ();
		float difficultyInfluencedMinimumGroundWidth = MINIMUM_GROUND_WIDTH_EASIEST - (MINIMUM_GROUND_WIDTH_EASIEST - MINIMUM_GROUND_WIDTH_HARDEST) * difficulty;
		if(groundWidth < difficultyInfluencedMinimumGroundWidth) {
			groundWidth = difficultyInfluencedMinimumGroundWidth;
		} else if(groundWidth > MAXIMUM_GROUND_WIDTH) {
			groundWidth = MAXIMUM_GROUND_WIDTH;
		}

		// the y deviation between block and ground depends on the width of the preceding block
		float yGround = blockWidth * Random.Range (Y_OFFSET_OF_GROUND_DEVIATION_FACTOR_DOWNWARDS, Y_OFFSET_OF_GROUND_DEVIATION_FACTOR_UPWARDS);
		if (yGround > yHighest) {
			yGround = yHighest;
		} else if (yGround < yLowest) {
			yGround = yLowest;
		}

		// low chance to randomly get a sand tile
		string randomTileset = Random.Range (0, 1) < 0.15f? GroundFactory.GRASS : GroundFactory.SAND;

		return GroundFactory.GetGround(
			lastX,
			yGround,
			groundWidth,
			randomTileset
		);
	}
}
