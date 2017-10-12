using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Rigidbody2D rb; 

	Vector3 vel; 
	Vector3 p;

	public Transform firePoint;

	public float shootSpeed; 

	Vector3 velchange1;
	Vector3 velchange2; 
	Vector3 velchange3; 
	Vector3 velchange4; 
	Vector3 velchange5; 
	Vector3 velchange6; 

	bool hitWall; 
	bool hitRoof; 

	void Start(){
		//Gets the rigidbody of the bullet
		rb.GetComponent<Rigidbody2D> ();

		//Input.mousePosition is local, this code changes it to World Point
		p = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		//Creates the velocity by getting the world point position of the mouse - the position of the bullet multiplied by the bullet speed
		vel = ((Vector2)p - (Vector2)transform.position).normalized * shootSpeed;

		//Goes into the Gun script and makes the script usuable in the bullet script
		GameObject gun = GameObject.Find ("Gun");
		Gun gunscript = gun.GetComponent<Gun> ();

			velchange1 = (vel *= .90f);

			velchange2 = (vel *= .75f);

			velchange3 = (vel *= .60f); 

			velchange4 = (vel *= .45f);

			velchange5 = (vel *= .30f); 

			velchange6 = (vel *= .20f);

	}


	// Update is called once per frame
	void Update () {
		//Constatnly changes the position according to the intial velocity made at start
		transform.position += vel; 

		//Goes into the Gun script and makes the script usuable in the bullet script
		GameObject gun = GameObject.Find ("Gun");
		Gun gunscript = gun.GetComponent<Gun> ();

		if (gunscript.bulletCount == 5) {
			vel = velchange1;
		}

		if (gunscript.bulletCount == 4) {
			vel = velchange2;
		}

		if (gunscript.bulletCount == 3) {
			vel = velchange3; 
		}

		if (gunscript.bulletCount == 2) {
			vel = velchange4;
		}

		if (gunscript.bulletCount == 1) {
			vel = velchange5; 
		}

		if (gunscript.bulletCount == 0) {
			vel = velchange6;
		}

		//If a wall was hit, reverse the x velocity
		if (hitWall == true) {
			vel.x *= -1f;
			hitWall = false;
		}

		//If the roof was hit, reverse the y velocity
		if (hitRoof == true) {
			vel.y *= -1f;
			hitRoof = false;
		}

		//If the bullet goes out of bounds and its x position is greater than 10 or less than -10, then destroy it
		if (transform.position.x > 10 || transform.position.x < -10) {
			Destroy (gameObject);
		}
			
	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Wall") {
			hitWall = true;

		}
		if (coll.gameObject.tag == "Roof") {
			hitRoof = true;
		}
	}
}
