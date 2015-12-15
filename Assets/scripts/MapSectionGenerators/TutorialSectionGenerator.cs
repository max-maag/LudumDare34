using UnityEngine;
using System;


public class TutorialSectionGenerator : IMapSectionGenerator {

	private const int BLOCK_HEIGHT = 20;

	public GameObject GenerateSection(float difficulty, float xNextGenerate, float lastY, GameObject lastElement) {
		string tileset = GroundFactory.GRASS;

		// falling does not hurt
		float y = lastY;
		{
			float width = 8;
			y -= 1;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}
		{
			float width = 6;
			y -= 1;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}

		// single block which is already in place
		{
			float width = 5;
			BlockFactory.instance.getSingleBlockObstacle (xNextGenerate, y, width, BLOCK_HEIGHT);
			xNextGenerate += width;
		}
		{
			float width = 12;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}

		// block which needs to be moved upwards to pass
		{
			float width = 1;
			BlockFactory.instance.getSingleBlockObstacle (xNextGenerate, (float)y + 1.0f, width, 1.0f);
			xNextGenerate += width;
		}
		{
			float width = 11;
			y -= 2;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}

		// block which needs to be used as an elevator
		{
			float width = 7;
			BlockFactory.instance.getSingleBlockObstacle (xNextGenerate, y, width, BLOCK_HEIGHT);
			xNextGenerate += width;
		}
		{
			float width = 11;
			y += 5;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}

		// section which requires jumping
		{
			float width = 5;
			float gap = 4;
			BlockFactory.instance.getSingleBlockObstacle (xNextGenerate, y-1, width, BLOCK_HEIGHT);
			xNextGenerate += width;
			xNextGenerate += gap;
		}
		{
			float width = 12;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}

		// section which requires one to fix the first block
		{
			float width = 1;
			BlockFactory.instance.getSingleBlockObstacle (xNextGenerate, y, width, BLOCK_HEIGHT);
			xNextGenerate += width;
		}
		{
			float width = 7;
			BlockFactory.instance.getSingleBlockObstacle (xNextGenerate, y + 3, width, BLOCK_HEIGHT);
			xNextGenerate += width;
		}

		// section with a cannon
		{
			float width = 7;
			GroundFactory.GetGround (xNextGenerate, y, width, tileset);
			xNextGenerate += width;
		}
		{
			float width = 4;
			BlockFactory.instance.getMultBlockObstacleWithCanon(xNextGenerate, y+4, width, new float[] {1.5f, 2f}, new float[] {2.5f}, new bool[] {true, false});
			xNextGenerate += width;
		}

		// final ground before the "real" level starts
		return GroundFactory.GetGround (xNextGenerate, y, 10, tileset);
	}

}


