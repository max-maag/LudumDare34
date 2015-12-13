using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
	public CanvasGroup gameOverScreen;
	public CanvasGroup score;

	private ScoreKeeper playerScore;

	private float startTime;

	private bool isPlayerDead;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		playerScore = gameObject.GetComponent<ScoreKeeper>();
		isPlayerDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPlayerDead) {
			score.gameObject.GetComponentInChildren<Text>().text =
				"Score: " + (long) Mathf.Round(playerScore.score);
		}
	}

	void OnPlayerDeath() {
		gameOverScreen.gameObject.SetActive(true);
		score.gameObject.SetActive(false);
		isPlayerDead = true;
	}

	public void OnRestartButtonClick() {
		SceneManager.LoadScene(0);
	}
}
