using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	float clock; 

	float slowDown; 

	int score; 
	int highScore; 

	public Text scoreText; 
	public Text clockText; 

	public bool gameOver; 
	public Text gameOverText; 

	public Text highScoreText; 

	public Text restartText; 

	string highscoreKey = "Highscore"; 

	bool restart; 

	public AudioSource audioSource; 
	public AudioClip targetHit; 
	public AudioClip shotSound; 
	public AudioClip slowShotSound; 
	public AudioClip hitWallSound; 

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		//Sets the clock to 60 secs when the game starts
		clock = 61;
		//Score starts at 0 
		score = 0; 
		//Turns off restart function so the game can be played
		restart = false; 
	}
	
	// Update is called once per frame
	void Update () {
		//Sets the highscore to the highest score stored in PlayerPrefs
		highScore = PlayerPrefs.GetInt (highscoreKey); 

		//The clock will go down depending on how much the bullets have slowed down
		clock -= (slowDown * Time.deltaTime); 

		//Finds the gun in the scene and gets the script on it
		GameObject gun = GameObject.Find ("Gun");
		Gun gunscript = gun.GetComponent<Gun> ();

		//This switch statement gives each bullet count an amount to slow down the clock by
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

		//The clock will read the lowest int of the current time
		clockText.text = "" + Mathf.FloorToInt(clock); 

		//Finds the target in the scene and gets the scripts
		GameObject target = GameObject.FindGameObjectWithTag ("Target");
		Target targetScript = target.GetComponent<Target> ();

		//If target was hit with a bullet, add 1 point
		if (targetScript.bulletHit) {
			audioSource.PlayOneShot (targetHit, .3f);
			score += 1; 
		}
		//If target was hit with a hitbox/melee attack, add 1 point
		if (targetScript.hitboxHit) {
			audioSource.PlayOneShot (targetHit, .3f);
			score += 1; 
		}

		if (gunscript.shotsound && gunscript.bulletCount > 3f) {
			audioSource.PlayOneShot (shotSound, 1f); 
			gunscript.shotsound = false;
		} else if (gunscript.shotsound && gunscript.bulletCount <= 3f) {
			audioSource.PlayOneShot (slowShotSound, 1f); 
			gunscript.shotsound = false;
		}

		GameObject walls = GameObject.FindGameObjectWithTag ("Wall");
		Walls wallScript = walls.GetComponent<Walls> ();

		if (wallScript.hitSound) {
			audioSource.PlayOneShot (hitWallSound); 
			wallScript.hitSound = false;
		}

		//The score will show as the score earned this round
		scoreText.text = "Score: " + score;  

		//If the score is greater than the highscore, than set the PlayerPrefs to the score
		if (score > highScore) {
			PlayerPrefs.SetInt (highscoreKey, score); 
			PlayerPrefs.Save (); 
		}

		//If the clock reaches less than 0, Game Over
		if (clock <= 0) {
			clock = 0; 
			GameOver (); 
		}

		//Finds the player on the scene and gets its script 
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		Movement playerScript = player.GetComponent<Movement> ();

		//If player is hit with a bullet, Game Over
		if (playerScript.playerDead) {
			GameOver ();
		}

		//If the restart is true and Escape is pressed, load the scene again and return Time.timeScale back to 1
		if (restart) {
			if (Input.GetKeyDown (KeyCode.Escape)) {
				SceneManager.LoadScene(0);
				Time.timeScale = 1; 
			}
		}
	}

	//Function that is called when the game is over and presents the score and highscore on the screen
	void GameOver(){
		Time.timeScale = 0;
		gameOverText.text = "Game Over"; 
		highScoreText.text = "Highscore: " + highScore; 
		clockText.text = "";
		restartText.text = "Press Esc to Restart"; 
		restart = true; 
	}

}
