using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	private const string UP_BUTTON_NAME = "up";
	private const string DOWN_BUTTON_NAME = "down";
	public Text highscoreTextfield;

	public Text easyTextfield;
	public Text middleTextfield;
	public Text hardTextfield;

	public GameObject canvas;
	public GameObject manualTexture;
	public GameObject difficultyCanvas;

	enum state{showTitle, showManual, showDifficulty};
	state titleState;

	int difficulty;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey(PlayerPrefsStrings.HIGHSCORE_FIELD)) {
			highscoreTextfield.text = "Highscore: " + PlayerPrefs.GetFloat(PlayerPrefsStrings.HIGHSCORE_FIELD); 
			difficulty = 1;
		} else {
			difficulty = 0;
		}
		titleState = state.showTitle;

		switch(difficulty) {
			case 0: easyTextfield.color = Color.black; break;
			case 1: middleTextfield.color = Color.black; break;
			case 2: hardTextfield.color = Color.black; break;
		}

	}

	void Update() {

		if(Input.GetKeyDown(KeyCode.Return) && titleState == state.showDifficulty) {
			PlayerPrefs.SetInt(PlayerPrefsStrings.DIFFICULTY_FIELD, difficulty);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		}
			
		if(Input.GetKeyDown(UP_BUTTON_NAME) || Input.GetKeyDown(DOWN_BUTTON_NAME)) {
			switch(titleState) {
			case state.showTitle : 
				showControls();
				titleState = state.showManual;
				break;
			case state.showManual:
				showDifficulty();
				titleState = state.showDifficulty;
				break;
			case state.showDifficulty:
				if(Input.GetKeyDown(UP_BUTTON_NAME)) {
					difficulty -= 1;
					if(difficulty < 0) difficulty = 0;
				}

				if(Input.GetKeyDown(DOWN_BUTTON_NAME)) {
					difficulty += 1;
					if(difficulty >2) difficulty = 2;
				}

				easyTextfield.color = Color.white;
				middleTextfield.color = Color.white;
				hardTextfield.color = Color.white;

				switch(difficulty) {
					case 0: easyTextfield.color = Color.black; break;
					case 1: middleTextfield.color = Color.black; break;
					case 2: hardTextfield.color = Color.black; break;
				}
				break;
			}

		}
	}

	void showControls() {
		canvas.SetActive(false);
		manualTexture.SetActive(true);
	}

	void showDifficulty() {
		difficultyCanvas.SetActive(true);
		manualTexture.SetActive(false);
	}
}
