using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIController : MonoBehaviour {
	public CanvasGroup gameOverScreen;
	public CanvasGroup score;

	private float startTime;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		int seconds = (int) Mathf.Floor(Time.time - startTime);
		int minutes = seconds / 60;

		// Fucking string.Format, how does it work???
		string time = "";

		if(minutes < 10)
			time += "0";

		time += minutes + ":";

		if(seconds < 10)
			time += "0";

		time += seconds;

		score.gameObject.GetComponentInChildren<Text>().text = time;
	}

	void OnPlayerDeath() {
		gameOverScreen.gameObject.SetActive(true);
		score.gameObject.SetActive(false);
	}

	public void OnRestartButtonClick() {
		SceneManager.LoadScene(0);
	}
}
