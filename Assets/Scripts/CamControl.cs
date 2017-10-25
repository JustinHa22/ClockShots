using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {
    public float lerpAmnt;
    Vector2 truePos;

    public int shakeTimer;
    public float shakeIntensity;
    public static CamControl me;
    // Use this for initialization
    void Start () {
        me = this;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		truePos = transform.position;


        Vector2 shake = Vector2.zero;
        if (shakeTimer > 0) {
            shakeTimer--;
            shake = Random.insideUnitCircle * shakeIntensity;
        }
        transform.position = truePos + shake;

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
	}

    public void Shake(float intensity, int time) {
        shakeTimer = Mathf.Max(shakeTimer, time);
        shakeIntensity = Mathf.Max(shakeIntensity, intensity);
    }

}
