using UnityEngine;
using System.Collections;

public class JumpSectionGenerator : IMapSectionGenerator {
	public NormalDistribution yBlockDistribution;
	public NormalDistribution blockWidthDistribution;
	public NormalDistribution blockHeightDistribution;
	public NormalDistribution gapSizeDistribution;
	public NormalDistribution yGroundDistribution;
	public NormalDistribution groundWidthDistribution;

	public JumpSectionGenerator(
		NormalDistribution yBlock,
		NormalDistribution blockWidth,
		NormalDistribution blockHeight,
		NormalDistribution gapSize,
		NormalDistribution yGround,
		NormalDistribution groundWidth) {

		yBlockDistribution = yBlock;
		blockWidthDistribution = blockWidth;
		blockHeightDistribution = blockHeight;
		gapSizeDistribution = gapSize;
		yGroundDistribution = yGround;
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


		float groundX = lastX + blockWidth + (float) gapSizeDistribution.NextNormal();
		float groundWidth = (float) groundWidthDistribution.NextNormal();

		return GroundFactory.GetGround(
			groundX,
			blockY + (float) yBlockDistribution.NextNormal(),
			groundWidth, GroundFactory.GRASS);
	}
}
