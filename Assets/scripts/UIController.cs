using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
	public CanvasGroup gameOverScreen;
	public Text score;
	public Text gameOverScore;

	private ScoreKeeper playerScore;

	private float startTime;

	private bool isPlayerDead;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		startTime = Time.time;
		playerScore = gameObject.GetComponent<ScoreKeeper>();
		isPlayerDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPlayerDead) {
			score.text = "Score: " + (long) Mathf.Round(playerScore.score);
		}
	}

	void OnPlayerDeath() {
		if(gameOverScreen != null)
			gameOverScreen.gameObject.SetActive(true);

		if(score != null)
			score.gameObject.SetActive(false);

		gameOverScore.text = "Score: " + (long) Mathf.Round(playerScore.score);

		Time.timeScale = 0;

		isPlayerDead = true;
	}

	public void OnRestartButtonClick() {
		SceneManager.LoadScene(0);
	}
}
