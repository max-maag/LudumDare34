using UnityEngine;
using System.Collections;

public class JumpSectionGenerator : IMapSectionGenerator {
	private const float MIN_GAP = 2f; // 2 * player width

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
		float blockY = 0.5f * lastY + (float) yBlockDistribution.NextNormal();

		BlockFactory.instance.getSingleBlockObstacle(
			lastX,
			blockY,
			blockWidth,
			(float) blockHeightDistribution.NextNormal());

		float maxBlockOffset = BLOCK_MAX_SPEED * 2 * blockWidth/PLAYER_MAX_SPEED;
		float minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y;

		Debug.Log(getMaxGapWidth(blockY + /*difficulty */ maxBlockOffset, minY));

		float gapWidth = Mathf.Max(
			Random.Range(difficulty, 1f) * getMaxGapWidth(blockY + maxBlockOffset, minY),
			MIN_GAP
		);

		float groundY = 0.8f * Mathf.Max(
			blockY + Random.Range(difficulty,1) * (maxBlockOffset + getMaxGapHeight(gapWidth) - MIN_GAP/2),
			minY);

		float groundX = lastX + blockWidth + gapWidth;
			
		float groundWidth = (float) groundWidthDistribution.NextNormal();

		return GroundFactory.GetGround(
			groundX,
			groundY,
			groundWidth, GroundFactory.GRASS);
	}

	private float getMaxGapWidth(float y0, float yMin) {
		float p = BLOCK_MAX_SPEED * 0.5f / PLAYER_GRAVITY;
		return PLAYER_MAX_SPEED * (-p + Mathf.Sqrt(p + (yMin - y0)/PLAYER_GRAVITY)) + MIN_GAP/2;
	}

	private float getMaxGapHeight(float width) {
		float travelTime = width/PLAYER_MAX_SPEED;
		return BLOCK_MAX_SPEED * travelTime + PLAYER_GRAVITY * travelTime * travelTime;
	}
}
