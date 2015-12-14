using UnityEngine;
using System.Collections;

public class BlockAndGroundGenerator : IMapSectionGenerator {
	private NormalDistribution blockYDist;
	private NormalDistribution blockWidthDist;
	private NormalDistribution blockHeightDist;
	private NormalDistribution groundYDist;
	private NormalDistribution groundWidthDist;

	public BlockAndGroundGenerator(
		NormalDistribution blockY,
		NormalDistribution blockWidth,
		NormalDistribution blockHeight,
		NormalDistribution groundY,
		NormalDistribution groundWidth) {
			blockYDist = blockY;
			blockWidthDist = blockWidth;
			blockHeightDist = blockHeight;
			groundYDist = groundY;
			groundWidthDist = groundWidth;
	}

	public GameObject GenerateSection(float difficulty, float lastX, float lastY, GameObject lastElement) {
		float blockY = 0.8f * lastY + (float) blockYDist.NextNormal();
		float blockWidth = (float) blockWidthDist.NextNormal();
		float blockHeight = (float) blockHeightDist.NextNormal();

		BlockFactory.instance.getSingleBlockObstacle(lastX, blockY, blockWidth, blockHeight);
		return GroundFactory.GetGround(
			lastX + blockWidth,
			blockY + (float) groundYDist.NextNormal(),
			(float) groundWidthDist.NextNormal(),
			GroundFactory.GRASS
		);
	}
}
