using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

public class AsteroidMovement : MonoBehaviour {


	public float maxSpeed = 0.25f;
	public float rotSpeed = 60f;
	private float startingAngle;
	public Vector3 velocity;

	void Start () {

		startingAngle = Random.Range (1, 90);
		velocity = new Vector3(1,Random.Range(-1,1)*0.5f,0).normalized * maxSpeed * Random.Range(0.5f,2f);
	
	}
	
	// Update is called once per frame
	void Update () {
		//this.boundarieRestriction.RestrictToBoundaries (0.8f);
		startingAngle += 0.5f;
		Quaternion desiredRot = Quaternion.Euler( 0, 0,startingAngle );
		transform.rotation = Quaternion.RotateTowards( transform.rotation, desiredRot, rotSpeed * Time.deltaTime);
		transform.position += velocity * Time.deltaTime;
	}
}
