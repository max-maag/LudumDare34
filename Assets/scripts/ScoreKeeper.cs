using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {
	[HideInInspector]
	public float score;

	// Use this for initialization
	void Start () {
		score = 0;
	}

	void Update () {
		score += 50 * Time.deltaTime;
	}
}
