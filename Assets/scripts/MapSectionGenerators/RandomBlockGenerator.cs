using UnityEngine;
using System.Collections;

public class RandomBlockGenerator : IMapSectionGenerator {
	
	public NormalDistribution yOffsetOfBlockDistribution;
	public NormalDistribution heightOfBlockDistribution;
	public NormalDistribution widthOfBlockDistribution;

	public RandomBlockGenerator(
		NormalDistribution yOffset,
		NormalDistribution width,
		NormalDistribution height) {

		yOffsetOfBlockDistribution = yOffset;
		widthOfBlockDistribution = width;
		heightOfBlockDistribution = height;
		
	}

	public float GenerateSection(float difficulty, float xNextElement) {
		float heightOfBlock = (float) heightOfBlockDistribution.NextNormal ();
		float widthOfBlock = (float) widthOfBlockDistribution.NextNormal ();
		float yOffsetOfBlock = (float)yOffsetOfBlockDistribution.NextNormal ();

		if(Random.Range (0, 2) % 2 == 0) {
			BlockFactory.instance.getMultiBlockObstacle (xNextElement, yOffsetOfBlock, widthOfBlock, new float[] { 2.0f, 3.0f }, new float[] { 3 });
		} else {
			BlockFactory.instance.getSingleBlockObstacle (xNextElement, yOffsetOfBlock, widthOfBlock, heightOfBlock);
		}

		return xNextElement + widthOfBlock;
	}
}
