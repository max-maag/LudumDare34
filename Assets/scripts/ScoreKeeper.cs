using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	[HideInInspector]
	public float score;

	private bool stop;
	private float playerXOld;
	private GameObject player;

	// Use this for initialization
	void Start () {
		score = 0;
		player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
		playerXOld = player.transform.position.x;
	}

	void Update () {
		if (!stop) {
			score += (player.transform.position.x - playerXOld)*MapGenerator.instance.currentDifficulty;
			playerXOld = player.transform.position.x;
		}
	}

	void OnPlayerDeath() {
		stop = true;
	}
		
}
