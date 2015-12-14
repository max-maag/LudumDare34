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


	public float GenerateSection(float difficulty, float lastX) {
		float blockWidth = (float) blockWidthDistribution.NextNormal();
		BlockFactory.instance.getSingleBlockObstacle(
			lastX,
			(float) yBlockDistribution.NextNormal(),
			blockWidth,
			(float) blockHeightDistribution.NextNormal());


		float groundX = lastX + blockWidth + (float) gapSizeDistribution.NextNormal();
		float groundWidth = (float) groundWidthDistribution.NextNormal();

		GroundFactory.GetGround(
			groundX,
			(float) yBlockDistribution.NextNormal(),
			groundWidth, GroundFactory.GRASS);

		return groundX + groundWidth;
	}
}
