using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";

	private readonly IMapSectionGenerator[] sectionGenerators = {
		new JumpSectionGenerator(
			new NormalDistribution(0,1),
			new NormalDistribution(2,0.5),
			new NormalDistribution(5, 1),
			new NormalDistribution(3, 0.8),
			new NormalDistribution(0, 0.15),
			new NormalDistribution(5, 1)
		),
		new RandomBlockGenerator(
			new NormalDistribution(0,2),
			new NormalDistribution(2,0.5),
			new NormalDistribution(4,3)
		),
		new BlockAndGroundGenerator(
			new NormalDistribution(0,1),
			new NormalDistribution(4, 0.7),
			new NormalDistribution(3, 0.5),
			new NormalDistribution(0, 0.15),
			new NormalDistribution(5, 1)
		)
//		,
//		new CannonSectionGenerator(new NormalDistribution(0,2),
//			new NormalDistribution(2,0.5))
	};

	/// x coordinate at which the next element should be placed at (or: x coordinate up to which the level is defined)
	private float xNextElement, lastY;

	private GameObject lastElement;

	// Use this for initialization
	void Start () {
		lastElement = GameObject.FindWithTag(Tags.GROUND_TAG);
		xNextElement = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.x;
		lastY = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.y;

		lastElement = new BlockAndGroundGenerator(
			new NormalDistribution(0,1),
			new NormalDistribution(3, 0.5),
			new NormalDistribution(3, 0.3),
			new NormalDistribution(0, 1),
			new NormalDistribution(5, 0.2)
		).GenerateSection(0, xNextElement, lastY, lastElement);

		xNextElement = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.x;
		lastY = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.y;
	}
	
	// Update is called once per frame
	void Update () {
		float xRightOfCamera = Camera.main.ViewportToWorldPoint(Vector3.right).x;
		while(xNextElement <= xRightOfCamera) {
			lastElement =  sectionGenerators[Random.Range(0, sectionGenerators.Length)].GenerateSection(0, xNextElement, lastY, lastElement);
			xNextElement = lastElement.GetComponentInChildren<Collider2D>().bounds.max.x;
			lastY = lastElement.GetComponentInChildren<Collider2D>().bounds.max.y;
		}
	}
}
