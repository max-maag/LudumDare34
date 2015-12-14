using UnityEngine;
using System.Collections;

public class CannonSectionGenerator : IMapSectionGenerator {

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

	public GameObject GenerateSection(float difficulty, float xNextElement, GameObject lastElement) {
		float widthOfGroundBlock = (float) widthOfGroundBlockDistribution.NextNormal ();
		float widthOfCanonBlock = (float) widthOfCannonBlockDistribution.NextNormal ();

		float yOffsetOfBlock = lastElement.GetComponentInChildren<Collider2D>().bounds.max.y;

		Debug.Log("yOffsetOfBlock:" + yOffsetOfBlock);

		GroundFactory.GetGround(
			xNextElement,
			yOffsetOfBlock,
			widthOfGroundBlock, GroundFactory.GRASS);

		float cannonX = xNextElement+widthOfGroundBlock;

		switch(Random.Range (0, 2)) {
		case 0: BlockFactory.instance.getMultBlockObstacleWithCanon (cannonX, yOffsetOfBlock+5, widthOfCanonBlock, new float[] { 1.0f, 1.0f }, new float[] { 2 }, new bool[] {false, true});
			break;
		case 1: BlockFactory.instance.getMultBlockObstacleWithCanon (cannonX, yOffsetOfBlock-2, widthOfCanonBlock, new float[] { 1.0f, 1.0f }, new float[] { 2 }, new bool[] {true, false});
			break;
		}

		float GroundX = cannonX+widthOfCanonBlock;

		return GroundFactory.GetGround(
			GroundX ,
			yOffsetOfBlock,
			widthOfGroundBlock-2, GroundFactory.GRASS);
	}
}
