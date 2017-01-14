using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Resources;

public class MinerDroidAI : MonoBehaviour {

	private GameObject currentAsteroid;
	private Vector3 asteroidVelocity;
	public float maxSpeed = 1.5f;
	public float rotSpeed = 60f;
	public float distanceToAsteroid = 0.5f;
	ParticleSystem particleEffect;
	int resources = 0;
	public GameLogic gameLogic;

	void Start(){
		particleEffect = GetComponent<ParticleSystem> ();
	}

	void Update(){
		if (currentAsteroid == null)
		{
			//Select Asteroid
			currentAsteroid = LookForClosestAsteroid ();
		}
			
		//Check if on top of the asteroid
		if (OnAsteroid(currentAsteroid))
		{
			//Sync with Asteroid Velocity
			SyncVelocity(currentAsteroid);
			//Mine
			Mine(currentAsteroid);
			return;
		}

		//Move to the target
		MoveToTarget (currentAsteroid.transform);


	}

	void Mine(GameObject asteroid){

		//Play Mining effect if not playing before
		if (!particleEffect.isPlaying) {
			particleEffect.Play ();
		}
		//Scale the asteroid down to achieve mining effect
		Vector3 scale = asteroid.transform.localScale;
		asteroid.transform.localScale = scale - Vector3.one*Time.deltaTime*0.3f;
		//Add resources
		addResources();
		//Destroy asteroid if scaled to 30%
		if (asteroid.transform.localScale.x < 0.3f) {
			Destroy (asteroid);
		}
	}

	void addResources(){
		if (Time.deltaTime > 0) {
			resources++;
			gameLogic.resources++;
		}
	}

	void MoveToTarget (Transform target){
		
		particleEffect.Stop ();
		FaceGOWithTag (target,rotSpeed);
		MoveForward (maxSpeed);
	}

	void MoveForward(float speed)
	{
		//Gets the position of the GameObject, creates 
		//a velocity vector and then rotates it to 
		//finally apply it to the the GO transform

		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;
		transform.position = pos;	

	}

	void FaceGOWithTag(Transform target,float rotSpeed)
	{

		Vector3 dir = target.transform.position - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle );

		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
	}


	GameObject LookForClosestAsteroid()
	{
		//Look for the closest metal Asteroid

		//Get list of METAL Asteroids
		GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		List<GameObject> metalAsteroids = new List<GameObject> ();
		foreach (GameObject ast in asteroids) 
			{
			if (ast.name == "MetalAsteroid_Big(Clone)" || ast.name == "MetalAsteroid_Medium(Clone)")
				metalAsteroids.Add (ast);
			}

		
		//Calculate Distance between Asteroids and Self

		//Choose closest one
		return metalAsteroids [0];
		
		}

	bool OnAsteroid(GameObject asteroid){
		return (Vector3.Distance (asteroid.transform.position,
			    this.transform.position) < distanceToAsteroid);
		 
	}

	void SyncVelocity(GameObject asteroid){
		
		asteroidVelocity = asteroid.GetComponent<AsteroidMovement> ().velocity;
		transform.position += asteroidVelocity*Time.deltaTime;
	
	}




}
