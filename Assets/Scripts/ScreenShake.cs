using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	Vector3 startPosition; // remember where we were before we started shaking
	public float shakeStrength = 0f;

	// Use this for initialization
	void Start () {
		startPosition = transform.localPosition;
	}

	// Update is called once per frame
	void Update () {

			shakeStrength = 3f;

		// decay the shake strength
		shakeStrength = Mathf.Clamp( shakeStrength - Time.deltaTime, 0f, 1f);

//		Vector3 shakeSideDirection = transform.right * Mathf.Sin( Time.time * 27f) * 0.16f;
//		Vector3 shakeUpDirection = transform.up * Mathf.Sin( Time.time * 23f) * 0.16f;
//		transform.localPosition = startPosition + (shakeSideDirection + shakeUpDirection) * shakeStrength;

	}

	public void shake(){
		Vector3 shakeSideDirection = transform.right * Mathf.Sin( Time.time * 27f) * 0.16f;
		Vector3 shakeUpDirection = transform.up * Mathf.Sin( Time.time * 23f) * 0.16f;
		transform.localPosition = startPosition + (shakeSideDirection + shakeUpDirection) * shakeStrength;
	}
}