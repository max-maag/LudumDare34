using UnityEngine;
using System.Collections;

public class JumpSectionGenerator : IMapSectionGenerator {
	private const float MIN_GAP = 1f; // player width

	private const float PLAYER_MAX_SPEED = 5f;
	private const float BLOCK_MAX_SPEED = 8f;
	private const float PLAYER_GRAVITY = -2 * 9.81f;

	private const float MIN_BLOCK_LENGTH = 0.2f;
	private const float MAX_BLOCK_LENGTH = 0.2f;

	public NormalDistribution yBlockDistribution;
	public NormalDistribution blockWidthDistribution;
	public NormalDistribution blockHeightDistribution;
	public NormalDistribution groundWidthDistribution;

	public JumpSectionGenerator(
		NormalDistribution yBlock,
		NormalDistribution blockWidth,
		NormalDistribution blockHeight,
		NormalDistribution groundWidth) {

		yBlockDistribution = yBlock;
		blockWidthDistribution = blockWidth;
		blockHeightDistribution = blockHeight;
		groundWidthDistribution = groundWidth;
	}


	public GameObject GenerateSection(float difficulty, float lastX, float lastY, GameObject lastElement) {
		float blockWidth = (float) blockWidthDistribution.NextNormal();
		float blockY = 0.8f * lastY + (float) yBlockDistribution.NextNormal();

		BlockFactory.instance.getSingleBlockObstacle(
			lastX,
			blockY,
			blockWidth,
			(float) blockHeightDistribution.NextNormal());

		float maxBlockOffset = BLOCK_MAX_SPEED * 2 * blockWidth/PLAYER_MAX_SPEED;
		float minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

		float gapWidth = Mathf.Max(
			Random.Range(0, difficulty) * getMaxGapWidth(blockY + difficulty * maxBlockOffset, minY),
			MIN_GAP
		);

		float groundY = Mathf.Max(blockY + Random.Range(-1,1) * difficulty * getMaxGapHeight(gapWidth), minY);

		float groundX = lastX + blockWidth + gapWidth;
			
		float groundWidth = (float) groundWidthDistribution.NextNormal();

		return GroundFactory.GetGround(
			groundX,
			groundY,
			groundWidth, GroundFactory.GRASS);
	}

	private float getMaxGapWidth(float y0, float yMin) {
		float p = PLAYER_MAX_SPEED / 2 / PLAYER_GRAVITY;
		return -p + Mathf.Sqrt(p + (yMin - y0)/PLAYER_GRAVITY);
	}

	private float getMaxGapHeight(float width) {
		float travelTime = width/PLAYER_MAX_SPEED;
		return BLOCK_MAX_SPEED * travelTime + PLAYER_GRAVITY * travelTime * travelTime;
	}
}
