using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	float clock; 

	float slowDown; 

	int score; 
	static int highScore; 

	public Text scoreText; 
	public Text clockText; 

	public bool gameOver; 
	public Text gameOverText; 

	public Text highScoreText; 

	public Text restartText; 

	bool restart; 

	// Use this for initialization
	void Start () {
		clock = 60;
		score = 0; 
		highScore = 0; 
		restart = false; 
	}
	
	// Update is called once per frame
	void Update () {

		clock -= (slowDown * Time.deltaTime); 

		GameObject gun = GameObject.Find ("Gun");
		Gun gunscript = gun.GetComponent<Gun> ();

		switch (gunscript.bulletCount) {
		case 6:
			slowDown = 1f; 
			break;
		case 5:
			slowDown = .875f;
			break; 
		case 4:
			slowDown = .75f;
			break; 
		case 3:
			slowDown = .625f;
			break; 
		case 2:
			slowDown = .5f; 
			break;
		case 1: 
			slowDown = .375f;
			break;
		case 0: 
			slowDown = .25f;
			break; 
		}
			
		clockText.text = "" + Mathf.FloorToInt(clock); 

		GameObject target = GameObject.FindGameObjectWithTag ("Target");
		Target targetScript = target.GetComponent<Target> ();

		if (targetScript.bulletHit) {
			score += 1; 
		}

		if (targetScript.hitboxHit) {
			score += 1; 
		}

		scoreText.text = "Score: " + score; 

		if (highScore < score) {
			highScore = score; 
		}

		if (clock <= 0) {
			GameOver (); 
		}

		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Movement playerScript = player.GetComponent<Movement> ();

		if (playerScript.playerDead) {
			//GameOver ();
		}

		if (restart) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				SceneManager.LoadScene(0);
				Time.timeScale = 1; 
			}
		}
	}

	void GameOver(){
		Time.timeScale = 0;
		gameOverText.text = "Game Over"; 
		highScoreText.text = "Highscore: " + highScore; 
		restartText.text = "Press Esc to Restart"; 
		restart = true; 
	}

}
