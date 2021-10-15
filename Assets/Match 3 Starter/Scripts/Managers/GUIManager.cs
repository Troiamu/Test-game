﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour {
	public static GUIManager instance;

	public GameObject gameOverPanel;
	public Text yourScoreTxt;
	public Text highScoreTxt;

	public Text scoreTxt;
	public Text moveCounterTxt;

	public GameObject pauseScreen;

	private int score, moveCounter;

	void Awake() {
		instance = GetComponent<GUIManager>();
		moveCounter = 99;
	}

	// Show the game over panel
	public void GameOver() {
		GameManager.instance.gameOver = true;

		gameOverPanel.SetActive(true);

		if (score > PlayerPrefs.GetInt("HighScore")) {
			PlayerPrefs.SetInt("HighScore", score);
			highScoreTxt.text = "New Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		} else {
			highScoreTxt.text = "Best: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		yourScoreTxt.text = score.ToString();
	}

	public int Score {
		get {
			return score;
		}

		set {
			score = value;
			scoreTxt.text = score.ToString();
		}
	}

	public int MoveCounter {
		get {
			return moveCounter;
		}

		set {
			moveCounter = value;
			if (moveCounter <= 0) {
				moveCounter = 0;
				StartCoroutine(WaitForShifting());
			}
			moveCounterTxt.text = moveCounter.ToString();
		}
	}

	private IEnumerator WaitForShifting() {
		yield return new WaitUntil(() => !BoardManager.instance.IsShifting);
		yield return new WaitForSeconds(.25f);
		GameOver();
	}

	void Update() 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			PauseUnpause();
		}

	}

	public void PauseUnpause()
	{
		if(!pauseScreen.activeInHierarchy)
		{
			pauseScreen.SetActive(true);
			Time.timeScale = 0f;
		} else
		{
			pauseScreen.SetActive(false);
			Time.timeScale = 1f;
		}

	}

	public void Restart() 
	{
		 SceneManager.LoadScene("Game");
	}


}
