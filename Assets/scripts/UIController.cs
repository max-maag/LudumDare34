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
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPlayerDead) {
			score.text = "Score: " + (long) Mathf.Round(playerScore.score);
		}
	}

	void OnPlayerDeath() {
		isPlayerDead = true;
		if(gameOverScreen != null)
			gameOverScreen.gameObject.SetActive(true);

		if(score != null) {
			score.gameObject.SetActive(false);
		}
		gameOverScore.text = "Score: " + (long) Mathf.Round(playerScore.score);
	}

	public void OnRestartButtonClick() {
		SceneManager.LoadScene(0);
	}
}
