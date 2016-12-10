using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float maxSpeed = 5f;
	public float rotSpeed = 90f;
	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
	public GameObject bulletPrefab;
	int bulletLayer;
	public float fireDelay = 0.50f;
	float cooldownTimer = 0;

	void Start () {
	
	}
	

	void Update () {
	
		MoveForward (maxSpeed);
		FaceGOWithTag ("Player");
		//If cooldown timer has finished shoot
		//and reset cooldown
		cooldownTimer -= Time.deltaTime;
		if (cooldownTimer <= 0) {
			Shooting (bulletPrefab, bulletOffset);
			cooldownTimer = fireDelay;

		}

	}

	void MoveForward(float speed)
	{
		 //Gets the position of the GameObject, creates 
		 //a velocity vector and then rotates it to 
		 //finally apply it to the the GO transform

		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		transform.position = pos;	

	}

	void FaceGOWithTag(string tag)
	{
		//Rotates the GO to face another GO with 
		//the given tag

			GameObject go = GameObject.FindWithTag (tag);

		// At this point, we've either found the player,
		// or he/she doesn't exist right now.

		if(go == null)
			return;	// Try again next frame!

		Vector3 dir = go.transform.position - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle );

		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}

	void Shooting(GameObject bullet,Vector3 offset, string tag = "Player", float distance = 4.0f)

	{
		//Find the player and return if not found
		GameObject go = GameObject.FindWithTag (tag);
		if(go == null)
			return;	// Try again next frame!

		//If at shooting distance from target rotate offset 
		//and shoot bullet in self.layer
		if( Vector3.Distance(transform.position, go.transform.position) < distance) {

			Vector3 turnedOffset = transform.rotation * offset;
			GameObject bulletGO = (GameObject)Instantiate(bullet, transform.position + turnedOffset, transform.rotation);
			bulletGO.layer = gameObject.layer;
		}

	}






}
