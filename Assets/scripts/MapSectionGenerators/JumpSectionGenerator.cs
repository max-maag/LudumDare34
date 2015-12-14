using UnityEngine;
using System.Collections;

public class JumpSectionGenerator : IMapSectionGenerator {
	public float GenerateSection(float difficulty, float lastX) {
		BlockFactory.instance.getSingleBlockObstacle(lastX, 0, 2, 1);
		GroundFactory.instance.getEarth(lastX+3, 1, 5);

		return lastX+8;
	}
}
