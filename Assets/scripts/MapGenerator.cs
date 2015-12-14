using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
	private const string PLAYER_TAG = "player";

	private const float EASY_DIFFICULTY = -500;
	private const float NORMAL_DIFFICULTY = 0;
	private const float HARD_DIFFICULTY = 250;

	private readonly IMapSectionGenerator[] sectionGenerators = {
		new JumpSectionGenerator(
			new NormalDistribution(0,2),
			new NormalDistribution(2,0.5),
			new NormalDistribution(5, 1),
			new NormalDistribution(3, 0.8),
			new NormalDistribution(0.5, 0.3),
			new NormalDistribution(5, 1)
		),
		new RandomBlockGenerator(
			new NormalDistribution(0,2),
			new NormalDistribution(2,0.5),
			new NormalDistribution(4,3)
		),
		new FlappyBirdSectionGenerator()
	};

	/// x coordinate at which the next element should be placed at (or: x coordinate up to which the level is defined)
	private float xNextElement;

	/// the first x coordinate at which an element was generated
	private float xNextElementInitial;
	private float currentDifficulty;
	private float difficulty;
	private GameObject lastElement;

	// Use this for initialization
	void Start () {
		// TODO get this from the menu or somewhere
		difficulty = EASY_DIFFICULTY;
		adjustDifficulty (0);

		GameObject lastElement = GameObject.FindWithTag(Tags.GROUND_TAG);
		xNextElement = lastElement.GetComponent<Collider2D> ().bounds.max.x;

	}

	// Update is called once per frame
	void Update () {
		float xRightOfCamera = Camera.main.ViewportToWorldPoint(Vector3.right).x;
		while(xNextElement <= xRightOfCamera) {
			lastElement =  sectionGenerators[Random.Range(0, sectionGenerators.Length)].GenerateSection(0, xNextElement, lastElement);
			xNextElement = lastElement.GetComponentInChildren<Collider2D>().bounds.max.x;
			adjustDifficulty (xNextElement - xNextElementInitial);
		}
	}

	// see http://www.wolframalpha.com/input/?i=plot+1%2F%281%2Bexp%28-0.005+*+%28x+-500%29%29%29+for+x+%3D+0+to+1000
	private void adjustDifficulty(float distanceTraveled) {
		currentDifficulty = 1.0f / (1 + Mathf.Exp (-0.005f * (distanceTraveled + difficulty)));
	}
}