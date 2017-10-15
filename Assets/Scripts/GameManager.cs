using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	float clock; 

	float slowDown; 

	// Use this for initialization
	void Start () {
		clock = 60;
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
			slowDown = .9f;
			break; 
		case 4:
			slowDown = .7f;
			break; 
		case 3:
			slowDown = .5f;
			break; 
		case 2:
			slowDown = .35f; 
			break;
		case 1: 
			slowDown = .2f;
			break;
		case 0: 
			slowDown = .18f;
			break; 
		}
		Debug.Log (clock);
	}
}
