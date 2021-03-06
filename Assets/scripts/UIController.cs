﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
	public CanvasGroup gameOverScreen;
	public Text scoreTextfield;
	public Text gameOverScoreTextfield;
	public Text highScoreTextfield;

	private ScoreKeeper playerScore;

	private float startTime;

	private bool isPlayerDead;
	private bool scoreCheckDone;

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
			scoreTextfield.text = "Score: " + (long) Mathf.Round(playerScore.score);
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

		if(scoreTextfield != null) {
			scoreTextfield.gameObject.SetActive(false);
		}

		float score =  (long) Mathf.Round(playerScore.score);

		gameOverScoreTextfield.text = "Score: " + score;

		if(!scoreCheckDone) {
			if(checkScore(score)){
				PlayerPrefs.SetFloat(PlayerPrefsStrings.HIGHSCORE_FIELD,score);
				highScoreTextfield.text = "New highscore: " +PlayerPrefs.GetFloat(PlayerPrefsStrings.HIGHSCORE_FIELD) + "!!!";
			}
			else {
				highScoreTextfield.text = "Highscore: " +PlayerPrefs.GetFloat(PlayerPrefsStrings.HIGHSCORE_FIELD);
			}
			scoreCheckDone = true;
		}
	}

	public void OnRestartButtonClick() {
		if(timeTillRestartPossible <= 0.0f) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			scoreCheckDone = false;
		}
	}

	bool checkScore(float newScore) {
		if(PlayerPrefs.HasKey(PlayerPrefsStrings.HIGHSCORE_FIELD)){
			return PlayerPrefs.GetFloat(PlayerPrefsStrings.HIGHSCORE_FIELD)<newScore;
		}
		return true;
	}
}
