using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	[HideInInspector]
	public float score;

	private bool stop;

	// Use this for initialization
	void Start () {
		score = 0;
	}

	void Update () {
		if (!stop) {
			score += 50 * Time.deltaTime;
		}
	}

	void OnPlayerDeath() {
		stop = true;
	}
		
}
