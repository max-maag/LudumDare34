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

	public float timeTillRestartPossible;

	private const string UP_BUTTON_NAME = "up";
	private const string DOWN_BUTTON_NAME = "down";

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
		} else {
			if(Input.GetButton(UP_BUTTON_NAME) || Input.GetButton(DOWN_BUTTON_NAME)) {
				OnRestartButtonClick();
			}
			timeTillRestartPossible -= Time.deltaTime;
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
		if(timeTillRestartPossible <= 0.0f)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
