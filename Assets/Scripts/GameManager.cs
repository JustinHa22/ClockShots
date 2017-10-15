using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	float clock; 

	float slowDown; 

	static int score; 

	// Use this for initialization
	void Start () {
		clock = 60;
		score = 0; 
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

		GameObject target = GameObject.FindGameObjectWithTag ("Target");
		Target targetScript = target.GetComponent<Target> ();

		if (targetScript.bulletHit) {
			score += 1; 
		}

		if (targetScript.hitboxHit) {
			score += 1; 
		}
		Debug.Log (score);
	}
}
