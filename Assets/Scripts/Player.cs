using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class Player : MonoBehaviour {

	//Behaviour content

	void Start () 
	{
		bulletLayer = gameObject.layer;
	}
	
	void Update () 
	{

		//Face the mouse
		FacetToMouse (rotSpeed);

		// Move in forward relative to player Input.GetAxis("Vertical")
		MoveForward(maxSpeed, Input.GetAxis("Vertical"));

		// Avoid hitting the camara Ortographic Boundaries
		RestrictToBoundaries (shipBoundaryRadius);

		//Shooting
		cooldownTimer -= Time.deltaTime;
		if(Input.GetButton("Fire1") && cooldownTimer <= 0 && Time.deltaTime > 0) 
		{
			//Reset timer
			cooldownTimer = fireDelay;
			// SHOOT!
			Shoot(bulletPrefab, bulletOffset);

		}

		ShieldRegenerator ();


	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Asteroid"){
			MoveForward (-2.5f*maxSpeed);
		}
		DamageHandler (1);
		if (health <= 0) Die ();
	}

	//Class Definition

	//Movement
	public float maxSpeed = 5f;
	public float rotSpeed = 180f;
	//Boundaries
	float shipBoundaryRadius = 0.5f;
	//Shooting
	public Vector3 bulletOffset = new Vector3(0, 0.5f, 0);
	public GameObject bulletPrefab;
	int bulletLayer;
	public float fireDelay = 0.25f;
	float cooldownTimer = 0;
	//Stats

	public float health = 10f;
	public float maxHealth = 10f;
	public float shield = 10f;
	public float maxShield = 10f;
	public float regenRate = 0.2f;




	void Die() {
		Destroy(gameObject);
	}

	void ShieldRegenerator(){
		if (this.shield < maxShield) {
			this.shield += this.regenRate * Time.deltaTime;
		}
	}

	void DamageHandler(float damage){

		if (this.shield > damage) {
			this.shield -= damage;
		}
		else{
			damage -= this.shield;
			this.shield = 0;
			this.health -= damage;
		}

	}

	void MoveForward(float maxSpeed, float input = 1f)
	{
		//Moves forward according to the vertical axis 
		//or just forward if not given an input

		Vector3 velocity = new Vector3(0, input * maxSpeed * Time.deltaTime, 0);
		transform.position += transform.rotation * velocity;

	}

	void Shoot(GameObject prefab, Vector3 offset)

	{
		cooldownTimer = fireDelay;
		// SHOOT!
		Vector3 newOffset = transform.rotation * offset;
		GameObject bulletGO = (GameObject)Instantiate(prefab, transform.position + newOffset, transform.rotation);
		bulletGO.layer = bulletLayer;


	}
		

	void FacetToMouse(float rotSpeed)
	{
		
		// Gets the mouse positions and transforms it into world units from pixels
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Calculates the vector between the ship and the mouse position
		Vector3 dir = mousePos - transform.position;
		dir.Normalize(); 
		// Calculates angle of the vector and creates quaternion with the right angle
		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
		Quaternion desiredRot = Quaternion.Euler( 0, 0,zAngle );
		// Rotates towards desired rotation with rotation speed
		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);


	}

	void WASDMovement(float maxSpeed, float rotSpeed)

	{
		// ROTATE the ship.

		// Grab our rotation quaternion
		Quaternion rot = transform.rotation;
		// Grab the Z euler angle
		float z = rot.eulerAngles.z;
		// Change the Z angle based on input
		z -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
		// Recreate the quaternion
		rot = Quaternion.Euler( 0, 0, z );
		// Feed the quaternion into our rotation
		transform.rotation = rot;
		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime, 0);
		pos += transform.rotation * velocity;

	}


	void RestrictToBoundaries(float objectRadius)

	{

		// RESTRICT the player to the camera's boundaries!
		Vector3 pos = transform.position;
		// First to vertical, because it's simpler
		if(pos.y+objectRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - objectRadius;
		}
		if(pos.y-objectRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + objectRadius;
		}

		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		// Now do horizontal bounds
		if(pos.x+objectRadius > widthOrtho) {
			pos.x = widthOrtho - objectRadius;
		}
		if(pos.x-objectRadius < -widthOrtho) {
			pos.x = -widthOrtho + objectRadius;
		}

		// Finally, update our position!!
		transform.position = pos;

	}


		

}
