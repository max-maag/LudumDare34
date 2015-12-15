using UnityEngine;
using System.Collections;

public class CannonSectionGenerator : IMapSectionGenerator {

	private const float MINIMUM_HEIGHT_OF_ELEMENT_VISIBLE = 1f;
	private const float CENTERING_FACTOR = 0.8f;


	private const float Y_OFFSET_OF_BLOCK_DEVIATION = 2;
	private const float Y_OFFSET_OF_GROUND_DEVIATION_FACTOR_UPWARDS = 0.7f;
	private const float Y_OFFSET_OF_GROUND_DEVIATION_FACTOR_DOWNWARDS = -1f;

	public NormalDistribution yOffsetOfBlockDistribution;
	public NormalDistribution widthOfGroundBlockDistribution;
	public NormalDistribution widthOfCannonBlockDistribution;

	public CannonSectionGenerator(
		NormalDistribution yOffset,
		NormalDistribution widthGroundBlock,
		NormalDistribution widthCannonBlock) {

		yOffsetOfBlockDistribution = yOffset;
		widthOfGroundBlockDistribution = widthGroundBlock;
		widthOfCannonBlockDistribution = widthCannonBlock;

	}

	public GameObject GenerateSection(float difficulty, float xNextElement,float lastY, GameObject lastElement) {
		float widthOfGroundBlock = (float) widthOfGroundBlockDistribution.NextNormal ();
		float widthOfCanonBlock = (float) widthOfCannonBlockDistribution.NextNormal ();

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

		BlockFactory.instance.getSingleBlockObstacle(xNextElement, yBlock, widthOfGroundBlock, 2);
		float lastX = xNextElement + widthOfGroundBlock;

		GroundFactory.GetGround(
			lastX,
			lastY,
			widthOfGroundBlock, GroundFactory.GRASS);

		float cannonX = lastX+widthOfGroundBlock;

		switch(Random.Range (0, 2)) {
		case 0: BlockFactory.instance.getMultBlockObstacleWithCanon (cannonX, lastY+6, widthOfCanonBlock, new float[] { 1.0f, 1.0f }, new float[] { 2.5f }, new bool[] {false, true});
			break;
		case 1: BlockFactory.instance.getMultBlockObstacleWithCanon (cannonX, lastY-1, widthOfCanonBlock, new float[] { 1.0f, 1.0f }, new float[] { 2.5f }, new bool[] {true, false});
			break;
		}

		float GroundX = cannonX+widthOfCanonBlock;

		return GroundFactory.GetGround(
			GroundX ,
			lastY,
			widthOfGroundBlock-2, GroundFactory.GRASS);
	}
}
