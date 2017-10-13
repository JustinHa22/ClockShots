using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public GameObject bullet; 

	public Transform Firepoint;

	public int bulletCount;

	bool shot; 

	bool reload; 

	public List<GameObject> bulletList = new List <GameObject>();  

	// Use this for initialization
	void Start () {
		bulletCount = 6;
	}
	
	// Update is called once per frame
	void Update (){
		//If the left click on the mouse is pressed, shot becomes true instantiating a bullet
		if (Input.GetMouseButtonDown (0) && bulletCount != 0) {
			shot = true; 
		}
		//If the right click on the mouse or R is pressed, then reloading becomes true
		if ((Input.GetMouseButtonDown (1) && bulletCount != 6) || Input.GetKeyDown(KeyCode.R) && bulletCount != 6) {
			reload = true;
		}

		//Get the Screen positions of the object
		Vector2 positionOnScreen = Camera.main.WorldToViewportPoint (transform.position);

		//Get the Screen position of the mouse
		Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint (Input.mousePosition);

		//Get the angle between the points
		float angle = AngleBetweenTwoPoints (positionOnScreen, mouseOnScreen);

		//Ta Daaa
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, angle));
	}

	void FixedUpdate(){
		//If left click is true and player has bullets, then spawn a bullet at the Firepoint and subtract 1 bullet from chamber
		if (shot == true && bulletCount > 0) {
			Instantiate (bullet,Firepoint.position, transform.rotation);
			bulletList.Add (bullet);
			bulletCount -= 1;
			shot = false;
		}

		if (bulletCount >= 6) {
			bulletCount = 6;
		}

		if (bulletCount <= 0) {
			bulletCount = 0;
		}

		if (reload == true) {
			bulletCount += 1; 
			reload = false;
		}
	}

	float AngleBetweenTwoPoints (Vector3 a, Vector3 b)
	{
		return Mathf.Atan2 (a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
	}
		

}
