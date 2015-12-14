using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

	public static MapGenerator instance;
	private const string PLAYER_TAG = "player";

	private const float EASY_DIFFICULTY = -150;
	private const float NORMAL_DIFFICULTY = -50;
	private const float HARD_DIFFICULTY = 100;

	private readonly IMapSectionGenerator[] sectionGenerators = {
		new JumpSectionGenerator(
			new NormalDistribution(0,1),
			new NormalDistribution(2,0.5),
			new NormalDistribution(5, 1),
			new NormalDistribution(3, 0.8),
			new NormalDistribution(0, 0.15),
			new NormalDistribution(5, 1)
		),
		new BlockAndGroundGenerator(
			new NormalDistribution(0,1),
			new NormalDistribution(4, 0.7),
			new NormalDistribution(3, 0.5),
			new NormalDistribution(0, 0.15),
			new NormalDistribution(5, 1)
		),
		new CannonSectionGenerator(new NormalDistribution(0,2),
			new NormalDistribution(5,1),
			new NormalDistribution(4,0.5)),
		new FlappyBirdSectionGenerator()
	};

	/// x coordinate at which the next element should be placed at (or: x coordinate up to which the level is defined)
	private float xNextElement;
	/// the first x coordinate at which an element was generated
	private float xNextElementInitial;

	private float lastY;
	private GameObject lastElement;
	
	public float currentDifficulty;
	private float difficulty;	

	// Use this for initialization
	void Start () {
		instance = this;
		int difficultyFromMenu = PlayerPrefs.GetInt(PlayerPrefsStrings.DIFFICULTY_FIELD);
		switch(difficultyFromMenu) {
			case 0: difficulty = EASY_DIFFICULTY; break;
			case 1: difficulty = NORMAL_DIFFICULTY; break;
			case 2: difficulty = HARD_DIFFICULTY; break;
			default: difficulty = EASY_DIFFICULTY; break;
		}
			
		adjustDifficulty (0);

		lastElement = GameObject.FindWithTag (Tags.GROUND_TAG);
		xNextElement = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.x;
		lastY = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.y;

		// necessary for calculating the current difficulty in each update
		xNextElementInitial = xNextElement;

		// start off with a tutorial-ish section for beginners of the game
		if(difficulty < NORMAL_DIFFICULTY) {
			lastElement = new TutorialSectionGenerator ().GenerateSection (currentDifficulty, xNextElement, lastY, lastElement);
			xNextElement = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.x;
			lastY = lastElement.GetComponentInChildren<Collider2D> ().bounds.max.y;
		}
	}

	// Update is called once per frame
	void Update () {
		float xRightOfCamera = Camera.main.ViewportToWorldPoint(Vector3.right).x;
		while(xNextElement <= xRightOfCamera) {
			lastElement =  sectionGenerators[Random.Range(0, sectionGenerators.Length)].GenerateSection(currentDifficulty, xNextElement, lastY, lastElement);
			xNextElement = lastElement.GetComponentInChildren<Collider2D>().bounds.max.x;
			lastY = lastElement.GetComponentInChildren<Collider2D>().bounds.max.y;
			adjustDifficulty (xNextElement - xNextElementInitial);
			Debug.Log (currentDifficulty);
		}
	}

	// see http://www.wolframalpha.com/input/?i=plot+1%2F%281%2Bexp%28-0.01+*+%28x+-150%29%29%29+for+x+%3D+0+to+1000
	private void adjustDifficulty(float distanceTraveled) {
		currentDifficulty = 1.0f / (1 + Mathf.Exp (-0.01f * (distanceTraveled + difficulty)));
	}
}